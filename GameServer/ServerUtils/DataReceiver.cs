﻿using GameServer.Manager;

namespace GameServer.ServerUtils
{
    class DataReceiver
    {
        internal static void Read(Client client, string data)
        {
            switch (data)
            {
                case "keep connection":
                    client.Write("keep connection");
                    break;

                case "disconnection":
                    client.Disconnect();
                    break;

                case "login":
                    Login.ReadLogin(client);
                    break;

                case "isReady":
                    GamesManager.StartGames(client);
                    break;

                default:
                    break;
            }
        }
        
    }
}