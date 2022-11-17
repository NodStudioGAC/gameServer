using System;

namespace GameServer.ServerUtils.DataReceiverUtils
{
    class Disconnection
    {

        internal static void EndGame(Client client)
        {
            Console.WriteLine("start endgame");
            MorpionManager.EraseGame(MorpionManager.GetGame(client));
            client.Disconnect();
        }

    }
}
