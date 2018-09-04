using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Kwikly {
    class Config {
        public static string kwiklyFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Default.Kwikly";

        public static DataSet Load() {
            DataSet ds = new DataSet();
            ds.ReadXml(kwiklyFilePath);
            Save(ds);
            return ds;
        }

        public static void Save(DataSet ds) {
            ds.WriteXml(kwiklyFilePath);
        }

        public static bool DoesExists() {
            return File.Exists(kwiklyFilePath);
        }

        public static void Create(ProgressBar pBar) {
            DataSet ds = new DataSet();
            
            DataTable settingTable = ds.Tables.Add("Settings");
            settingTable.Columns.Add("DropDate");
            DataRow settingRow = settingTable.NewRow();
            settingRow["DropDate"] = DataSetHelper.GetLastWednesday();
            settingTable.Rows.Add(settingRow);

            DataTable accountTable = ds.Tables.Add("Account");
            accountTable.Columns.Add("Nr");
            accountTable.Columns.Add("Login");
            accountTable.Columns.Add("Name");
            accountTable.Columns.Add("Rank");
            accountTable.Columns.Add("Level");
            accountTable.Columns.Add("Prime");
            accountTable.Columns.Add("Drop");
            accountTable.Columns.Add("SteamID32");
            accountTable.Columns.Add("SteamID64");

            int i = 0;
            pBar.Minimum = i;
            pBar.Maximum = SteamHelper.GetAllLocalSteamID32().Length;

            foreach (string steamID32 in SteamHelper.GetAllLocalSteamID32()) {
                long _steamID64 = SteamHelper.GetSteamID64BySteamID32(int.Parse(steamID32));
                DataRow accountRow = accountTable.NewRow();
                accountRow["Nr"] = DataSetHelper.GetDoubleDigitNumber(i);
                accountRow["Login"] = SteamHelper.GetLoginNameBySteamID64(_steamID64);
                accountRow["Name"] = SteamHelper.GetNameBySteamID64(_steamID64);
                accountRow["Rank"] = 0;
                accountRow["Level"] = 1;
                accountRow["Prime"] = 0;
                accountRow["Drop"] = 0;
                accountRow["SteamID32"] = steamID32;
                accountRow["SteamID64"] = _steamID64;
                accountTable.Rows.Add(accountRow);
                pBar.Value = i++;
            }

            Save(ds);
        }

        public static int CheckForNewAccounts(DataSet ds, ProgressBar pBar) {
            List<string> currentSteamID32List = new List<string>();
            List<string> fetchedSteamID32List = new List<string>(SteamHelper.GetAllLocalSteamID32());

            foreach (DataRow row in ds.Tables[1].Rows) {
                currentSteamID32List.Add(row["SteamID32"].ToString());
            }
            
            List<string> newSteamID32List = fetchedSteamID32List.Except(currentSteamID32List).ToList();

            if (newSteamID32List.Count == 0)
                return 0;

            DataTable accountTable = ds.Tables[1];
            int i = accountTable.Rows.Count;
            pBar.Minimum = i;
            pBar.Maximum = i + newSteamID32List.Count;

            foreach (string steamID32 in newSteamID32List) {
                long _steamID64 = SteamHelper.GetSteamID64BySteamID32(int.Parse(steamID32));
                DataRow accountRow = accountTable.NewRow();
                accountRow["Nr"] = DataSetHelper.GetDoubleDigitNumber(i);
                accountRow["Login"] = SteamHelper.GetLoginNameBySteamID64(_steamID64);
                accountRow["Name"] = SteamHelper.GetNameBySteamID64(_steamID64);
                accountRow["Rank"] = VisualHelper.RankToBitmap(0);
                accountRow["Level"] = 1;
                accountRow["Prime"] = VisualHelper.PrimeToBitmap(0);
                accountRow["Drop"] = VisualHelper.DropToBitmap(0);
                accountRow["SteamID32"] = steamID32;
                accountRow["SteamID64"] = _steamID64;
                accountTable.Rows.Add(accountRow);
                pBar.Value = i++;
            }

            DataSetHelper.NormalizeForSaving(ds);

            return newSteamID32List.Count;
        }
    }
}
