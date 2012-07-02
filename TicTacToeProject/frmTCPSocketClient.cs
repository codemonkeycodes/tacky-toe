using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TicTacToeProject
{
    public partial class frmTCPSocketClient : Form
    {
        //Constant variables
        private const int PORT_NUMBER = 9999;
        private const int BUFFER_SIZE = 1024;

        //Socket and stream variables
        private TcpClient client = null;
        private Stream stream = null;
        private Socket socket = null;
        private StreamReader reader = null;
        private StreamWriter writer = null;
        private static ASCIIEncoding encoding = new ASCIIEncoding();

        #region Controls Event
        /// <summary>
        /// Constructor
        /// </summary>
        public frmTCPSocketClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TCPSocketClient_Load(object sender, EventArgs e)
        {
            //Connect to server
            ConnectToServer();

            Thread receiveFromServer = new Thread(ReceiveFromServer);
            receiveFromServer.Start();
        }

        /// <summary>
        /// Mark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkPoint_Click(object sender, EventArgs e)
        {
            Label lblPosition = (Label)sender;

            if (lblPosition.Text == "")
            {
                //Get cell position({0,0},{0,1},{0,2}...)
                TableLayoutPanelCellPosition cellPosition = tlpClientBoard.GetCellPosition(lblPosition);

                //Calculate cellposition({0,0},{0,1},{0,2}...) to space number(0->8)
                int spaceNumber = cellPosition.Row * 2 + cellPosition.Row + cellPosition.Column;

                lblPosition.Text = TicTacToeLogic.PlayerSymbol.O.ToString();

                //Send to client
                writer.WriteLine(spaceNumber);

                //Disable and wait client...
                lblResult.Text = "Waiting player 1...";
                tlpClientBoard.Enabled = false;
            }
        }

        /// <summary>
        /// Quit game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            //Close socket
            QuiteGame();
        }

        /// <summary>
        /// New game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            writer.WriteLine("New");
        } 
        #endregion

        #region Private Methods
        /// <summary>
        /// Connect to server
        /// </summary>
        /// <returns></returns>
        private bool ConnectToServer()
        {
            try
            {
                client = new TcpClient();

                //Connect
                client.Connect("199.107.223.235", PORT_NUMBER);
                stream = client.GetStream();
                socket = client.Client;

                //Start game
                StartGame();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to server. Please try again.");
            }

            return true;
        }

        /// <summary>
        /// Start game
        /// </summary>
        private void StartGame()
        {
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            //Send
            writer.WriteLine("Start");

            //Disable board first
            tlpClientBoard.Enabled = false;
            lblResult.Text = "Player 1 play first. Please wait...";
        }

        /// <summary>
        /// Receive from client
        /// </summary>
        private void ReceiveFromServer()
        {
            if (reader == null)
            {
                return;
            }

            try
            {
                while (true)
                {
                    //Receive from client
                    string strResult = reader.ReadLine();

                    if (!string.IsNullOrEmpty(strResult))
                    {
                        if ("QUIT".Equals(strResult.ToUpper()))
                        {
                            if (writer != null)
                            {
                                writer.WriteLine("Quit");
                            }

                            QuiteGame();
                        }
                        else if ("NEW".Equals(strResult.ToUpper()))
                        {
                            DialogResult dlrPlayAgain = MessageBox.Show("Do you want to play again?", "New game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dlrPlayAgain == System.Windows.Forms.DialogResult.Yes)
                            {
                                writer.WriteLine("PlayAgain");
                                NewGame();
                            }
                            else
                            {
                                writer.WriteLine("DoNotPlayAgain");
                                QuiteGame();
                            }
                        }
                        else if ("PLAYAGAIN".Equals(strResult.ToUpper()))
                        {
                            NewGame();
                        }
                        else if ("DONOTPLAYAGAIN".Equals(strResult.ToUpper()))
                        {
                            QuiteGame();
                            break;
                        }
                        else
                        {
                            if (strResult.ToUpper().StartsWith("END"))
                            {
                                //Show result
                                string[] arrResult = strResult.Split(';');

                                tlpClientBoard.Invoke((MethodInvoker)(() => tlpClientBoard.Enabled = false));
                                lblResult.Invoke((MethodInvoker)(() => lblResult.Text = arrResult[1].ToString()));
                                lblPlayer1Score.Invoke((MethodInvoker)(() => lblPlayer1Score.Text = arrResult[2].ToString()));
                                lblPlayer2Score.Invoke((MethodInvoker)(() => lblPlayer2Score.Text = arrResult[3].ToString()));
                            }
                            else
                            {
                                int spaceNumber = int.Parse(strResult);
                                int row = 0, column = 0;
                                //Calculate SpaceNumber(0,1,2...) to CellPosition({0,0},{0,1}....)
                                switch (spaceNumber)
                                {
                                    case 0:
                                        row = 0;
                                        column = 0;
                                        break;
                                    case 1:
                                        row = 0;
                                        column = 1;
                                        break;
                                    case 2:
                                        row = 0;
                                        column = 2;
                                        break;
                                    case 3:
                                        row = 1;
                                        column = 0;
                                        break;
                                    case 4:
                                        row = 1;
                                        column = 1;
                                        break;
                                    case 5:
                                        row = 1;
                                        column = 2;
                                        break;
                                    case 6:
                                        row = 2;
                                        column = 0;
                                        break;
                                    case 7:
                                        row = 2;
                                        column = 1;
                                        break;
                                    case 8:
                                        row = 2;
                                        column = 2;
                                        break;
                                }

                                //Get position which server mark
                                Label lblMark = (Label)tlpClientBoard.GetControlFromPosition(column, row);
                                lblMark.Invoke((MethodInvoker)(() => lblMark.Text = TicTacToeLogic.PlayerSymbol.X.ToString()));

                                //Your turn
                                lblResult.Invoke((MethodInvoker)(() => lblResult.Text = "Your turn"));
                                tlpClientBoard.Invoke((MethodInvoker)(() => tlpClientBoard.Enabled = true));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// New game
        /// </summary>
        private void NewGame()
        {
            //Reset board
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    Label lbl = (Label)tlpClientBoard.GetControlFromPosition(column, row);
                    lbl.Invoke((MethodInvoker)(() => lbl.Text = ""));
                }
            }
            lblResult.Invoke((MethodInvoker)(() => lblResult.Text = "Player 1 play first. Please wait..."));

            //Disable board
            tlpClientBoard.Invoke((MethodInvoker)(() => tlpClientBoard.Enabled = false));

        }

        /// <summary>
        /// Quite game
        /// </summary>
        private void QuiteGame()
        {
            //Close stream
            if (stream != null)
            {
                stream.Close();
            }

            //Close socket
            if (socket != null)
            {
                socket.Close();
            }

            //Close client
            if (client != null)
            {
                client.Close();
            }

            //Close form
            this.Close();
        } 
        #endregion
    }
}
