using System.Drawing;
using System.Windows.Forms;

namespace Kwikly {
    class DataGridHelper {

        public static void LoadLayout(DataGridView dg) {
            dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg.Columns["Login"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //todo
            //dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // this turns off auto-resize.
            // Now we just need to set sensible sizes from the start.

            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dg.Columns["Login"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["Level"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dg.Columns["SteamID32"].Visible = false;
            dg.Columns["SteamID64"].Visible = false;

            dg.ReadOnly = true;
        }

        public static void MouseDownEvent(DataGridViewCellMouseEventArgs e, DataGridView dg) {
            if (e.Button == MouseButtons.Right) {
                if (e.RowIndex != -1 && e.ColumnIndex != -1) {
                    dg.CurrentCell = dg.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                else {
                    dg.CurrentCell = null;
                }
            }
        }

        public static void MouseDoubleClickEvent(DataGridViewCellMouseEventArgs e, DataGridView dataGrid) {
            switch (e.Button) {
                case MouseButtons.Left:
                if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Nr"]) ||
                    e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Login"]) ||
                    e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Name"])) {
                    SteamHelper.SetLoginName(dataGrid.Rows[e.RowIndex].Cells["Login"].Value.ToString());
                    SteamHelper.Restart();
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Rank"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Rank"].Selected = true;

                    int rankNr = VisualHelper.BitmapToRank((dataGrid.CurrentCell.Value as Bitmap).GetHashCode());

                    if (rankNr == 18) {
                        rankNr = 0;
                    }
                    else {
                        rankNr++;
                    }

                    dataGrid.CurrentCell.Value = VisualHelper.RankToBitmap(rankNr);
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Level"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Level"].Selected = true;

                    int currentLevel = (int)dataGrid.CurrentCell.Value;
                    int level = 0;
                    level = currentLevel == 39 ? 1 : currentLevel + 1;

                    dataGrid.CurrentCell.Value = level;
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Prime"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Prime"].Selected = true;

                    int primeState = 0;
                    primeState = VisualHelper.BitmapToPrime((dataGrid.CurrentCell.Value as Bitmap).GetHashCode()) == 0 ? 1 : 0;

                    dataGrid.CurrentCell.Value = VisualHelper.PrimeToBitmap(primeState);
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Drop"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Drop"].Selected = true;

                    int dropState = 0;
                    dropState = VisualHelper.BitmapToDrop((dataGrid.CurrentCell.Value as Bitmap).GetHashCode()) == 0 ? 1 : 0;

                    dataGrid.CurrentCell.Value = VisualHelper.DropToBitmap(dropState);
                    break;
                }

                break;

                case MouseButtons.Right:
                if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Rank"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Rank"].Selected = true;

                    int rankNr = VisualHelper.BitmapToRank((dataGrid.CurrentCell.Value as Bitmap).GetHashCode());

                    if (rankNr == 0) {
                        rankNr = 18;
                    }
                    else {
                        rankNr--;
                    }

                    dataGrid.CurrentCell.Value = VisualHelper.RankToBitmap(rankNr);
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Level"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Level"].Selected = true;

                    int currentLevel = (int)dataGrid.CurrentCell.Value;
                    int level = 0;
                    level = currentLevel == 1 ? 39 : currentLevel - 1;

                    dataGrid.CurrentCell.Value = level;
                    break;
                }

                break;
            }
        }
    }
}
