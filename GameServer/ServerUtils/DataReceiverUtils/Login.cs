using Models;
using System;

namespace GameServer.ServerUtils
{
    class Login
    {
        internal static void ReadLogin(Client client)
        {
            string login = client.sReader.ReadString();
            Matchmaking.STATE state = Matchmaking.AddPlayer(login, client);
            switch (state)
            {
                case Matchmaking.STATE.WAITING:
                    client.Write("waitingRoom");
                    break;
                case Matchmaking.STATE.UNWAITING:
                    client.Write("unwaitingRoom");
                    break;
                case Matchmaking.STATE.MATCH:
                    client.Write("startGame");
                    break;

            }
            Console.WriteLine(login);
        }
    }
}
