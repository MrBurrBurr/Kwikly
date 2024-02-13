using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kwikly {
    class ContextHelper {
        public static void DeleteEvent(DataGridView dataGrid) {
            int rowToDelete = dataGrid.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            dataGrid.Rows.RemoveAt(rowToDelete);
        }

        public static void RefreshNameEvent(DataGridView dataGrid) {
            dataGrid.CurrentCell.Value = SteamHelper.GetNameBySteamID64((long)dataGrid.Rows[dataGrid.CurrentCell.RowIndex].Cells["SteamID64"].Value);
        }

        public static async Task RefreshRankEventAsync(DataGridView dataGrid)
        {
            var cell = dataGrid.CurrentCell;
            string steamId64 = dataGrid.Rows[cell.RowIndex].Cells["SteamId64"].Value.ToString();
            string rank = await SteamHelper.GetRankBySteamID64Async(long.Parse(steamId64));
            cell.Value = rank;
            VisualHelper.UpdateRankColor(rank, cell);
        }

        public static void UpDownEvent(DataGridView dataGrid, bool up) {
            string selectedNr = dataGrid.CurrentCell.Value.ToString();
            int rowIndex = dataGrid.CurrentCell.RowIndex;
            rowIndex = up ? rowIndex - 1 : rowIndex + 1;

            string desiredNr = dataGrid.Rows[rowIndex].Cells["Nr"].Value.ToString();

            dataGrid.CurrentCell.Value = desiredNr;
            dataGrid.Rows[rowIndex].Cells["Nr"].Value = selectedNr;
        }
    }
}
