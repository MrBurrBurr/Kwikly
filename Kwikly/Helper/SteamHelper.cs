using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Kwikly {
    class SteamHelper {
        private static string _steamPath = Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam").GetValue("SteamPath").ToString();
        private static long  _steamID64base = 76561197960265728;
        private static string _configFile = _steamPath + @"/config/config.vdf";
        private static string _steamURL = "http://steamcommunity.com/profiles/";

        private static void Start() {
            Process.Start(Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam").GetValue("SteamExe").ToString());
        }

        private static void Stop() {
            while (IsRunning()) {
                ProcessStartInfo ps = new ProcessStartInfo(Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam").GetValue("SteamExe").ToString());
                ps.Arguments = "-shutdown";
                ps.WindowStyle = ProcessWindowStyle.Hidden;

                using (Process p = Process.Start(ps)) {
                    if (GetProcess() == null)
                        return;

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

        public static int CurrentUserID {
            get {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam\\ActiveProcess")) {
                    return (int)registryKey.GetValue("ActiveUser");
                }
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
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static string GetLoginNameBySteamID64(long steamID64) {
            Match match = Regex.Match(File.ReadAllText(_configFile), @"\""(.*)\""[^\r][^\S\n]+{[^\r][^\S\n]+\""SteamID\""\t\t\""" + steamID64, RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : "";
        }

        public static string GetSteamID64ByLoginName(string loginName) {
            Match match = Regex.Match(File.ReadAllText(_configFile), @"\""" + loginName + @"\""[^\r][^\S\n]+[^\r]{[^\r][^\S\n]+\""SteamID\""[^\S]+\""([0-9]+)\""", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : "";
        }

        public static long GetSteamID64BySteamID32(int steamID32) {
            return _steamID64base + steamID32;
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
