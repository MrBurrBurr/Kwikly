using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kwikly {
    internal static class VisualHelper {
        private static readonly Dictionary<int, Bitmap> dropToBitmap;
        private static readonly Dictionary<int, int> bitmapToDrop;
        private static readonly Bitmap Black = Properties.Resources.Black;
        private static readonly Bitmap Star = Properties.Resources.Star;
        private static readonly Color common = ColorTranslator.FromHtml("#B1C3D9");
        private static readonly Color uncommon = ColorTranslator.FromHtml("#5E98D7");
        private static readonly Color rare = ColorTranslator.FromHtml("#4B69FF");
        private static readonly Color mythical = ColorTranslator.FromHtml("#8846FF");
        private static readonly Color legendary = ColorTranslator.FromHtml("#D22CE6");
        private static readonly Color ancient = ColorTranslator.FromHtml("#EB4B4B");
        private static readonly Color unusual = ColorTranslator.FromHtml("#FED700");

        public enum DropStateEnum {
            NonDrop = 0,
            Drop = 1
        }
        static VisualHelper() {
            dropToBitmap = new Dictionary<int, Bitmap>() {
                {0, Black},
                {1, Star}
            };

            bitmapToDrop = new Dictionary<int, int>() {
                {Black.GetHashCode(), 0},
                {Star.GetHashCode(), 1}
            };
        }
        public static Bitmap DropToBitmap(int dropState) {
            return dropToBitmap[dropState];
        }
        public static int BitmapToDrop(int dropBitmap) {
            return bitmapToDrop[dropBitmap];
        }
        public static ProgressBar CreateNewProgressBar(MainForm frm, string text) {
            ProgressBar _pBar = new ProgressBar();
            _pBar.Location = new Point(0, 0);
            _pBar.Width = 200;
            _pBar.Height = 30;
            _pBar.Dock = DockStyle.Top;

            frm.Controls.Add(_pBar);
            frm.Height = 30;
            frm.Text = text;
            return _pBar;
        }
        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0 && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        public static void UpdateRankColor(string rank, DataGridViewCell currentCell) {
            if (rank == "---") {
                currentCell.Style.BackColor = common;
                return;
            }

            rank = rank.Replace(",", "");
            int rankInt;
            if (int.TryParse(rank, out rankInt))
            {
                var cellStyle = currentCell.Style;

                if (IsBetween(rankInt, 0, 4999)) cellStyle.BackColor = common;
                if (IsBetween(rankInt, 5000, 9999)) cellStyle.BackColor = uncommon;
                if (IsBetween(rankInt, 10000, 14999)) cellStyle.BackColor = rare;
                if (IsBetween(rankInt, 15000, 19999)) cellStyle.BackColor = mythical;
                if (IsBetween(rankInt, 20000, 24999)) cellStyle.BackColor = legendary;
                if (IsBetween(rankInt, 25000, 29999)) cellStyle.BackColor = ancient;
                if (IsBetween(rankInt, 30000, 99999)) cellStyle.BackColor = unusual;
            }
        }
        public static void VisualizeRanks(DataGridView dg)
        {
            foreach (DataGridViewRow account in dg.Rows)
            {
                string rank = account.Cells["Rank"].Value.ToString();
                UpdateRankColor(rank, account.Cells["Rank"]);
            }
        }

        public static void VisualizeLoggedInAccount(DataGridView dg) {
            foreach (DataGridViewRow row in dg.Rows) {
                foreach (DataGridViewColumn col in dg.Columns) {
                    dg.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.White;
                }
            }

            if (SteamHelper.IsRunning()) {
                foreach (DataGridViewRow account in dg.Rows) {
                    if (account.Cells["Login"].Value.ToString() == SteamHelper.CurrentUsername) {
                        account.Cells["Nr"].Style.BackColor = Color.LightGreen;
                        account.Selected = true;
                        break;
                    }

                    dg.ClearSelection();
                }
            }
            else {
                dg.ClearSelection();
            }
        }

        public static void VisualizeInactiveAccounts(DataGridView dg) {
            foreach (DataGridViewRow account in dg.Rows) {
                if (account.Cells["Login"].Value.ToString() == "") {
                    account.DefaultCellStyle.ForeColor = Color.LightGray;
                    break;
                }
            }
        }
    }
}
