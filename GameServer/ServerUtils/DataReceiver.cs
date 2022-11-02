using GameServer.Manager;
using GameServer.ServerUtils.DataReceiverUtils;
using System;

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
                    Disconnection.EndGame(client);
                    break;

                case "login":
                    Login.ReadLogin(client);
                    break;

                case "isReady":
                    GamesManager.StartGames(client);
                    break;

                case "watchedTheirCards":
                    GamesManager.SetGameStep(client, "watchedTheirCards");
                    break;

                case "action":
                    GamesManager.ReceiveAction(client);
                    break;
                case "newStockCard":
                    GamesManager.CreateNewStockCard(client);
                    break;
                case "binCard":
                    GamesManager.ReceiveBinCard(client);
                    break;

                default:
                    break;
            }
        }
        
    }
}
