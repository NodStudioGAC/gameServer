﻿using GameServer.ServerUtils;
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
                player.client.Write(JsonSerializer.Serialize(game.guid));
                Console.WriteLine(JsonSerializer.Serialize(game.guid));
            }


        }
        internal static void StartGames(Client client)
        {
            Console.WriteLine("startGames");
            string guid = client.sReader.ReadString();
            Console.WriteLine("gui");
            Console.WriteLine(guid);
            foreach (Game game in createdGames)
                    if(game.guid.ToString() == guid)
                    {
                    Console.WriteLine(game);
                        if (game.started)
                            foreach (Player player in game.players)
                            {
                                Console.WriteLine("ready");
                                player.client.Write("playGame");
                                player.client.Write(JsonSerializer.Serialize(game));
                            }
                    
                        else
                            game.started = true;

                        break;
                    }
        }
    }
}
