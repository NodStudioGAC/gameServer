using GameServer.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.ServerUtils.DataReceiverUtils
{
    class Disconnection
    {

        internal static void EndGame(Client client)
        {
            client.Disconnect();
            GamesManager.EndGame(client);
        }

    }
}
