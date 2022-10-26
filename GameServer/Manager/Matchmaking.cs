using GameServer.Manager;
using GameServer.ServerUtils;
using System;

namespace Models
{
    class Matchmaking
    {
        #region STATIC_VARIABLES
        internal static Player waitingPlayer;
        internal static bool isWaitingPlayer;
        internal enum STATE { WAITING, UNWAITING, MATCH };
        #endregion

        #region FUNCTIONS
        internal static STATE AddPlayer(string login, Client client)
        {
            Player newPlayer = new Player(login, client);
            if (!isWaitingPlayer)
            {
                Console.WriteLine("here");
                isWaitingPlayer = true;
                waitingPlayer = newPlayer;
                return STATE.WAITING;
            }
            else if (newPlayer.Equals(waitingPlayer))
            {
                isWaitingPlayer = false;
                waitingPlayer = null;
                return STATE.UNWAITING;
            }
            else
            {
                isWaitingPlayer = false;
                GamesManager.CreateNewGame(waitingPlayer, newPlayer);
                return STATE.MATCH;
            }

        }
        #endregion

    }
}
