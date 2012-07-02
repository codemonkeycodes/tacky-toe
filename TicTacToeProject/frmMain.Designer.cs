namespace TicTacToeProject
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoPlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playersToolStripMenuItem,
            this.networkToolStripMenuItem,
            this.statsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(713, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onePlayerToolStripMenuItem,
            this.twoPlayersToolStripMenuItem});
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.playersToolStripMenuItem.Text = "Players";
            // 
            // onePlayerToolStripMenuItem
            // 
            this.onePlayerToolStripMenuItem.Name = "onePlayerToolStripMenuItem";
            this.onePlayerToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.onePlayerToolStripMenuItem.Text = "One player";
            this.onePlayerToolStripMenuItem.Click += new System.EventHandler(this.onePlayerToolStripMenuItem_Click);
            // 
            // twoPlayersToolStripMenuItem
            // 
            this.twoPlayersToolStripMenuItem.Name = "twoPlayersToolStripMenuItem";
            this.twoPlayersToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.twoPlayersToolStripMenuItem.Text = "Two players";
            this.twoPlayersToolStripMenuItem.Click += new System.EventHandler(this.twoPlayersToolStripMenuItem_Click);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.connectToServerToolStripMenuItem});
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            this.networkToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.networkToolStripMenuItem.Text = "Network";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.startServerToolStripMenuItem.Text = "Start server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // connectToServerToolStripMenuItem
            // 
            this.connectToServerToolStripMenuItem.Name = "connectToServerToolStripMenuItem";
            this.connectToServerToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.connectToServerToolStripMenuItem.Text = "Connect to server";
            this.connectToServerToolStripMenuItem.Click += new System.EventHandler(this.connectToServerToolStripMenuItem_Click);
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewStatisticsToolStripMenuItem});
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.statsToolStripMenuItem.Text = "Stats";
            // 
            // viewStatisticsToolStripMenuItem
            // 
            this.viewStatisticsToolStripMenuItem.Name = "viewStatisticsToolStripMenuItem";
            this.viewStatisticsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewStatisticsToolStripMenuItem.Text = "View Statistics";
            this.viewStatisticsToolStripMenuItem.Click += new System.EventHandler(this.viewStatisticsToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 408);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Tic Tac Toe";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem playersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onePlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoPlayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewStatisticsToolStripMenuItem;

    }
}