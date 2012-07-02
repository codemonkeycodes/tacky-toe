using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TicTacToeProject
{        
    /// <summary>
    /// A single player in a game.
    /// </summary>
    public class Player : IXmlSerializable
    {
        private int wins;
        private int losses;
        private int total_games;
        private string name;

        /*
            Create a new player.

            name - The name of this player
        */
        public Player() : this("")
        {
            
        }

        public Player(string _name) : this(_name, 0, 0, 0 )
        {
        }

        public Player(string _name, int _wins, int _losses, int _total)
        {
            name = _name;
            wins = _wins;
            losses = _losses;
            total_games = _total;
        }

        /*
            Award a point to this player, increasing
            their score by one.
        */
        public void AddWin()
        {
            wins++;
            AddTotalGames();
        }

        public void AddLoss()
        {
            losses++;
            AddTotalGames();
        }

        public void AddTie()
        {
            AddTotalGames();
        }

        public void AddTotalGames()
        {
            total_games++;
        }

        public string GetName()
        {
            return name;
        }

        public int GetWins()
        {
            return wins;
        }

        public int GetLosses()
        {
            return losses;
        }

        public int GetTotal()
        {
            return total_games;
        }


        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(name);
            writer.WriteValue(wins);
            writer.WriteValue(losses);
            writer.WriteValue(total_games);
        }

        public void ReadXml(XmlReader reader)
        {
            name = reader.ReadString();
            wins = reader.ReadContentAsInt();
            losses = reader.ReadContentAsInt();
            total_games = reader.ReadContentAsInt();
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }
    }
}