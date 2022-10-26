using Models;
using System;

namespace GameServer.ServerUtils
{
    class Login
    {
        internal static void ReadLogin(Client client)
        {
            string login = client.sReader.ReadString();
            if (Matchmaking.AddPlayer(login, client))
                client.Write("waitingRoom");

            Console.WriteLine(login);
        }
    }
}
