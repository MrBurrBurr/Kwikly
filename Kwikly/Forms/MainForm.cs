using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kwikly {
    public partial class MainForm : Form {
        
        private Version newVersion = null;
        private Version currentVersion = null;
        private string updateUrl = null;
        private bool startupUpdateCheck = true;
        private Uri updateDownloadURL = new Uri("https://github.com/MrBurrBurr/Kwikly/releases/latest");

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            try {
                if (!Config.DoesExists()) {
                    ProgressBar pBar = VisualHelper.CreateNewProgressBar(this, "Creating config...");
                    Config.Create(pBar);
                    pBar.Dispose();
                }
                LoadDataGrid();
                LoadLayout();
                CheckDropDate();
                VisualizeInactiveAccounts();
                VisualizeLoggedInAccount();
                VisualizeRanks();
                CheckForUpdates();
                CleanUpAfterUpdate();
            }
            catch (Exception ex) {
                MessageBox.Show("Error showing Kwikly: " + ex.Message);
            }
        }

        private void LoadDataGrid() {
            try {
                DataGrid.DataSource = DataSetHelper.NormalizeDataSet(Config.Load());
                DataGrid.DataMember = "Account";
            }
            catch (Exception ex) {
                DialogResult result;
                result = MessageBox.Show(this, "Error loading xml: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (result == DialogResult.OK) {
                    TrayIcon.Dispose();
                    FormClosing -= MainForm_FormClosing;
                    Application.Exit();
                    return;
                }
            }
        }

        private void LoadLayout() {
            try {
                DataGridHelper.LoadLayout(DataGrid);
                TrayIcon.Icon = Icon;
                Text = Application.ProductName + " v" + Application.ProductVersion;
                Height = DataGrid.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 62;
                CenterToScreen();
                DataGrid.Sort(DataGrid.Columns["Nr"], ListSortDirection.Ascending);
            }
            catch (Exception ex) {
                MessageBox.Show("Error loading layout: " + ex.Message);
            }
        }

        private void CheckDropDate() {
            try {
                DataSetHelper.CheckDropDate((DataSet)DataGrid.DataSource);
            }
            catch (Exception ex) {
                MessageBox.Show("Error checking drop date: " + ex.Message);
            }
        }

        private void VisualizeLoggedInAccount() {
            try {
                VisualHelper.VisualizeLoggedInAccount(DataGrid);
            }
            catch (Exception ex) {
                MessageBox.Show("Error visualizing logged in account: " + ex.Message);
            }
        }

        private void VisualizeRanks() {
            try
            {
                VisualHelper.VisualizeRanks(DataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error visualizing ranks: " + ex.Message);
            }
        }

        private void VisualizeInactiveAccounts() {
            try {
                VisualHelper.VisualizeInactiveAccounts(DataGrid);
            }
            catch (Exception ex) {
                MessageBox.Show("Error visualizing inactive accounts: " + ex.Message);
            }
        }

        private void CheckForUpdates() {
            try {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += (s, e) => {
                    Match matchVersion = Regex.Match(e.Result, @"css-truncate-target\"">(.*)<\/", RegexOptions.IgnoreCase);
                    if (matchVersion.Success) {
                        newVersion = new Version(matchVersion.Groups[1].Value);
                        currentVersion = new Version(Application.ProductVersion);

                        Match matchDownload = Regex.Match(e.Result, @"release-downloads"">\n\s*<li>\n\s*<a href=""(.*).exe""", RegexOptions.IgnoreCase);
                        if (matchDownload.Success) {
                            updateUrl = "https://github.com" + matchDownload.Groups[1].Value.ToString() + ".exe";
                            CompareVersions();
                        }
                    }
                };
                client.DownloadStringAsync(updateDownloadURL);
                client.Dispose();
            }
            catch (Exception ex) {
                MessageBox.Show("Error checking for updates: " + ex.Message);
            }
        }

        private void CleanUpAfterUpdate() {
            try {
                UpdateHelper.CleanUpAfterUpdate();
            }
            catch (Exception ex) {
                MessageBox.Show("Error cleaning up: " + ex.Message);
            }
        }

        private void CompareVersions() {
            if (newVersion > currentVersion) {
                DialogResult updateDialog = MessageBox.Show(string.Format("A new version is available! Would you like to update now?\nYou will update from version {0} to {1}", Application.ProductVersion, newVersion.ToString()), "New Version", MessageBoxButtons.YesNo);
                if (updateDialog == DialogResult.Yes) {
                    ProgressBar pBar = VisualHelper.CreateNewProgressBar(this, "Updating...");
                    UpdateHelper.DownloadFileToTempPath(pBar, updateUrl);
                }
            }
            else if (!startupUpdateCheck) {
                if (newVersion < currentVersion) {
                    MessageBox.Show(string.Format("You are using the beta version: {0}", Application.ProductVersion));
                }
                else {
                    MessageBox.Show(string.Format("You are using the latest version: {0}", Application.ProductVersion));
                }
            }

            newVersion = null;
            startupUpdateCheck = false;
        }

        private ContextMenuStrip GetColumnContext(DataGridView dg) {
            ContextMenuStrip context = new ContextMenuStrip();
            context.Closed += (s, e) => {
                dg.ContextMenuStrip = null;
            };

            foreach (DataGridViewColumn column in dg.Columns) {
                ToolStripMenuItem contextMenuItem = new ToolStripMenuItem(column.Name);
                contextMenuItem.Checked = column.Visible;
                contextMenuItem.Click += (s, e) => {
                    var localColumn = column;
                    System.Diagnostics.Debug.WriteLine("width=" + Width);
                    contextMenuItem.Checked = !contextMenuItem.Checked;
                    localColumn.Visible = contextMenuItem.Checked;

                    if (localColumn.Visible) {
                        Width = Width + localColumn.Width;
                    }
                    else {
                        Width = Width - localColumn.Width;
                    }
                };
                context.Items.Add(contextMenuItem);
            }

            return context;
        }

        private ContextMenuStrip GetRightClickContext(DataGridView dg) {
            ContextMenuStrip context = new ContextMenuStrip();
            context.Closed += (s, e) => {
                dg.ContextMenuStrip = null;
            };

            if (dg.CurrentCell.ColumnIndex == dg.Columns.IndexOf(dg.Columns["Nr"])) {
                ToolStripMenuItem UpToolStripMenuItem = new ToolStripMenuItem("Up", Properties.Resources.Up);
                UpToolStripMenuItem.Name = "UpToolStripMenuItem";
                UpToolStripMenuItem.Enabled = dg.CurrentCell.RowIndex == 0 ? false : true;
                UpToolStripMenuItem.Click += new EventHandler(UpToolStripMenuItem_Click);
                context.Items.Add(UpToolStripMenuItem);

                ToolStripMenuItem DownToolStripMenuItem = new ToolStripMenuItem("Down", Properties.Resources.Down);
                DownToolStripMenuItem.Name = "DownToolStripMenuItem";
                DownToolStripMenuItem.Enabled = dg.CurrentCell.RowIndex == dg.RowCount - 1 ? false : true;
                DownToolStripMenuItem.Click += new EventHandler(DownToolStripMenuItem_Click);
                context.Items.Add(DownToolStripMenuItem);
            }
            else if (dg.CurrentCell.ColumnIndex == dg.Columns.IndexOf(dg.Columns["Login"])) {
                ToolStripMenuItem DeleteToolStripMenuItem = new ToolStripMenuItem("Delete", Properties.Resources.Delete);
                DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
                DeleteToolStripMenuItem.Click += new EventHandler(DeleteToolStripMenuItem_Click);
                context.Items.Add(DeleteToolStripMenuItem);
            }
            else if (dg.CurrentCell.ColumnIndex == dg.Columns.IndexOf(dg.Columns["Name"])) {
                ToolStripMenuItem RefreshNameToolStripMenuItem = new ToolStripMenuItem("Refresh", Properties.Resources.Refresh);
                RefreshNameToolStripMenuItem.Name = "RefreshNameToolStripMenuItem";
                RefreshNameToolStripMenuItem.Click += new EventHandler(RefreshNameToolStripMenuItem_Click);
                context.Items.Add(RefreshNameToolStripMenuItem);
            }
            else if (dg.CurrentCell.ColumnIndex == dg.Columns.IndexOf(dg.Columns["Rank"]))
            {
                ToolStripMenuItem RefreshRankToolStripMenuItem = new ToolStripMenuItem("Refresh", Properties.Resources.Refresh);
                RefreshRankToolStripMenuItem.Name = "RefreshRankToolStripMenuItem";
                RefreshRankToolStripMenuItem.Click += new EventHandler(RefreshRankToolStripMenuItem_Click);
                context.Items.Add(RefreshRankToolStripMenuItem);
            }

            return context;
        }

        private void DataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Right:
                    try {
                        if (e.RowIndex == -1) {
                            DataGrid.ContextMenuStrip = GetColumnContext(DataGrid);
                        }
                        else {
                            DataGrid.ContextMenuStrip = GetRightClickContext(DataGrid);
                        }

                        DataGrid.ContextMenuStrip.Show(new Point(MousePosition.X, MousePosition.Y));
                    }
                    catch (Exception ex) {
                        MessageBox.Show("Error showing context menu: " + ex.Message);
                    }
                    break;
            }
        }

        private void DataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                DataGridHelper.MouseDoubleClickEvent(e, DataGrid);

                if (e.Button == MouseButtons.Left &&
                    e.ColumnIndex == DataGrid.Columns.IndexOf(DataGrid.Columns["Nr"]) ||
                    e.ColumnIndex == DataGrid.Columns.IndexOf(DataGrid.Columns["Login"]) ||
                    e.ColumnIndex == DataGrid.Columns.IndexOf(DataGrid.Columns["Name"])) {
                    VisualHelper.VisualizeLoggedInAccount(DataGrid);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error changing value while double clicking: " + ex.Message);
            }
        }

        private void DataGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                DataGridHelper.MouseDownEvent(e, DataGrid);
            }
            catch (Exception ex) {
                MessageBox.Show("Error selecting cell for context menu: " + ex.Message);
            }
        }

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            try {
                if (e.Button == MouseButtons.Right)
                    return;

                if (WindowState == FormWindowState.Minimized) {
                    Show();
                    WindowState = FormWindowState.Normal;
                    return;
                }

                Hide();
                WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex) {
                MessageBox.Show("Error while clicking tray icon: " + ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                DataSetHelper.NormalizeForSaving((DataSet)DataGrid.DataSource);
                TrayIcon.Dispose();
            }
            catch (Exception ex) {
                MessageBox.Show("Error minimizing Kwikly: " + ex.Message);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Application.Exit();
            }
            catch (Exception ex) {
                MessageBox.Show("Error exiting Kwikly: " + ex.Message);
            }
        }

        private void UpToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                ContextHelper.UpDownEvent(DataGrid, true);
                LoadLayout();
            }
            catch (Exception ex) {
                MessageBox.Show("Error while moving account up: " + ex.Message);
            }
        }

        private void DownToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                ContextHelper.UpDownEvent(DataGrid, false);
                LoadLayout();
            }
            catch (Exception ex) {
                MessageBox.Show("Error while moving account down: " + ex.Message);
            }
        }

        private void RefreshNameToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                ContextHelper.RefreshNameEvent(DataGrid);
            }
            catch (Exception ex) {
                MessageBox.Show("Error while refreshing name: " + ex.Message);
            }
        }

        private void RefreshRankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ContextHelper.RefreshRankEventAsync(DataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while refreshing rank: " + ex.Message);
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                ContextHelper.DeleteEvent(DataGrid);
                DataSetHelper.CorrectAccountNumbers((DataSet)DataGrid.DataSource);
                DataSetHelper.NormalizeForSaving((DataSet)DataGrid.DataSource);
                LoadLayout();
                VisualHelper.VisualizeLoggedInAccount(DataGrid);
            }
            catch (Exception ex) {
                MessageBox.Show("Error while deleting account: " + ex.Message);
            }
        }

        private void ReloadConfigToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                DialogResult reloadConfig = MessageBox.Show("Are you sure you want to reload the config?\nSettings like level, rank and drop will be lost.", "Reload config", MessageBoxButtons.YesNo);
                if (reloadConfig == DialogResult.Yes) {
                    ProgressBar pBar = VisualHelper.CreateNewProgressBar(this, "Reloading config...");
                    Config.Create(pBar);
                    pBar.Dispose();
                    LoadDataGrid();
                    LoadLayout();
                    DataSetHelper.CheckDropDate((DataSet)DataGrid.DataSource);
                    VisualizeInactiveAccounts();
                    VisualizeLoggedInAccount();
                    VisualizeRanks();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error reloading config: " + ex.Message);
            }
        }

        private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                if (newVersion == null || currentVersion == null)  CheckForUpdates();
                else CompareVersions();
            }
            catch (Exception ex) {
                MessageBox.Show("Error checking for updates: " + ex.Message);
            }
        }

        private void CheckForNewAccountsToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                ProgressBar pBar = VisualHelper.CreateNewProgressBar(this, "Looking for new accounts...");
                int newAccounts = Config.CheckForNewAccounts((DataSet)DataGrid.DataSource, pBar);
                pBar.Dispose();
                LoadDataGrid();
                LoadLayout();
                VisualizeInactiveAccounts();
                VisualizeLoggedInAccount();
                VisualizeRanks();

                MessageBox.Show(string.Format("{0} account(s) added", newAccounts));
            }
            catch (Exception ex) {
                MessageBox.Show("Error checking for new accounts: " + ex.Message);
            }
        }

        private void BotsToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                ChangeConfig(@"\Bots.Kwikly");
            }
            catch (Exception ex) {
                MessageBox.Show("Error switching to bot config: " + ex.Message);
            }
        }

        private void DefaultToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                ChangeConfig(@"\Default.Kwikly");
            }
            catch (Exception ex) {
                MessageBox.Show("Error switching to default config: " + ex.Message);
            }
        }

        private void ChangeConfig(string cfg) {
            defaultToolStripMenuItem.Checked = !defaultToolStripMenuItem.Checked;
            botsToolStripMenuItem.Checked = !botsToolStripMenuItem.Checked;

            DataSetHelper.NormalizeForSaving((DataSet)DataGrid.DataSource);

            Config.kwiklyFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + cfg;

            LoadDataGrid();
            LoadLayout();
            VisualizeLoggedInAccount();
            VisualizeRanks();
        }
    }
}