using System;

namespace GameServer.ServerUtils.DataReceiverUtils
{
    class Disconnection
    {

        internal static void EndGame(Client client)
        {
            Console.WriteLine("start endgame");
            Console.WriteLine(client);
            client.Disconnect();
        }

    }
}
