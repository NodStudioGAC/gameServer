using GameServer.ServerUtils.DataSenderUtils;
using GameServer.ServerUtils.Models;
using System;

namespace GameServer.ServerUtils.DataReceiverUtils
{
    class Disconnection
    {

        internal static void EndGame(Client client)
        {
            Console.WriteLine("start endgame");
            Game currentGame = MorpionManager.GetGame(client);
            MorpionSender.SendEndGame(Cell.STATE.VOID, currentGame);
            MorpionManager.EraseGame(MorpionManager.GetGame(client));
            client.Disconnect();
        }

    }
}
