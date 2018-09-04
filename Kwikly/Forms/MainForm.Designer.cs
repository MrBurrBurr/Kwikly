namespace Kwikly
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkForNewAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.SwitchConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.TrayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.AllowUserToResizeColumns = false;
            this.DataGrid.AllowUserToResizeRows = false;
            this.DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGrid.Location = new System.Drawing.Point(0, 0);
            this.DataGrid.MultiSelect = false;
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.RowHeadersVisible = false;
            this.DataGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid.Size = new System.Drawing.Size(407, 56);
            this.DataGrid.TabIndex = 0;
            this.DataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseClick);
            this.DataGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseDoubleClick);
            this.DataGrid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseDown);
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayMenuStrip;
            this.TrayIcon.Text = "Kwikly";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
            // 
            // TrayMenuStrip
            // 
            this.TrayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForNewAccountsToolStripMenuItem,
            this.CheckForUpdatesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.SwitchConfigToolStripMenuItem,
            this.ReloadConfigToolStripMenuItem,
            this.toolStripMenuItem2,
            this.MenuItemExit});
            this.TrayMenuStrip.Name = "MenuStrip";
            this.TrayMenuStrip.Size = new System.Drawing.Size(202, 126);
            // 
            // checkForNewAccountsToolStripMenuItem
            // 
            this.checkForNewAccountsToolStripMenuItem.Image = global::Kwikly.Properties.Resources.Account;
            this.checkForNewAccountsToolStripMenuItem.Name = "checkForNewAccountsToolStripMenuItem";
            this.checkForNewAccountsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.checkForNewAccountsToolStripMenuItem.Text = "Check for new accounts";
            this.checkForNewAccountsToolStripMenuItem.Click += new System.EventHandler(this.CheckForNewAccountsToolStripMenuItem_Click);
            // 
            // CheckForUpdatesToolStripMenuItem
            // 
            this.CheckForUpdatesToolStripMenuItem.Image = global::Kwikly.Properties.Resources.Update;
            this.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem";
            this.CheckForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.CheckForUpdatesToolStripMenuItem.Text = "Check for updates";
            this.CheckForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 6);
            // 
            // SwitchConfigToolStripMenuItem
            // 
            this.SwitchConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.botsToolStripMenuItem});
            this.SwitchConfigToolStripMenuItem.Image = global::Kwikly.Properties.Resources.Switch;
            this.SwitchConfigToolStripMenuItem.Name = "SwitchConfigToolStripMenuItem";
            this.SwitchConfigToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.SwitchConfigToolStripMenuItem.Text = "Switch config";
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Checked = true;
            this.defaultToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultToolStripMenuItem.Image = global::Kwikly.Properties.Resources.Default;
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.DefaultToolStripMenuItem_Click);
            // 
            // botsToolStripMenuItem
            // 
            this.botsToolStripMenuItem.Image = global::Kwikly.Properties.Resources.Bot;
            this.botsToolStripMenuItem.Name = "botsToolStripMenuItem";
            this.botsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.botsToolStripMenuItem.Text = "Bots";
            this.botsToolStripMenuItem.Click += new System.EventHandler(this.BotsToolStripMenuItem_Click);
            // 
            // ReloadConfigToolStripMenuItem
            // 
            this.ReloadConfigToolStripMenuItem.Image = global::Kwikly.Properties.Resources.Refresh;
            this.ReloadConfigToolStripMenuItem.Name = "ReloadConfigToolStripMenuItem";
            this.ReloadConfigToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.ReloadConfigToolStripMenuItem.Text = "Reload config";
            this.ReloadConfigToolStripMenuItem.Click += new System.EventHandler(this.ReloadConfigToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.Image = global::Kwikly.Properties.Resources.Exit;
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.Size = new System.Drawing.Size(201, 22);
            this.MenuItemExit.Text = "Exit";
            this.MenuItemExit.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(407, 56);
            this.Controls.Add(this.DataGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kwikly";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.TrayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip TrayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem CheckForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ReloadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForNewAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SwitchConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem botsToolStripMenuItem;
    }
}

