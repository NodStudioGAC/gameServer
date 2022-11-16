using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.ServerUtils.Models
{
    class Player
    {
        #region ATTRIBUTES
        internal string playername;
        internal Client client;
        internal Cell.STATE state;
        #endregion

        #region CONSTRUCTOR
        public Player(string playername, Client client, Cell.STATE state)
        {
            this.playername = playername;
            this.client = client;
            this.state = state;
        }
        #endregion

    }
}
