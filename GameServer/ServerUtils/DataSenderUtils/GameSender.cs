using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.ServerUtils.DataSenderUtils
{
    class GameSender
    {
        internal static void SendInitGame(Game game, Player player)
        {
            Console.WriteLine("ready");
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
                    player.client.Write(card.value);
                    player.client.Write(card.sign.ToString());
                }
                player.client.Write("end");
            }
            foreach (Card card in game.cards)
            {
                player.client.Write("card");
                player.client.Write(card.value);
                player.client.Write(card.sign.ToString());
            }
            Console.WriteLine("end");
            player.client.Write("end");
        }
    }
}
