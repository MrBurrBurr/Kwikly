using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kwikly {
    class VisualHelper {
        private static Dictionary<int, Bitmap> rankToBitmap;
        private static Dictionary<int, Bitmap> primeToBitmap;
        private static Dictionary<int, Bitmap> dropToBitmap;
        private static Dictionary<int, int> bitmapToRank;
        private static Dictionary<int, int> bitmapToPrime;
        private static Dictionary<int, int> bitmapToDrop;

        static Bitmap Unranked = Properties.Resources.Unranked;
        static Bitmap S1 = Properties.Resources.S1;
        static Bitmap S2 = Properties.Resources.S2;
        static Bitmap S3 = Properties.Resources.S3;
        static Bitmap S4 = Properties.Resources.S4;
        static Bitmap SE = Properties.Resources.SE;
        static Bitmap SEM = Properties.Resources.SEM;
        static Bitmap GN1 = Properties.Resources.GN1;
        static Bitmap GN2 = Properties.Resources.GN2;
        static Bitmap GN3 = Properties.Resources.GN3;
        static Bitmap GN4 = Properties.Resources.GN4;
        static Bitmap MG1 = Properties.Resources.MG1;
        static Bitmap MG2 = Properties.Resources.MG2;
        static Bitmap MGE = Properties.Resources.MGE;
        static Bitmap DMG = Properties.Resources.DMG;
        static Bitmap LE = Properties.Resources.LE;
        static Bitmap LEM = Properties.Resources.LEM;
        static Bitmap SMFC = Properties.Resources.SMFC;
        static Bitmap GE = Properties.Resources.GE;

        static Bitmap Red = Properties.Resources.Red;
        static Bitmap Green = Properties.Resources.Green;

        static Bitmap Black = Properties.Resources.Black;
        static Bitmap Star = Properties.Resources.Star;

        public enum PrimeStateEnum {
            NonPrime = 0,
            Prime = 1
        }

        public enum DropStateEnum {
            NonDrop = 0,
            Drop = 1
        }

        static VisualHelper() {
            rankToBitmap = new Dictionary<int, Bitmap>() {
                {0, Unranked},
                {1, S1},
                {2, S2},
                {3, S3},
                {4, S4},
                {5, SE},
                {6, SEM},
                {7, GN1},
                {8, GN2},
                {9, GN3},
                {10, GN4},
                {11, MG1},
                {12, MG2},
                {13, MGE},
                {14, DMG},
                {15, LE},
                {16, LEM},
                {17, SMFC},
                {18, GE}
            };

            primeToBitmap = new Dictionary<int, Bitmap>() {
                {0, Red},
                {1, Green}
            };

            dropToBitmap = new Dictionary<int, Bitmap>() {
                {0, Black},
                {1, Star}
            };

            bitmapToRank = new Dictionary<int, int>() {
                {Unranked.GetHashCode(), 0},
                {S1.GetHashCode(), 1},
                {S2.GetHashCode(), 2},
                {S3.GetHashCode(), 3},
                {S4.GetHashCode(), 4},
                {SE.GetHashCode(), 5},
                {SEM.GetHashCode(), 6},
                {GN1.GetHashCode(), 7},
                {GN2.GetHashCode(), 8},
                {GN3.GetHashCode(), 9},
                {GN4.GetHashCode(), 10},
                {MG1.GetHashCode(), 11},
                {MG2.GetHashCode(), 12},
                {MGE.GetHashCode(), 13},
                {DMG.GetHashCode(), 14},
                {LE.GetHashCode(), 15},
                {LEM.GetHashCode(), 16},
                {SMFC.GetHashCode(), 17},
                {GE.GetHashCode(), 18}
            };

            bitmapToPrime = new Dictionary<int, int>() {
                {Red.GetHashCode(), 0},
                {Green.GetHashCode(), 1}
            };

            bitmapToDrop = new Dictionary<int, int>() {
                {Black.GetHashCode(), 0},
                {Star.GetHashCode(), 1}
            };
        }

        public static Bitmap RankToBitmap(int rank) {
            return rankToBitmap[rank];
        }

        public static Bitmap PrimeToBitmap(int primeState) {
            return primeToBitmap[primeState];
        }

        public static Bitmap DropToBitmap(int dropState) {
            return dropToBitmap[dropState];
        }

        public static int BitmapToRank(int rankBitmap) {
            return bitmapToRank[rankBitmap];
        }

        public static int BitmapToPrime(int primeBitmap) {
            return bitmapToPrime[primeBitmap];
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
