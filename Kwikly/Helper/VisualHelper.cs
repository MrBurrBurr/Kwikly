using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kwikly {
    internal class VisualHelper {
        private static readonly Dictionary<int, Bitmap> rankToBitmap;
        private static readonly Dictionary<int, Bitmap> primeToBitmap;
        private static readonly Dictionary<int, Bitmap> dropToBitmap;
        private static readonly Dictionary<int, int> bitmapToRank;
        private static readonly Dictionary<int, int> bitmapToPrime;
        private static readonly Dictionary<int, int> bitmapToDrop;
        private static readonly Bitmap Unranked = Properties.Resources.Unranked;
        private static readonly Bitmap S1 = Properties.Resources.S1;
        private static readonly Bitmap S1_grey = Properties.Resources.S1_modified;
        private static readonly Bitmap S2 = Properties.Resources.S2;
        private static readonly Bitmap S2_grey = Properties.Resources.S2;
        private static readonly Bitmap S3 = Properties.Resources.S3;
        private static readonly Bitmap S3_grey = Properties.Resources.S3;
        private static readonly Bitmap S4 = Properties.Resources.S4;
        private static readonly Bitmap S4_grey = Properties.Resources.S4;
        private static readonly Bitmap SE = Properties.Resources.SE;
        private static readonly Bitmap SE_grey = Properties.Resources.SE_modified;
        private static readonly Bitmap SEM = Properties.Resources.SEM;
        private static readonly Bitmap SEM_grey = Properties.Resources.SEM_modified;
        private static readonly Bitmap GN1 = Properties.Resources.GN1;
        private static readonly Bitmap GN1_grey = Properties.Resources.GN1_modified;
        private static readonly Bitmap GN2 = Properties.Resources.GN2;
        private static readonly Bitmap GN2_grey = Properties.Resources.GN2_modified;
        private static readonly Bitmap GN3 = Properties.Resources.GN3;
        private static readonly Bitmap GN3_grey = Properties.Resources.GN3_modified;
        private static readonly Bitmap GN4 = Properties.Resources.GN4;
        private static readonly Bitmap GN4_grey = Properties.Resources.GN4_modified;
        private static readonly Bitmap MG1 = Properties.Resources.MG1;
        private static readonly Bitmap MG1_grey = Properties.Resources.MG1_modified;
        private static readonly Bitmap MG2 = Properties.Resources.MG2;
        private static readonly Bitmap MG2_grey = Properties.Resources.MG2_modified;
        private static readonly Bitmap MGE = Properties.Resources.MGE;
        private static readonly Bitmap MGE_grey = Properties.Resources.MGE_modified;
        private static readonly Bitmap DMG = Properties.Resources.DMG;
        private static readonly Bitmap DMG_grey = Properties.Resources.DMG_modified;
        private static readonly Bitmap LE = Properties.Resources.LE;
        private static readonly Bitmap LE_grey = Properties.Resources.LE_modified;
        private static readonly Bitmap LEM = Properties.Resources.LEM;
        private static readonly Bitmap LEM_grey = Properties.Resources.LEM_modified;
        private static readonly Bitmap SMFC = Properties.Resources.SMFC;
        private static readonly Bitmap SMFC_grey = Properties.Resources.SMFC_modified;
        private static readonly Bitmap GE = Properties.Resources.GE;
        private static readonly Bitmap GE_grey = Properties.Resources.GE_modified;
        private static readonly Bitmap Red = Properties.Resources.Red;
        private static readonly Bitmap Green = Properties.Resources.Green;
        private static readonly Bitmap Black = Properties.Resources.Black;
        private static readonly Bitmap Star = Properties.Resources.Star;

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
                {18, GE},
                {19, S1_grey},
                {20, S2_grey},
                {21, S3_grey},
                {22, S4_grey},
                {23, SE_grey},
                {24, SEM_grey},
                {25, GN1_grey},
                {26, GN2_grey},
                {27, GN3_grey},
                {28, GN4_grey},
                {29, MG1_grey},
                {30, MG2_grey},
                {31, MGE_grey},
                {32, DMG_grey},
                {33, LE_grey},
                {34, LEM_grey},
                {35, SMFC_grey},
                {36, GE_grey}
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
                {GE.GetHashCode(), 18},
                {S1_grey.GetHashCode(), 19},
                {S2_grey.GetHashCode(), 20},
                {S3_grey.GetHashCode(), 21},
                {S4_grey.GetHashCode(), 22},
                {SE_grey.GetHashCode(), 23},
                {SEM_grey.GetHashCode(), 24},
                {GN1_grey.GetHashCode(), 25},
                {GN2_grey.GetHashCode(), 26},
                {GN3_grey.GetHashCode(), 27},
                {GN4_grey.GetHashCode(), 28},
                {MG1_grey.GetHashCode(), 29},
                {MG2_grey.GetHashCode(), 30},
                {MGE_grey.GetHashCode(), 31},
                {DMG_grey.GetHashCode(), 32},
                {LE_grey.GetHashCode(), 33},
                {LEM_grey.GetHashCode(), 34},
                {SMFC_grey.GetHashCode(), 35},
                {GE_grey.GetHashCode(), 36},
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

        public static int RankToGreyRank(int rank) {
            if (rank == 0) return 0;
            // find grey rank
            if (rank <= 18) return rank + 18;
            else return rank - 18;
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
