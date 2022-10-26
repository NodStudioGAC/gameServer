using GameServer.ServerUtils;
using Models;
using System;
using System.Collections.Generic;

namespace GameServer.Manager
{
    class GamesManager
    {
        internal static List<Game> createdGames;
        internal static List<string> startedGames;
        internal static void CreateNewGame(Player player1, Player player2)
        {
            createdGames.Add(new Game(player1, player2));

        }
        internal static void StartGames(Client client)
        {
            string guid = client.sReader.ReadString();

            if (startedGames.Contains(guid))
                foreach(Game game in createdGames)
                    if(game.guid.ToString() == guid)
                    {
                        foreach(Player player in game.players)
                        {
                            player.client.Write("startGame");
                        }
                        break;
                    }
            else
                startedGames.Add(guid);
            
        }
    }
}
