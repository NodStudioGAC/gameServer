using GameServer.Manager;
using GameServer.ServerUtils;
using System;

namespace Models
{
    class Matchmaking
    {
        #region VARIABLES
        internal static Player waitingPlayer;
        internal static bool isWaitingPlayer;
        #endregion

        #region FUNCTIONS
        internal static bool AddPlayer(string login, Client client)
        {
            Player newPlayer = new Player(login, client);
            if (!isWaitingPlayer)
            {
                Console.WriteLine("here");
                isWaitingPlayer = true;
                waitingPlayer = newPlayer;
                return true;
            }
            else
            {
                isWaitingPlayer = false;
                GamesManager.CreateNewGame(waitingPlayer, newPlayer);
                return false;
            }

        }
        #endregion

    }
}
