using Models;
using System;

namespace GameServer.ServerUtils
{
    class Login
    {
        internal static void ReadLogin(Client client)
        {
            string login = client.sReader.ReadString();
            Console.WriteLine(login);
            Matchmaking.STATE state = Matchmaking.AddPlayer(login, client);
            if(state == Matchmaking.STATE.MATCH)
                client.Write("startGame");

        }
    }
}
