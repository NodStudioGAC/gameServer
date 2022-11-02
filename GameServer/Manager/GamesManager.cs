﻿using GameServer.ServerUtils;
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
                GameSender.SendGameID(player.client, game.guid.ToString());
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
                            GameSender.SendEndGame(playerInGame.client);

                        break;
                    }
        }

        internal static void SetGameStep(Client client, string currentStep)
        {
            Game game = SearchClientStartedGame(client);
            if(game != null)
                if (game.step == currentStep)
                    foreach (Player playerInGame in game.players)
                        GameSender.SendWatchedCardsVerification(playerInGame.client);

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
                        GameSender.SendAction(playerInGame.client, action);
        }
        internal static void CreateNewStockCard(Client client)
        {
            Game game = SearchClientStartedGame(client);
            Card newStockCard = game.GetStockCard();
            GameSender.SendNewStockCard(client, newStockCard);
        }
        internal static void ReceiveBinCard(Client client)
        {
            int index = client.sReader.ReadInt32();
            Game currentGame;
            Card card = null;
            foreach (Game game in createdGames)
                if (game.started)
                    foreach (Player player in game.players)
                        if (player.client.id == client.id)
                        {
                            currentGame = game;
                            card = player.cards[index];
                            SendBinCard(game, player, card);
                            break;
                        }

            card?.owner?.cards.Remove(card);
            card.owner = null;
        }

        internal static void SendBinCard(Game game, Player currentPlayer, Card card)
        {
            foreach(Player player in game.players)
            {
                card.power.Action(currentPlayer.client);
                GameSender.SendBinCard(player.client, card);
            }
        }
        internal static void SendOtherPlayerCards(Client client)
        {
            List<int> indexList = new List<int>();
            while (client.sReader.ReadString() == "cardIndex")
                indexList.Add(client.sReader.ReadInt32());
            Game game = SearchClientStartedGame(client);
            if(game != null){
                foreach (Player playerInGame in game.players)
                    if (playerInGame.client.id != client.id)
                        foreach(int index in indexList)
                            GameSender.SendOtherPlayerCards(client, playerInGame.cards[index]);
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

        internal static Card SearchPlayerCard(Client client, int index)
        {
            foreach (Game game in createdGames)
                if (game.started)
                    foreach (Player player in game.players)
                        if (player.client.id == client.id)
                        {
                            return player.cards[index];
                        }
            return null;
        }
        
        internal static void UpdateDeck(Client client, int index, Card card)
        {
            Game game = SearchClientStartedGame(client);
            foreach(Player player in game.players)
            {
                if (player.client.id == client.id)
                {
                    
                }
            }
        }

        #endregion

    }

}

