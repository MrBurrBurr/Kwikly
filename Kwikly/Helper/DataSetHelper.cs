using System;
using System.Data;
using System.Drawing;

namespace Kwikly {
    class DataSetHelper {

        public static void NormalizeForSaving(DataSet originalDataSet) {
            DataSet normalizedDataSet = new DataSet();
            DataTable settingTable = normalizedDataSet.Tables.Add("Settings");
            settingTable.Columns.Add("DropDate", typeof(DateTime));
            DataRow settingRow = settingTable.NewRow();
            DataRow originalRow = originalDataSet.Tables[0].Rows[0];
            settingRow["DropDate"] = originalRow["DropDate"];
            settingTable.Rows.Add(settingRow);

            DataTable normalizedTable = normalizedDataSet.Tables.Add("Account");

            normalizedTable.Columns.Add("Nr", typeof(string));
            normalizedTable.Columns.Add("Login", typeof(string));
            normalizedTable.Columns.Add("Name", typeof(string));
            normalizedTable.Columns.Add("Rank", typeof(int));
            normalizedTable.Columns.Add("Level", typeof(int));
            normalizedTable.Columns.Add("Prime", typeof(int));
            normalizedTable.Columns.Add("Drop", typeof(int));
            normalizedTable.Columns.Add("SteamID32", typeof(int));
            normalizedTable.Columns.Add("SteamID64", typeof(long));

            foreach (DataRow row in originalDataSet.Tables[1].Rows) {
                DataRow normalizedRow = normalizedTable.NewRow();
                normalizedRow["Nr"] = row["Nr"];
                normalizedRow["Login"] = row["Login"];
                normalizedRow["Name"] = row["Name"];
                normalizedRow["Rank"] = VisualHelper.BitmapToRank((row["Rank"] as Bitmap).GetHashCode());
                normalizedRow["Level"] = row["Level"];
                normalizedRow["Prime"] = VisualHelper.BitmapToPrime((row["Prime"] as Bitmap).GetHashCode());
                normalizedRow["Drop"] = VisualHelper.BitmapToDrop((row["Drop"] as Bitmap).GetHashCode());
                normalizedRow["SteamID32"] = row["SteamID32"];
                normalizedRow["SteamID64"] = row["SteamID64"];
                normalizedTable.Rows.Add(normalizedRow);
            }

            Config.Save(normalizedDataSet);
        }

        public static DataSet NormalizeDataSet(DataSet originalDataSet) {
            DataSet normalizedDataSet = new DataSet();
            DataTable settingsTable = normalizedDataSet.Tables.Add("Settings");
            settingsTable.Columns.Add("DropDate", typeof(DateTime));
            DataRow dropDateRow = settingsTable.NewRow();
            DataRow originalRow = originalDataSet.Tables[0].Rows[0];
            dropDateRow["DropDate"] = originalRow["DropDate"];
            settingsTable.Rows.Add(dropDateRow);

            DataTable normalizedTable = normalizedDataSet.Tables.Add("Account");

            normalizedTable.Columns.Add("Nr", typeof(string));
            normalizedTable.Columns.Add("Login", typeof(string));
            normalizedTable.Columns.Add("Name", typeof(string));
            normalizedTable.Columns.Add("Rank", typeof(Bitmap));
            normalizedTable.Columns.Add("Level", typeof(int));
            normalizedTable.Columns.Add("Prime", typeof(Bitmap));
            normalizedTable.Columns.Add("Drop", typeof(Bitmap));
            normalizedTable.Columns.Add("SteamID32", typeof(int));
            normalizedTable.Columns.Add("SteamID64", typeof(long));

            foreach (DataRow row in originalDataSet.Tables[1].Rows) {
                DataRow normalizedRow = normalizedTable.NewRow();
                normalizedRow["Nr"] = row["Nr"];
                normalizedRow["Login"] = row["Login"];
                normalizedRow["Name"] = row["Name"];
                normalizedRow["Rank"] = VisualHelper.RankToBitmap(int.Parse(row["Rank"].ToString()));
                normalizedRow["Level"] = row["Level"];
                normalizedRow["Prime"] = VisualHelper.PrimeToBitmap(int.Parse(row["Prime"].ToString()));
                normalizedRow["Drop"] = VisualHelper.DropToBitmap(int.Parse(row["Drop"].ToString()));
                normalizedRow["SteamID32"] = row["SteamID32"];
                normalizedRow["SteamID64"] = row["SteamID64"];
                normalizedTable.Rows.Add(normalizedRow);
            }

            return normalizedDataSet;
        }

        public static void CheckDropDate(DataSet ds) {
            if (ds != null) {
                DataRow dropRow = ds.Tables[0].Rows[0];
                TimeSpan difference = DateTime.Now.Date - DateTime.Parse(dropRow["DropDate"].ToString());

                if (difference.TotalDays >= 7) {
                    foreach (DataRow row in ds.Tables[1].Rows) {
                        row["Drop"] = VisualHelper.DropToBitmap((int)VisualHelper.DropStateEnum.NonDrop);
                    }

                    dropRow["DropDate"] = string.Format("{0:dd.MM.yyyy}", GetLastWednesday());
                }
            }
        }

        public static string GetDoubleDigitNumber(int number) {
            return number.ToString().Length == 1 ? "00" + number.ToString() : "0" + number.ToString();
        }

        public static DateTime GetLastWednesday() {
            DateTime today = DateTime.Now.Date;

            if (today.AddDays(-1).DayOfWeek == DayOfWeek.Wednesday) {
                return today.AddDays(-1).Date;
            }
            else if (today.AddDays(-2).DayOfWeek == DayOfWeek.Wednesday) {
                return today.AddDays(-2).Date;
            }
            else if (today.AddDays(-3).DayOfWeek == DayOfWeek.Wednesday) {
                return today.AddDays(-3).Date;
            }
            else if (today.AddDays(-4).DayOfWeek == DayOfWeek.Wednesday) {
                return today.AddDays(-4).Date;
            }
            else if (today.AddDays(-5).DayOfWeek == DayOfWeek.Wednesday) {
                return today.AddDays(-5).Date;
            }
            else if (today.AddDays(-6).DayOfWeek == DayOfWeek.Wednesday) {
                return today.AddDays(-6).Date;
            }
            else {
                return today;
            }
        }

        public static void CorrectAccountNumbers(DataSet ds) {
            DataTable dt = ds.Tables[1];
            foreach (DataRow row in dt.Rows) {
                row["Nr"] = GetDoubleDigitNumber(dt.Rows.IndexOf(row));
            }
        }
    }
}
