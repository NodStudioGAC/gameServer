﻿using Models;
using System;


namespace GameServer.ServerUtils.DataSenderUtils
{
    class GameSender
    {
        internal static void SendGameID(Client client, string guid)
        {
            client.Write("gameID");
            client.Write(guid);
        }

        internal static void SendInitGame(Game game, Player player)
        {
            player.client.Write("playGame");
            player.client.Write(game.indexPlayerTurn);
            foreach (Player playerInGame in game.players)
            {
                player.client.Write(playerInGame.playername);
                player.client.Write(playerInGame.client.id);
                player.client.Write(playerInGame.client.id == player.client.id);

                foreach (Card card in playerInGame.cards)
                {
                    player.client.Write("playercard");
                    SendCard(player.client, card);

                }
                player.client.Write("end");
            }
            foreach (Card card in game.cards)
            {
                player.client.Write("card");
                SendCard(player.client, card);

            }
            player.client.Write("end");
        }
        internal static void SendWatchedCardsVerification(Client client)
        {
            client.Write("playersWatchedTheirCards");
        }

        internal static void SendOtherPlayerCards(Client client, Card card)
        {
            client.Write("OtherPlayerCard");
            SendCard(client, card);
        }

        internal static void SendPlayCard(Client client, Game game, int index)
        {
            foreach (Player player in game.players)
            {
                player.client.Write("action");
                player.client.Write("playACard");
                player.client.Write(index);
                player.client.Write(client.id == player.client.id);
            }
        }

        internal static void SendNewStockCard(Game game, Card card)
        {
            foreach (Player player in game.players)
            {
                player.client.Write("newStockCard");
                SendCard(player.client, card);
            }
        }
        internal static void SendBinCard(Client client, Card card)
        {
            client.Write("binCard");
            Console.WriteLine("sendbincard");
            SendCard(client, card);
        }
        internal static void SendHaveAPower(Client client, string power)
        {
            client.Write("power");
            client.Write($"{power}");
        }

        internal static void SendEndGame(Client client)
        {
            client.Write("endGame");
        }

        #region UTILS

        internal static void SendCard(Client client, Card card)
        {
            client.Write(card.value);
            client.Write(card.sign.ToString());
        }

        #endregion
    }
}
