using GameServer.ServerUtils;
using System.Collections.Generic;

namespace Models
{
    internal class Player
    {

        #region ATTRIBUTES
        internal string playername;
        internal List<Card> cards;
        internal Client client;
        #endregion

        #region CONSTRUCTOR
        public Player(string playername, Client client)
        {
            this.playername = playername;
            this.client = client;
            this.cards = new List<Card>();
        }
        #endregion


    }
}
