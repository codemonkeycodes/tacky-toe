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
    /// <summary>
    /// Summary description for TCPSockServer.
    /// </summary>
    public partial class frmTCPSocketServer : Form
    {
        //Constant variables
        private const int PORT_NUMBER = 9999;
        private const int BUFFER_SIZE = 1024;

        //Variables and properties
        static ASCIIEncoding encoding = new ASCIIEncoding();
        private TcpListener listener = null;
        private Socket socket = null;
        private Stream stream = null;
        private StreamReader reader = null;
        private StreamWriter writer = null;

        private static Player player1 = null;
        private static Player player2 = null;
        private static TicTacToeLogic gameLogic = null;
        private static TicTacToeBoard gameBoard = null;

        #region Controls Event

        /// <summary>
        /// Constructor
        /// </summary>
        public frmTCPSocketServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TCPSocketServer_Load(object sender, EventArgs e)
        {
            //Start Server
            StartServer();

            //Receive from client
            Thread receive = new Thread(ReceiveFromClient);
            receive.Start();
        }

        /// <summary>
        /// Mark position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkPoint_Click(object sender, EventArgs e)
        {
            Label lblPosition = (Label)sender;

            //Get cell position({0,0},{0,1},{0,2}...)
            TableLayoutPanelCellPosition cellPosition = tlpServerBoard.GetCellPosition(lblPosition);

            //Calculate cellposition({0,0},{0,1},{0,2}...) to space number(0->8)
            int spaceNumber = cellPosition.Row * 2 + cellPosition.Row + cellPosition.Column;

            //Mark
            bool blnMark = gameLogic.Mark(spaceNumber);

            if (blnMark)
            {
                lblPosition.Text = TicTacToeLogic.PlayerSymbol.X.ToString();

                //Send to client
                writer.WriteLine(spaceNumber);

                //Check winner
                if (CheckWinnerOrTied())
                {
                    tlpServerBoard.Enabled = false;

                    //Send to client if win or tied
                    //The format is: End;Content_message;Player1_Score;Player2_Score
                    writer.WriteLine(string.Format("End;{0};{1};{2}", lblResult.Text, gameBoard.Player1Wins, gameBoard.Player2Wins));
                }
                else
                {
                    //Disable and wait client...
                    tlpServerBoard.Enabled = false;
                    lblResult.Text = "Waiting player 2...";
                }
            }
        }

        /// <summary>
        /// Quite game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            if (writer!=null)
            {
                writer.WriteLine("Quit");
            }
            //Close socket, listener
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

        #region Private methods
        /// <summary>
        /// Start server
        /// </summary>
        private void StartServer()
        {
            try
            {
                //Declare object
                player1 = new Player("Player 1");
                gameLogic = new TicTacToeLogic();

                //Create TCPListener
                IPAddress address = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(address, PORT_NUMBER);

                //Start and listen
                listener.Start();

                socket = listener.AcceptSocket();

                //Init variables
                stream = new NetworkStream(socket);
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.AutoFlush = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot start server. " + ex.Message);
            }
        }

        /// <summary>
        /// Receive from client
        /// </summary>
        private void ReceiveFromClient()
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
                        if ("START".Equals(strResult.ToUpper()))
                        {
                            //Start Game
                            StartGame();
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
                                break;
                            }
                        }
                        else if ("PLAYAGAIN".Equals(strResult.ToUpper()))
                        {
                            NewGame();
                        }
                        else if ("DONOTPLAYAGAIN".Equals(strResult.ToUpper()))
                        {
                            QuiteGame();
                        }
                        else if ("QUIT".Equals(strResult.ToUpper()))
                        {
                            if (writer != null)
                            {
                                writer.WriteLine("quite");
                            }

                            //Quit game
                            QuiteGame();
                        }
                        else
                        {
                            int spaceNumber = int.Parse(strResult);
                            //Mark position
                            bool blnMark = gameLogic.Mark(spaceNumber);

                            if (blnMark)
                            {
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

                                //Update mark
                                Label lblMark = (Label)tlpServerBoard.GetControlFromPosition(column, row);
                                lblMark.Invoke((MethodInvoker)(() => lblMark.Text = TicTacToeLogic.PlayerSymbol.O.ToString()));

                                //Your turn
                                lblResult.Invoke((MethodInvoker)(() => lblResult.Text = "Your turn"));
                                tlpServerBoard.Invoke((MethodInvoker)(() => tlpServerBoard.Enabled = true));

                                //Check winner
                                if (CheckWinnerOrTied())
                                {
                                    //Send to client if win or tied
                                    //The format is: End;Content_message;Player1_Score;Player2_Score
                                    writer.WriteLine(string.Format("End;{0};{1};{2}", lblResult.Text, gameBoard.Player1Wins, gameBoard.Player2Wins));
                                }
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
        /// Check winner or tied
        /// </summary>
        private bool CheckWinnerOrTied()
        {
            //Check winner
            string winnerName = "";
            TicTacToeLogic.PlayerSymbol winner = gameLogic.GetWinner();
            if (winner == TicTacToeLogic.PlayerSymbol.X)
            {
                winnerName = player1.GetName();
                player1.AddWin();
                player2.AddLoss();
            }
            else if (winner == TicTacToeLogic.PlayerSymbol.O)
            {
                winnerName = player2.GetName();
                player2.AddWin();
                player1.AddLoss();
            }

            //Update status player
            if (!string.IsNullOrEmpty(winnerName))
            {
                lblResult.Invoke((MethodInvoker)(() => lblResult.Text = string.Format("Player {0} won!", winnerName)));
                lblPlayer1Score.Invoke((MethodInvoker)(() => lblPlayer1Score.Text = gameBoard.Player1Wins.ToString()));
                lblPlayer2Score.Invoke((MethodInvoker)(() => lblPlayer2Score.Text = gameBoard.Player2Wins.ToString()));

                return true;
            }
            else if (gameLogic.IsTied())
            {
                lblResult.Invoke((MethodInvoker)(() => lblResult.Text = "Game is tied!"));
                player1.AddTie();
                player2.AddTie();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Start game
        /// </summary>
        private void StartGame()
        {
            player1 = new Player("Player 1");
            player2 = new Player("Player 2");
            gameLogic = new TicTacToeLogic();
            gameBoard = new TicTacToeBoard(player1, player2, gameLogic);
            //lblResult.Text = "You play first.";
            lblResult.Invoke((MethodInvoker)(() => lblResult.Text = "You play first!"));
        }

        /// <summary>
        /// New game
        /// </summary>
        private void NewGame()
        {
            //Reset game
            gameLogic = new TicTacToeLogic();

            //Reset board
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    Label lbl = (Label)tlpServerBoard.GetControlFromPosition(column, row);
                    lbl.Invoke((MethodInvoker)(() => lbl.Text = ""));
                }
            }
            lblResult.Invoke((MethodInvoker)(() => lblResult.Text = "You play first!"));

            //Enable board
            tlpServerBoard.Invoke((MethodInvoker)(() => tlpServerBoard.Enabled = true));
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
            if (listener != null)
            {
                listener.Stop();
            }

            //Close form
            this.Close();
        }
        #endregion
    }
}
