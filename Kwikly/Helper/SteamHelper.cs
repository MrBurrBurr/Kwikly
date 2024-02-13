using Microsoft.Win32;
using PuppeteerSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kwikly {
    class SteamHelper {
        private static string _steamPath = Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam").GetValue("SteamPath").ToString();
        private static long  _steamID64base = 76561197960265728;
        private static string _configFile = _steamPath + @"/config/config.vdf";
        private static string _steamURL = "http://steamcommunity.com/profiles/";
        private static string _csstatsURL = "https://csstats.gg/player/";
        private static string _csstatsRank = ".rank > .cs2rating";
        private static string _csstatsWins = ".ranks > .bottom > .wins > span";

        private static void Start() {
            Process.Start(Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam").GetValue("SteamExe").ToString());
        }

        private static void Stop() {
            while (IsRunning()) {
                ProcessStartInfo ps = new ProcessStartInfo(Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam").GetValue("SteamExe").ToString());
                ps.Arguments = "-shutdown";
                ps.WindowStyle = ProcessWindowStyle.Hidden;

                using (Process p = Process.Start(ps)) {
                    if (GetProcess() == null) return;
                    GetProcess().WaitForExit();
                }
            }
        }

        public static void Restart() {
            Stop();
            Start();
        }

        private static Process GetProcess() {
            Process[] steamProcesses = Process.GetProcessesByName("steam");
            return steamProcesses.Length == 0 ? null : steamProcesses[0];
        }

        public static bool IsRunning() {
            return GetProcess() != null ? true : false;
        }

        public static void SetLoginName(string loginName) {
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam", true)) {
                registryKey.SetValue("AutoLoginUser", loginName, RegistryValueKind.String);
            }
        }

        public static string CurrentUsername {
            get {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam")) {
                    return registryKey.GetValue("AutoLoginUser").ToString();
                }
            }
        }

        public static string[] GetAllLocalSteamID32() {
            string userdataDir = _steamPath + "/userdata/";

            List<string> accounts = new List<string>();

            string[] fileEntries = Directory.GetDirectories(userdataDir);

            foreach (string dirName in fileEntries) {
                string account = new DirectoryInfo(dirName).Name;

                if (IsDigitsOnly(account))
                    accounts.Add(account);
            }

            return accounts.ToArray();
        }

        private static bool IsDigitsOnly(string str) {
            foreach (char c in str) {
                if (c < '0' || c > '9')  return false;
            }

            return true;
        }

        public static string GetLoginNameBySteamID64(long steamID64) {
            Match match = Regex.Match(File.ReadAllText(_configFile), @"\""(.*)\""[^\r][^\S\n]+{[^\r][^\S\n]+\""SteamID\""\t\t\""" + steamID64, RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : "";
        }

        public static long GetSteamID64BySteamID32(int steamID32) {
            return _steamID64base + steamID32;
        }

        public static async Task<string> GetRankBySteamID64Async(long steamID64) {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            var url = _csstatsURL + steamID64;
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = false, Args = new[] { "--window-size=1,1" } }); // if headless is set to true csstats.gg will block us
            
            var page = await browser.NewPageAsync();
            await page.GoToAsync(url);

            string rank = "---";

            var element = await page.QuerySelectorAsync(_csstatsRank);
            if (element != null) {
                var innerText = await element.GetPropertyAsync("innerText");
                var csRank = await innerText.JsonValueAsync();
                rank = csRank.ToString();
            }

            if (rank == "---") {
                var cs2winsElement = await page.QuerySelectorAsync(_csstatsWins);
                if (element != null) {
                    var cs2InnerText = await cs2winsElement.GetPropertyAsync("innerText");
                    var csWins = await cs2InnerText.JsonValueAsync();
                    rank = csWins.ToString();
                }
            }

            browser.Dispose();
            return rank;
        }

        public static string GetNameBySteamID64(long steamID64) {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string html = client.DownloadString(_steamURL + steamID64);
            client.Dispose();

            Match match = Regex.Match(html, @"actual_persona_name\"">(.*)<\/", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : "";
        }
    }
}
