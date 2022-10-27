using GameServer.ServerUtils;
using GameServer.ServerUtils.DataSenderUtils;
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
            Console.WriteLine("createGame");
            Game game = new Game(player1, player2);
            createdGames.Add(game);
            foreach (Player player in game.players)
            {
                player.client.Write("gameID");
                player.client.Write(game.guid.ToString());
            }
        }
        internal static void StartGames(Client client)
        {
            Console.WriteLine("startGames");
            string guid = client.sReader.ReadString();
            Console.WriteLine("guid");
            Console.WriteLine(guid);
            foreach (Game game in createdGames)
            {
                Console.WriteLine(createdGames.Count);
                Console.WriteLine("game.guid.ToString()");
                Console.WriteLine(game.guid.ToString());
                if (game.guid.ToString() == guid)
                {
                    Console.WriteLine("game.started");
                    Console.WriteLine(game.started);
                    if (game.started)
                        foreach (Player player in game.players)
                            GameSender.SendInitGame(game, player);

                    else
                        game.started = true;
                    break;
                }
            }
        }
    }
}

