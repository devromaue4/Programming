namespace ExitGames.Client.DemoParticle
{
    partial class ParticleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelLobby = new System.Windows.Forms.Panel();
            this.listBoxLobby = new System.Windows.Forms.ListBox();
            this.btnJoinSelected = new System.Windows.Forms.Button();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.labelPlayerInfo = new System.Windows.Forms.ToolStripLabel();
            this.panelStats = new System.Windows.Forms.Panel();
            this.textBoxStats = new System.Windows.Forms.TextBox();
            this.splitterMain = new System.Windows.Forms.Splitter();
            this.toolStripStats = new System.Windows.Forms.ToolStrip();
            this.btnStatsToLog = new System.Windows.Forms.ToolStripButton();
            this.btnStatisticsReset = new System.Windows.Forms.ToolStripButton();
            this.btnAutoMove = new System.Windows.Forms.ToolStripButton();
            this.btnUseGroups = new System.Windows.Forms.ToolStripButton();
            this.btnGridSize = new System.Windows.Forms.ToolStripButton();
            this.btnChangeColor = new System.Windows.Forms.ToolStripButton();
            this.btnToLobby = new System.Windows.Forms.ToolStripButton();
            this.btnInfoButtons = new System.Windows.Forms.ToolStripDropDownButton();
            this.miUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.miStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panelGame = new ExitGames.Client.DemoParticle.ParticleGamePanel();
            this.panelMain.SuspendLayout();
            this.panelLobby.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.toolStripStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelGame);
            this.panelMain.Controls.Add(this.panelLobby);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(131, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(235, 288);
            this.panelMain.TabIndex = 2;
            // 
            // panelLobby
            // 
            this.panelLobby.Controls.Add(this.listBoxLobby);
            this.panelLobby.Controls.Add(this.btnJoinSelected);
            this.panelLobby.Controls.Add(this.btnCreateRoom);
            this.panelLobby.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLobby.Location = new System.Drawing.Point(0, 0);
            this.panelLobby.Name = "panelLobby";
            this.panelLobby.Size = new System.Drawing.Size(235, 288);
            this.panelLobby.TabIndex = 2;
            // 
            // listBoxLobby
            // 
            this.listBoxLobby.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLobby.FormattingEnabled = true;
            this.listBoxLobby.Items.AddRange(new object[] {
            "some",
            "text"});
            this.listBoxLobby.Location = new System.Drawing.Point(0, 0);
            this.listBoxLobby.Name = "listBoxLobby";
            this.listBoxLobby.Size = new System.Drawing.Size(235, 242);
            this.listBoxLobby.TabIndex = 2;
            // 
            // btnJoinSelected
            // 
            this.btnJoinSelected.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnJoinSelected.Location = new System.Drawing.Point(0, 242);
            this.btnJoinSelected.Name = "btnJoinSelected";
            this.btnJoinSelected.Size = new System.Drawing.Size(235, 23);
            this.btnJoinSelected.TabIndex = 4;
            this.btnJoinSelected.Text = "Join Selected";
            this.btnJoinSelected.UseVisualStyleBackColor = true;
            this.btnJoinSelected.Click += new System.EventHandler(this.OnClickJoinSelectedRoom);
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCreateRoom.Location = new System.Drawing.Point(0, 265);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(235, 23);
            this.btnCreateRoom.TabIndex = 3;
            this.btnCreateRoom.Text = "Create Room";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.OnClickCreateRoom);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAutoMove,
            this.btnUseGroups,
            this.btnGridSize,
            this.btnChangeColor,
            this.btnToLobby,
            this.toolStripSeparator1,
            this.btnInfoButtons,
            this.labelPlayerInfo});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(366, 25);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // labelPlayerInfo
            // 
            this.labelPlayerInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.labelPlayerInfo.Name = "labelPlayerInfo";
            this.labelPlayerInfo.Size = new System.Drawing.Size(63, 22);
            this.labelPlayerInfo.Text = "player info";
            // 
            // panelStats
            // 
            this.panelStats.Controls.Add(this.splitter1);
            this.panelStats.Controls.Add(this.textBoxStats);
            this.panelStats.Controls.Add(this.toolStripStats);
            this.panelStats.Controls.Add(this.textBox1);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStats.Location = new System.Drawing.Point(0, 25);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(131, 288);
            this.panelStats.TabIndex = 3;
            this.panelStats.Visible = false;
            // 
            // textBoxStats
            // 
            this.textBoxStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxStats.Location = new System.Drawing.Point(0, 25);
            this.textBoxStats.Multiline = true;
            this.textBoxStats.Name = "textBoxStats";
            this.textBoxStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStats.Size = new System.Drawing.Size(131, 159);
            this.textBoxStats.TabIndex = 0;
            // 
            // splitterMain
            // 
            this.splitterMain.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitterMain.Location = new System.Drawing.Point(131, 25);
            this.splitterMain.Name = "splitterMain";
            this.splitterMain.Size = new System.Drawing.Size(3, 288);
            this.splitterMain.TabIndex = 4;
            this.splitterMain.TabStop = false;
            this.splitterMain.Visible = false;
            // 
            // toolStripStats
            // 
            this.toolStripStats.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.btnStatisticsReset,
            this.btnStatsToLog});
            this.toolStripStats.Location = new System.Drawing.Point(0, 0);
            this.toolStripStats.Name = "toolStripStats";
            this.toolStripStats.Size = new System.Drawing.Size(131, 25);
            this.toolStripStats.TabIndex = 1;
            this.toolStripStats.Text = "toolStrip1";
            // 
            // btnStatsToLog
            // 
            this.btnStatsToLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStatsToLog.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.Pen;
            this.btnStatsToLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStatsToLog.Name = "btnStatsToLog";
            this.btnStatsToLog.Size = new System.Drawing.Size(23, 22);
            this.btnStatsToLog.ToolTipText = "Write stats to log";
            this.btnStatsToLog.Click += new System.EventHandler(this.OnClickCurrentStatisticsToLog);
            // 
            // btnStatisticsReset
            // 
            this.btnStatisticsReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStatisticsReset.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.Backward;
            this.btnStatisticsReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStatisticsReset.Name = "btnStatisticsReset";
            this.btnStatisticsReset.Size = new System.Drawing.Size(23, 22);
            this.btnStatisticsReset.ToolTipText = "Reset stats";
            this.btnStatisticsReset.Click += new System.EventHandler(this.OnClickStatisticsReset);
            // 
            // btnAutoMove
            // 
            this.btnAutoMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAutoMove.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.CircledBorderTriangleRight;
            this.btnAutoMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoMove.Name = "btnAutoMove";
            this.btnAutoMove.Size = new System.Drawing.Size(23, 22);
            this.btnAutoMove.ToolTipText = "Automove local player";
            this.btnAutoMove.Click += new System.EventHandler(this.OnClickAutoMove);
            // 
            // btnUseGroups
            // 
            this.btnUseGroups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUseGroups.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.Grid3;
            this.btnUseGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUseGroups.Name = "btnUseGroups";
            this.btnUseGroups.Size = new System.Drawing.Size(23, 22);
            this.btnUseGroups.ToolTipText = "Toggle usage of Interest Groups";
            this.btnUseGroups.Click += new System.EventHandler(this.OnClickInterestGroup);
            // 
            // btnGridSize
            // 
            this.btnGridSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGridSize.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.ArrowUpDown;
            this.btnGridSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGridSize.Name = "btnGridSize";
            this.btnGridSize.Size = new System.Drawing.Size(23, 22);
            this.btnGridSize.ToolTipText = "Change grid size";
            this.btnGridSize.Click += new System.EventHandler(this.OnClickGridSize);
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChangeColor.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.CircledSync;
            this.btnChangeColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(23, 22);
            this.btnChangeColor.ToolTipText = "New random color";
            this.btnChangeColor.Click += new System.EventHandler(this.OnClickChangeColor);
            // 
            // btnToLobby
            // 
            this.btnToLobby.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToLobby.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.CircledList;
            this.btnToLobby.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToLobby.Name = "btnToLobby";
            this.btnToLobby.Size = new System.Drawing.Size(23, 22);
            this.btnToLobby.ToolTipText = "Leave to lobby";
            this.btnToLobby.Click += new System.EventHandler(this.OnClickToLobby);
            // 
            // btnInfoButtons
            // 
            this.btnInfoButtons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInfoButtons.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUserInfo,
            this.miStatistics});
            this.btnInfoButtons.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.LightBulb;
            this.btnInfoButtons.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInfoButtons.Name = "btnInfoButtons";
            this.btnInfoButtons.Size = new System.Drawing.Size(29, 22);
            this.btnInfoButtons.ToolTipText = "Toggle info visibility";
            // 
            // miUserInfo
            // 
            this.miUserInfo.Name = "miUserInfo";
            this.miUserInfo.Size = new System.Drawing.Size(153, 22);
            this.miUserInfo.Text = "Show User Info";
            this.miUserInfo.Click += new System.EventHandler(this.OnClickUserInfo);
            // 
            // miStatistics
            // 
            this.miStatistics.Name = "miStatistics";
            this.miStatistics.Size = new System.Drawing.Size(153, 22);
            this.miStatistics.Text = "Show Statistics";
            this.miStatistics.Click += new System.EventHandler(this.OnClickStats);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 184);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(131, 104);
            this.textBox1.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 181);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(131, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.CircledPause;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.OnClickStatisticsToggle);
            // 
            // panelGame
            // 
            this.panelGame.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGame.Location = new System.Drawing.Point(0, 0);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(235, 288);
            this.panelGame.TabIndex = 2;
            this.panelGame.Paint += new System.Windows.Forms.PaintEventHandler(this.OnDraw);
            // 
            // ParticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 313);
            this.Controls.Add(this.splitterMain);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.toolStripMain);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "ParticleForm";
            this.Text = "ParticleForm";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyPress);
            this.panelMain.ResumeLayout(false);
            this.panelLobby.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.toolStripStats.ResumeLayout(false);
            this.toolStripStats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelLobby;
        private System.Windows.Forms.ListBox listBoxLobby;
        private System.Windows.Forms.Button btnCreateRoom;
        private ParticleGamePanel panelGame;
        private System.Windows.Forms.Button btnJoinSelected;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnAutoMove;
        private System.Windows.Forms.ToolStripButton btnUseGroups;
        private System.Windows.Forms.ToolStripButton btnGridSize;
        private System.Windows.Forms.ToolStripButton btnChangeColor;
        private System.Windows.Forms.ToolStripButton btnToLobby;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton btnInfoButtons;
        private System.Windows.Forms.ToolStripMenuItem miUserInfo;
        private System.Windows.Forms.ToolStripMenuItem miStatistics;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.TextBox textBoxStats;
        private System.Windows.Forms.ToolStripLabel labelPlayerInfo;
        private System.Windows.Forms.Splitter splitterMain;
        private System.Windows.Forms.ToolStrip toolStripStats;
        private System.Windows.Forms.ToolStripButton btnStatsToLog;
        private System.Windows.Forms.ToolStripButton btnStatisticsReset;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

