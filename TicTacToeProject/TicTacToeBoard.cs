using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeProject
{
    public class TicTacToeBoard : TicTacToeLogic
    {
        Player player1;
        Player player2;
        TicTacToeLogic gameLogic;

        public TicTacToeBoard()
        { 
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="gameLogic"></param>
        public TicTacToeBoard(Player player1, Player player2, TicTacToeLogic gameLogic)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.gameLogic = gameLogic;
        }

        /// <summary>
        /// Check state of space taken
        /// </summary>
        /// <param name="spaceNumber"></param>
        /// <returns></returns>
        public bool IsSpaceTaken(int spaceNumber)
        {
            return gameLogic.GetMark(spaceNumber) != TicTacToeLogic.PlayerSymbol.NoOne;
        }

        /// <summary>
        /// Get space symbol
        /// </summary>
        /// <param name="spaceNumber"></param>
        /// <returns></returns>
        public string GetSpaceString(int spaceNumber)
        {
            return GetSymbolString(gameLogic.GetMark(spaceNumber));
        }

        /// <summary>
        /// Game over
        /// </summary>
        public bool IsGameOver { get { return gameLogic.IsTied() || gameLogic.GetWinner() != TicTacToeLogic.PlayerSymbol.NoOne; } }

        /// <summary>
        /// Winner
        /// </summary>
        public string Winner { get { return GetSymbolPlayer(gameLogic.GetWinner()); } }

        /// <summary>
        /// Is tied
        /// </summary>
        public bool IsTied { get { return gameLogic.IsTied(); } }

        /// <summary>
        /// Player 1 name
        /// </summary>
        public string Player1Name { get { return player1.GetName(); } }

        /// <summary>
        /// Player 2 name
        /// </summary>
        public string Player2Name { get { return player2.GetName(); } }

        /// <summary>
        /// Player 1 score
        /// </summary>
        public int Player1Wins { get { return player1.GetWins(); } }

        public int Player1Losses { get { return player1.GetLosses(); } }

        /// <summary>
        /// Player 2 score
        /// </summary>
        public int Player2Wins { get { return player2.GetWins(); } }

        public int Player2Losses { get { return player2.GetLosses(); } }

        /// <summary>
        /// Current player name
        /// </summary>
        public string CurrentPlayerName { get { return GetSymbolPlayer(gameLogic.CurrentTurn); } }

        /// <summary>
        /// Get symbol player
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private string GetSymbolPlayer(TicTacToeLogic.PlayerSymbol symbol)
        {
            switch (symbol)
            {
                case TicTacToeLogic.PlayerSymbol.X:
                    return player1.GetName();
                case TicTacToeLogic.PlayerSymbol.O:
                    return player2.GetName();
                default:
                    return "";
            }
        }

        /// <summary>
        /// Get symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private string GetSymbolString(TicTacToeLogic.PlayerSymbol symbol)
        {
            switch (symbol)
            {
                case TicTacToeLogic.PlayerSymbol.X:
                    return "X";
                case TicTacToeLogic.PlayerSymbol.O:
                    return "O";
                default:
                    return " ";
            }
        }

        public System.Drawing.Color GetSymbolColor(TicTacToeLogic.PlayerSymbol symbol)
        {
            switch (symbol)
            {
                case PlayerSymbol.X:
                    return System.Drawing.Color.Blue;
                case PlayerSymbol.O:
                    return System.Drawing.Color.Green;
                default:
                    return System.Drawing.Color.Black;
            }
        }
    }
}