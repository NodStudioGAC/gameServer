using GameServer.ServerUtils;
using GameServer.ServerUtils.DataSenderUtils;
using Models;
using System;
using System.Collections.Generic;

namespace GameServer.Manager
{
    class GamesManager
    {
        #region VARIABLES
        internal static List<Game> createdGames = new List<Game>();
        #endregion
        #region FUNCTIONS
        internal static void CreateNewGame(Player player1, Player player2)
        {
            Game game = new Game(player1, player2);
            createdGames.Add(game);
            foreach (Player player in game.players)
            {
                // A METTRE DANS DATASENDER

                player.client.Write("gameID");
                player.client.Write(game.guid.ToString());

                // -------------------------
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

        internal static void EndGame(Client client)
        {
            Console.WriteLine("disconnected");
            Console.WriteLine(client);

            foreach (Game game in createdGames)
                foreach(Player player in game.players)
                    if(player.client.id == client.id)
                    {
                        game.started = false;
                        foreach (Player playerInGame in game.players)
                        {
                            // A METTRE DANS DATASENDER

                            playerInGame.client.Write("endGame");

                            // -------------------------
                        }

                        break;
                    }
        }

        internal static void SetGameStep(Client client, string currentStep)
        {

            Game game = SearchClientStartedGame(client);
            if(game != null)
                if (game.step == currentStep)
                {
                    foreach (Player playerInGame in game.players)
                    {
                        // A METTRE DANS DATASENDER

                        playerInGame.client.Write("playersWatchedTheirCards");

                        // -------------------------
                    }
                }
                else
                    game.step = currentStep;
                                
        }

        internal static void ReceiveAction(Client client)
        {
            Game game = SearchClientStartedGame(client);
            string action = client.sReader.ReadString();
            if(game != null)
                foreach(Player playerInGame in game.players)
                    if(playerInGame.client.id != client.id)
                    {
                        // A METTRE DANS DATASENDER

                        playerInGame.client.Write("action");
                        playerInGame.client.Write($"{action}");
                        switch (action)
                        {
                            case "playACard":
                                playerInGame.client.Write(client.sReader.ReadInt32());
                                break;

                            case "sameCard":
                                playerInGame.client.Write(client.sReader.ReadInt32());
                                break;

                            case "swapPower":
                                playerInGame.client.Write(client.sReader.ReadInt32());
                                playerInGame.client.Write(client.sReader.ReadInt32());
                                break;

                            case "seePower":
                                playerInGame.client.Write(client.sReader.ReadInt32());
                                playerInGame.client.Write(client.sReader.ReadBoolean());
                                break;
                        }

                        // -------------------------
                    }

        }
        #endregion

        #region UTILS
        internal static Game SearchClientStartedGame(Client client)
        {
            foreach (Game game in createdGames)
                if (game.started)
                    foreach (Player player in game.players)
                        if (player.client.id == client.id)
                        {
                            return game;
                        }
            return null;
        }
        #endregion

    }

}

