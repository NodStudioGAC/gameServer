using GameServer.ServerUtils;
using GameServer.ServerUtils.DataSenderUtils;
using Models;
using System;
using System.Collections.Generic;

namespace GameServer.Manager
{
    class GamesManager
    {
        internal static List<Game> createdGames = new List<Game>();
        internal static void CreateNewGame(Player player1, Player player2)
        {
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
            string guid = client.sReader.ReadString();
            foreach (Game game in createdGames)
                if (game.guid.ToString() == guid)
                {
                    if (game.started)
                        foreach (Player player in game.players)
                            GameSender.SendInitGame(game, player);

                    else
                        game.started = true;
                    break;
                }
        }

        internal static void EndGames(Client client)
        {
            Console.WriteLine("disconnected");
            foreach(Game game in createdGames)
                foreach(Player player in game.players)
                    if(player.client.id == client.id)
                    {
                        game.started = false;
                        foreach (Player playerInGame in game.players)
                            playerInGame.client.Write("endGame");

                        break;
                    }
        }
    }
}

