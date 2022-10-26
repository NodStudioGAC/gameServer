using GameServer.ServerUtils;
using Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GameServer.Manager
{
    class GamesManager
    {
        internal static List<Game> createdGames = new List<Game>();
        internal static void CreateNewGame(Player player1, Player player2)
        {
            createdGames.Add(new Game(player1, player2));
        }
        internal static void StartGames(Client client)
        {
            string guid = client.sReader.ReadString();
                foreach(Game game in createdGames)
                    if(game.guid.ToString() == guid)
                    {
                        if (game.started)
                            foreach (Player player in game.players)
                            {
                                player.client.Write("startGame");
                                player.client.Write(JsonSerializer.Serialize(game));
                            }
                    
                        else
                            game.started = true;

                        break;
                    }
        }
    }
}
