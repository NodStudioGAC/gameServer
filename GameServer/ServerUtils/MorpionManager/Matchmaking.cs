using GameServer.ServerUtils;
using GameServer.ServerUtils.Models;
using GameServer.ServerUtils.MorpionManager;

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
            if (!isWaitingPlayer)
            {
                isWaitingPlayer = true;
                waitingPlayer = new Player(login, client, Cell.STATE.CROSS);
                return STATE.WAITING;
            }
            else if (client.id == waitingPlayer.client.id)
            {
                isWaitingPlayer = false;
                waitingPlayer = null;
                return STATE.UNWAITING;
            }
            else
            {
                isWaitingPlayer = false;
                MorpionManager.CreateNewGame(waitingPlayer, new Player(login, client, Cell.STATE.CIRCLE));
                waitingPlayer.client.Write("startGame");
 

                return STATE.MATCH;
            }

        }
        #endregion

    }
}
