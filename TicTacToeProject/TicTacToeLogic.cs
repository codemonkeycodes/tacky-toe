using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeProject
{
    public class TicTacToeLogic
    {
        public const int SpaceCount = 9;
        private PlayerSymbol turn;
        private PlayerSymbol[] spaces = new PlayerSymbol[SpaceCount];

        /// <summary>
        /// Player symbol
        /// </summary>
        public enum PlayerSymbol
        {
            NoOne,
            X,
            O
        }

        // Handlers for game events
        public delegate void WinHandler(PlayerSymbol player);
        public delegate void TieHandler();

        // Game events
        public event WinHandler Win;
        public event TieHandler Tie;

        /// <summary>
        /// Constructor
        /// </summary>
        public TicTacToeLogic()
        {
            // Start a new game, with X going first
            turn = PlayerSymbol.X;
            Reset();
        }


        /// <summary>
        /// Reset the current game, clearing all spaces.
        /// The current turn is unaffected.
        /// </summary>
        public void Reset()
        {
            // Clear each space
            for (int spaceNumber = 0; spaceNumber < SpaceCount; spaceNumber++)
            {
                spaces[spaceNumber] = PlayerSymbol.NoOne;
            }
        }

        /// <summary>
        /// Mark a space on the board with the current
        /// player's symbol.
        /// spaceNumber - the index of the space to mark (0-8)
        /// Returns true if the mark was placed, false if the
        /// mark could not be placed (game over, or space already
        /// occupied).
        /// </summary>
        /// <param name="spaceNumber"></param>
        /// <returns></returns>
        public bool Mark(int spaceNumber)
        {
            // Ensure it's a valid move
            if (spaceNumber < 0 || spaceNumber >= SpaceCount || spaces[spaceNumber] != PlayerSymbol.NoOne || IsTied() || GetWinner() != PlayerSymbol.NoOne)
            {
                return false;
            }

            // Mark the space
            spaces[spaceNumber] = turn;

            // See if the game has ended
            PlayerSymbol winner = GetWinner();
            if (IsTied() && Tie != null)
            {
                Tie();
            }
            else if (winner != PlayerSymbol.NoOne && Win != null)
            {
                Win(turn);
            }

            // Switch turns
            if (turn == PlayerSymbol.X)
            {
                turn = PlayerSymbol.O;
            }
            else
            {
                turn = PlayerSymbol.X;
            }
            return true;
        }

        /// <summary>
        /// Get the mark in a space.
        /// spaceNumber - the index of the space to inspect (0-8)
        /// Returns the mark in the specified space.
        /// </summary>
        /// <param name="spaceNumber"></param>
        /// <returns></returns>
        public PlayerSymbol GetMark(int spaceNumber)
        {
            // Ensure it's a valid space
            if (spaceNumber < 0 || spaceNumber >= SpaceCount)
            {
                throw new System.ArgumentException("Invalid space number!");
            }
            return spaces[spaceNumber];
        }

        /// <summary>
        ///  Get the current turn.
        ///  Read-only.
        /// </summary>
        public PlayerSymbol CurrentTurn
        {
            get { return turn; }
        }

        /// <summary>
        /// Get the winner of the game.
        /// Returns X or O if a player won, or NoOne
        /// if there is no winner.
        /// </summary>
        /// <returns></returns>
        public PlayerSymbol GetWinner()
        {
            // Top-left to top-right
            if (spaces[0] != PlayerSymbol.NoOne && (spaces[0] == spaces[1]) && (spaces[1] == spaces[2]))
            {
                return spaces[0];
            }
            // Middle-left to middle-right
            else if (spaces[3] != PlayerSymbol.NoOne && (spaces[3] == spaces[4]) && (spaces[4] == spaces[5]))
            {
                return spaces[3];
            }
            // Bottom-left to bottom-right
            else if (spaces[6] != PlayerSymbol.NoOne && (spaces[6] == spaces[7]) && (spaces[7] == spaces[8]))
            {
                return spaces[6];
            }
            // Top-left to bottom-left
            else if (spaces[0] != PlayerSymbol.NoOne && (spaces[0] == spaces[3]) && (spaces[3] == spaces[6]))
            {
                return spaces[0];
            }
            // Top-middle to bottom-middle
            else if (spaces[1] != PlayerSymbol.NoOne && (spaces[1] == spaces[4]) && (spaces[4] == spaces[7]))
            {
                return spaces[1];
            }
            // Top-right to bottom-right
            else if (spaces[2] != PlayerSymbol.NoOne && (spaces[2] == spaces[5]) && (spaces[5] == spaces[8]))
            {
                return spaces[2];
            }
            // Top-left to bottom-right
            else if (spaces[0] != PlayerSymbol.NoOne && (spaces[0] == spaces[4]) && (spaces[4] == spaces[8]))
            {
                return spaces[0];
            }
            // Top-right to bottom-left
            else if (spaces[2] != PlayerSymbol.NoOne && (spaces[2] == spaces[4]) && (spaces[4] == spaces[6]))
            {
                return spaces[2];
            }

            // No winning comibnation found.
            return PlayerSymbol.NoOne;
        }

        /// <summary>
        ///  Determine if the current game is tied.
        ///  Returns true if the game is tied, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsTied()
        {
            //Check tied or not before mark
            //If game is tied(remaining one space), return
            bool isTiedBeforeCheck = CheckTiedBeforeMark();
            if (isTiedBeforeCheck == true)
            {
                return true;
            }

            // See if there are any spaces left
            for (int spaceNumber = 0; spaceNumber < SpaceCount; spaceNumber++)
            {
                if (spaces[spaceNumber] == PlayerSymbol.NoOne)
                {
                    // Found a space, not tied.
                    return false;
                }
            }

            // Checked every space. None are empty.
            // If neither player has won, then it's a tie.
            return (GetWinner() == PlayerSymbol.NoOne);
        }

        /// <summary>
        /// Check tied or not before the player mark
        /// Return: True : Tied 
        ///         False: Not Tied
        /// </summary>
        /// <returns></returns>
        private bool CheckTiedBeforeMark()
        {
            //Count the spaceNumber in sapces[]
            int countSpaceNumber = 0;
            //The position of spaceNumber
            int remainingSpaceNumber = 0;

            //Count spaceNumber in spaces[]
            for (int spaceNumber = 0; spaceNumber < SpaceCount; spaceNumber++)
            {
                if (spaces[spaceNumber] == PlayerSymbol.NoOne)
                {
                    countSpaceNumber++;
                    remainingSpaceNumber = spaceNumber;
                }
            }

            //Check spaceNumber
            if (countSpaceNumber == 1)
            {
                switch (remainingSpaceNumber)
                {
                    case 0:
                        //Check CurrentTurn with symbol at position: [1][2]; [3][6] and [4][8]
                        if ((CurrentTurn != spaces[1] || CurrentTurn != spaces[2])
                            && (CurrentTurn != spaces[3] || CurrentTurn != spaces[6])
                            && (CurrentTurn != spaces[4] || CurrentTurn != spaces[8]))
                        {
                            return true;
                        }
                        break;
                    case 1:
                        //Check CurrentTurn with symbol at position: [0][2] and [4][7]
                        if ((CurrentTurn != spaces[0] || CurrentTurn != spaces[2])
                            && (CurrentTurn != spaces[4] || CurrentTurn != spaces[7]))
                        {
                            return true;
                        }
                        break;
                    case 2:
                        //Check CurrentTurn with symbol at position: [0][1]; [4][6] and [5][8]
                        if ((CurrentTurn != spaces[0] || CurrentTurn != spaces[1])
                            && (CurrentTurn != spaces[4] || CurrentTurn != spaces[6])
                            && (CurrentTurn != spaces[5] || CurrentTurn != spaces[8]))
                        {
                            return true;
                        }
                        break;
                    case 3:
                        //Check CurrentTurn with symbol at position: [0][6] and [4][5]
                        if ((CurrentTurn != spaces[0] || CurrentTurn != spaces[6])
                            && (CurrentTurn != spaces[4] || CurrentTurn != spaces[5]))
                        {
                            return true;
                        }
                        break;
                    case 4:
                        //Check CurrentTurn with symbol at position: [0][8]; [1][7]; [2][6] and [3][5]
                        if ((CurrentTurn != spaces[0] || CurrentTurn != spaces[8])
                            && (CurrentTurn != spaces[1] || CurrentTurn != spaces[7])
                            && (CurrentTurn != spaces[2] || CurrentTurn != spaces[6])
                            && (CurrentTurn != spaces[3] || CurrentTurn != spaces[5]))
                        {
                            return true;
                        }
                        break;
                    case 5:
                        //Check CurrentTurn with symbol at position: [2][8] and [3][4]
                        if ((CurrentTurn != spaces[2] || CurrentTurn != spaces[8])
                            && (CurrentTurn != spaces[3] || CurrentTurn != spaces[4]))
                        {
                            return true;
                        }
                        break;
                    case 6:
                        //Check CurrentTurn with symbol at position: [0][3] and [7][8]
                        if ((CurrentTurn != spaces[0] || CurrentTurn != spaces[3])
                            && (CurrentTurn != spaces[7] || CurrentTurn != spaces[8]))
                        {
                            return true;
                        }
                        break;
                    case 7:
                        //Check CurrentTurn with symbol at position: [1][4] and [6][8]
                        if ((CurrentTurn != spaces[1] || CurrentTurn != spaces[4])
                            && (CurrentTurn != spaces[6] || CurrentTurn != spaces[8]))
                        {
                            return true;
                        }
                        break;
                    case 8:
                        //Check CurrentTurn with symbol at position: [2][5] and [6][7]
                        if ((CurrentTurn != spaces[2] || CurrentTurn != spaces[5])
                            && (CurrentTurn != spaces[6] || CurrentTurn != spaces[7]))
                        {
                            return true;
                        }
                        break;
                }
            }

            return false;
        }

        /// <summary>
        /// Check and mark with computer
        /// </summary>
        /// <returns></returns>
        public int ComputeMove()
        {
            PlayerSymbol comPlayerSymbol = CurrentTurn;
            PlayerSymbol humanPlayerSymbol = PlayerSymbol.O;
            if (PlayerSymbol.O == CurrentTurn)
            {
                humanPlayerSymbol = PlayerSymbol.X;
            }
            else
            {
                humanPlayerSymbol = PlayerSymbol.O;
            }

            //********************************* Horizontal - Vertical ************************************

            //Variables
            int horizonSpaceNumber = -1;
            int verticalSpaceNumber = -1;
            int horizonSpaceComPosition = -1;
            int verticalSpaceComPosition = -1;
            int horizonCountComPlayerSymbol = 0;
            int verticalCountComPlayerSymbol = 0;
            int horizonCountHumanPlayerSymbol = 0;
            int verticalCountHumanPlayerSymbol = 0;
            for (int row = 0; row < 3; row++)
            {
                //Reset variable
                horizonCountComPlayerSymbol = 0;
                verticalCountComPlayerSymbol = 0;
                horizonCountHumanPlayerSymbol = 0;
                verticalCountHumanPlayerSymbol = 0;
                horizonSpaceComPosition = -1;
                verticalSpaceComPosition = -1;

                for (int column = 0; column < 3; column++)
                {
                    //Calculate row, column to spaceNumber
                    horizonSpaceNumber = row * 2 + row + column;
                    verticalSpaceNumber = column * 2 + row + column;

                    //Get player symbol at spaceNumber position
                    PlayerSymbol horizonCurrentPlayerSymbol = GetMark(horizonSpaceNumber);
                    PlayerSymbol verticalCurrentPlayerSymbol = GetMark(verticalSpaceNumber);

                    //Check follow horizontal
                    if (horizonCurrentPlayerSymbol == humanPlayerSymbol)
                    {
                        horizonCountHumanPlayerSymbol++;
                    }
                    else if (horizonCurrentPlayerSymbol == comPlayerSymbol)
                    {
                        horizonCountComPlayerSymbol++;
                    }
                    else if (horizonCurrentPlayerSymbol == PlayerSymbol.NoOne)
                    {
                        horizonSpaceComPosition = horizonSpaceNumber;
                    }

                    //Check follow vertical
                    if (verticalCurrentPlayerSymbol == humanPlayerSymbol)
                    {
                        verticalCountHumanPlayerSymbol++;
                    }
                    else if (verticalCurrentPlayerSymbol == comPlayerSymbol)
                    {
                        verticalCountComPlayerSymbol++;
                    }
                    else if (verticalCurrentPlayerSymbol == PlayerSymbol.NoOne)
                    {
                        verticalSpaceComPosition = verticalSpaceNumber;
                    }
                }

                //Check result               
                if (horizonCountComPlayerSymbol == 2 && horizonSpaceComPosition > 0)
                {
                    Mark(horizonSpaceComPosition);
                    return horizonSpaceComPosition;
                }
                else if (verticalCountComPlayerSymbol == 2 && verticalSpaceComPosition > 0)
                {
                    Mark(verticalSpaceComPosition);
                    return verticalSpaceComPosition;
                }
                else if (horizonCountHumanPlayerSymbol == 2 && horizonSpaceComPosition > 0)
                {
                    Mark(horizonSpaceComPosition);
                    return horizonSpaceComPosition;
                }
                else if (verticalCountHumanPlayerSymbol == 2 && verticalSpaceComPosition > 0)
                {
                    Mark(verticalSpaceComPosition);
                    return verticalSpaceComPosition;
                }
            }
            //********************************************************************************************************

            //******************************************************* Diagonal *****************************************
            int principalCountComPlayerSymbol = 0;
            int principalSpaceComPosition = -1;
            int principalCountHumanPlayerSymbol = 0;
            int secondaryCountComPlayerSymbol = 0;
            int secondarySpaceComPosition = -1;
            int secondaryCountHumanPlayerSybol = 0;

            for (int i = 0; i < 3; i++)
            {
                //Principal diagonal - Position 0-4-8
                int principalSpaceNumber = i * 4;
                PlayerSymbol principalCurrentSymbol = GetMark(principalSpaceNumber);

                if (principalCurrentSymbol == humanPlayerSymbol)
                {
                    principalCountHumanPlayerSymbol++;
                }
                else if (principalCurrentSymbol == comPlayerSymbol)
                {
                    principalCountComPlayerSymbol++;
                }
                else if (principalCurrentSymbol == PlayerSymbol.NoOne)
                {
                    principalSpaceComPosition = principalSpaceNumber;
                }

                //Secondary diagonal - Position 2-4-6
                int secondarySpaceNumber = (i + 1) * 2;
                PlayerSymbol secondaryCurrentSymbol = GetMark(secondarySpaceNumber);

                if (secondaryCurrentSymbol == humanPlayerSymbol)
                {
                    secondaryCountHumanPlayerSybol++;
                }
                else if (secondaryCurrentSymbol == comPlayerSymbol)
                {
                    secondaryCountComPlayerSymbol++;
                }
                else if (secondaryCurrentSymbol == PlayerSymbol.NoOne)
                {
                    secondarySpaceComPosition = secondarySpaceNumber;
                }

            }

            //Check result
            if (principalCountComPlayerSymbol == 2 && principalSpaceComPosition > 0)
            {
                Mark(principalSpaceComPosition);
                return principalSpaceComPosition;
            }
            else if (secondaryCountComPlayerSymbol == 2 && secondarySpaceComPosition > 0)
            {
                Mark(secondarySpaceComPosition);
                return secondarySpaceComPosition;
            }
            else if (principalCountHumanPlayerSymbol == 2 && principalSpaceComPosition > 0)
            {
                Mark(principalSpaceComPosition);
                return principalSpaceComPosition;
            }
            else if (secondaryCountHumanPlayerSybol == 2 && secondarySpaceComPosition > 0)
            {
                Mark(secondarySpaceComPosition);
                return secondarySpaceComPosition;
            }
            //*********************************************************************************************************

            //Create random spaceNumber
            int randomSpaceNumber = RandomSpaceNumber();
            Mark(randomSpaceNumber);

            return randomSpaceNumber;
        }

        /// <summary>
        /// Create random space number
        /// </summary>
        /// <returns></returns>
        private int RandomSpaceNumber()
        {
            Random rand = new Random();
            int random = rand.Next(0, 9);
            while (GetMark(random) != PlayerSymbol.NoOne)
            {
                random = rand.Next(0, 9);
            }
            return random;

        }
    }
}