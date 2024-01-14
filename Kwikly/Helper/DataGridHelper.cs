using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kwikly {
    class DataGridHelper {

        public static void LoadLayout(DataGridView dg) {
            dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg.Columns["Login"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dg.Columns["Login"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dg.Columns["Level"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
        public static async Task MouseDoubleClickEventAsync(DataGridViewCellMouseEventArgs e, DataGridView dataGrid) {
            switch (e.Button) {
                case MouseButtons.Left:
                if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Nr"]) ||
                    e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Login"]) ||
                    e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Name"])) {
                    SteamHelper.SetLoginName(dataGrid.Rows[e.RowIndex].Cells["Login"].Value.ToString());
                    SteamHelper.Restart();
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Level"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Level"].Selected = true;

                    int currentLevel = (int)dataGrid.CurrentCell.Value;
                    int level = currentLevel == 39 ? 1 : currentLevel + 1;

                    dataGrid.CurrentCell.Value = level;
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Drop"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Drop"].Selected = true;

                    int dropState = VisualHelper.BitmapToDrop((dataGrid.CurrentCell.Value as Bitmap).GetHashCode()) == 0 ? 1 : 0;

                    dataGrid.CurrentCell.Value = VisualHelper.DropToBitmap(dropState);
                    break;
                }

                break;

                case MouseButtons.Right:
                if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Rank"]))
                {
                    string steamId64 = dataGrid.Rows[e.RowIndex].Cells["SteamId64"].Value.ToString();
                    string rank = await SteamHelper.GetRankBySteamID64Async(long.Parse(steamId64));
                    dataGrid.CurrentCell.Value = rank;
                    VisualHelper.UpdateRankColor(rank, dataGrid.CurrentCell);
                    break;
                }

                else if (e.ColumnIndex == dataGrid.Columns.IndexOf(dataGrid.Columns["Level"])) {
                    dataGrid.Rows[e.RowIndex].Cells["Level"].Selected = true;

                    int currentLevel = (int)dataGrid.CurrentCell.Value;
                    int level = currentLevel == 1 ? 39 : currentLevel - 1;

                    dataGrid.CurrentCell.Value = level;
                    break;
                }

                break;
            }
        }
    }
}
