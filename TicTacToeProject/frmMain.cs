using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToeProject
{
    public partial class frmMain : Form
    {
        List<Player> player_history = null;
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show Player1
            frmTCPSocketServer server = new frmTCPSocketServer();
            server.MdiParent = this;
            server.WindowState = FormWindowState.Maximized;
            server.Show();
        }

        /// <summary>
        /// Connect to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTCPSocketClient client = new frmTCPSocketClient();
            client.MdiParent = this;
            client.WindowState = FormWindowState.Maximized;
            client.Show();
        }

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //onePlayerToolStripMenuItem.Checked = true;
            //networkToolStripMenuItem.Visible = false;
            twoPlayersToolStripMenuItem.Checked = true;
            networkToolStripMenuItem.Visible = true;
            player_history = new List<Player>();
            try
            {

            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// Play one player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Invisible network menu item
            networkToolStripMenuItem.Visible = false;
            onePlayerToolStripMenuItem.Checked = true;
            twoPlayersToolStripMenuItem.Checked = false;

            //Show frmOnePlayer
            frmOnePlayer onePlayer = new frmOnePlayer();
            onePlayer.MdiParent = this;
            onePlayer.WindowState = FormWindowState.Maximized;
            onePlayer.Show();
        }

        /// <summary>
        /// Play 2 players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Visible network menu item
            networkToolStripMenuItem.Visible = true;
            onePlayerToolStripMenuItem.Checked = false;
            twoPlayersToolStripMenuItem.Checked = true;
        }

        private void viewStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //load player & computer stats..
            //display on new dialog
        }
       
    }
}
