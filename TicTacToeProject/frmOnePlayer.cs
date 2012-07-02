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
    public partial class frmOnePlayer : Form
    {
        private static Player humanPlayer = null;
        private static Player computerPlayer = null;
        private static TicTacToeLogic gameLogic = null;
        private static TicTacToeBoard gameBoard = null;

        public frmOnePlayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOnePlayer_Load(object sender, EventArgs e)
        {
            String[] users = new String[2];
            users[0] = "You";
            users[1] = "Computer";

            Random rand = new Random();
            int random_index = rand.Next(1);
            humanPlayer = new Player(users[random_index]);
            random_index = ( random_index == 1 ? 0 : 1 );
            computerPlayer = new Player(users[random_index]);
            gameLogic = new TicTacToeLogic();
            gameBoard = new TicTacToeBoard(humanPlayer, computerPlayer, gameLogic);
        }

        /// <summary>
        /// Mark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkPoint_Click(object sender, EventArgs e)
        {
            Label lblHumanMark = (Label)sender;

            //Get cell position({0,0},{0,1},{0,2}...)
            TableLayoutPanelCellPosition cellPosition = tlpGameBoard.GetCellPosition(lblHumanMark);

            //Calculate cellposition({0,0},{0,1},{0,2}...) to space number(0->8)
            int spaceNumber = cellPosition.Row * 2 + cellPosition.Row + cellPosition.Column;

            //Check and mark
            bool blnMark = gameLogic.Mark(spaceNumber);
            if (blnMark)
            {
                if (gameLogic.GetMark(spaceNumber) == TicTacToeBoard.PlayerSymbol.NoOne)
                {
                    lblHumanMark.Text = "";
                    lblHumanMark.ForeColor = gameBoard.GetSymbolColor(TicTacToeLogic.PlayerSymbol.NoOne);
                }
                else
                {
                    lblHumanMark.Text = gameLogic.GetMark(spaceNumber).ToString();
                    lblHumanMark.ForeColor = gameBoard.GetSymbolColor(TicTacToeLogic.PlayerSymbol.X);
                }

                //Check winner or tied
                if (CheckWinnerOrTied())
                {
                    tlpGameBoard.Enabled = false;
                    return;
                }


                //Computer play
                int comSpaceNumber = gameLogic.ComputeMove();
                if (comSpaceNumber > 0)
                {
                    int row = 0, column = 0;
                    //Calculate SpaceNumber(0,1,2...) to CellPosition({0,0},{0,1}....)
                    switch (comSpaceNumber)
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

                    //Update
                    Label lblComputerMark = (Label)tlpGameBoard.GetControlFromPosition(column, row);
                    if (gameLogic.GetMark(comSpaceNumber) == TicTacToeBoard.PlayerSymbol.NoOne)
                    {
                        lblComputerMark.Text = "";
                        lblComputerMark.ForeColor = gameBoard.GetSymbolColor(TicTacToeLogic.PlayerSymbol.NoOne);
                    }
                    else
                    {
                        lblComputerMark.Text = gameLogic.GetMark(comSpaceNumber).ToString();
                        lblComputerMark.ForeColor = gameBoard.GetSymbolColor(TicTacToeLogic.PlayerSymbol.O);
                    }

                    //Check winner or tied
                    if (CheckWinnerOrTied())
                    {
                        tlpGameBoard.Enabled = false;
                    }
                }
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
                winnerName = humanPlayer.GetName();
                humanPlayer.AddWin();
                computerPlayer.AddLoss();
            }
            else if (winner == TicTacToeLogic.PlayerSymbol.O)
            {
                winnerName = computerPlayer.GetName();
                computerPlayer.AddWin();
                humanPlayer.AddLoss();
            }

            //Update status player
            if (!string.IsNullOrEmpty(winnerName))
            {
                lblResult.Text = string.Format("{0} won!", winnerName);
                lblHumanScore.Text = gameBoard.Player1Wins.ToString();
                lblComputerScore.Text = gameBoard.Player2Wins.ToString();
                return true;
            }
            else if (gameLogic.IsTied())
            {
                lblResult.Text = "Game is tied!";
                humanPlayer.AddTie();
                computerPlayer.AddTie();
                return true;
            }

            return false;
        }

        /// <summary>
        /// New game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //Reset game
            gameLogic = new TicTacToeLogic();

            //Reset board
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    Label lbl = (Label)tlpGameBoard.GetControlFromPosition(column, row);
                    lbl.Text = "";
                }
            }
            lblResult.Text = "";

            //Enable board
            tlpGameBoard.Enabled = true;
        }

        /// <summary>
        /// Quit game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
