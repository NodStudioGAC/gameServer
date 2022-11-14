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
                player.client.gameID = game.guid.ToString();
                GameSender.SendGameID(player.client, game.guid.ToString());
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
                            GameSender.SendEndGame(playerInGame.client);

                        break;
                    }
        }
        internal static void SetGameStep(Client client, string currentStep)
        {
            Game game = SearchClientStartedGame(client);
            if(game != null)
                if (game.step == currentStep)
                    switch (currentStep)
                    {
                        case "watchedTheirCards":
                            foreach (Player playerInGame in game.players)
                                GameSender.SendWatchedCardsVerification(playerInGame.client);
                            break;

                        case "haveNewBinCard":
                            Console.WriteLine("haveNewBinCard");
                            foreach (Player playerInGame in game.players)
                                GameSender.SendNewBinCardVerification(playerInGame.client);
                            break;
                    }

                else
                    game.step = currentStep;
        }
        internal static void ReceiveAction(Client client)
        {
            Game game = SearchClientStartedGame(client);
            string action = client.sReader.ReadString();
            if(game != null)
                switch (action)
                {
                    case "playACard":
                        int indexPlayCard = client.sReader.ReadInt32();
                       GameSender.SendPlayCard(client, game, indexPlayCard);
                        break;

                    case "sameCard":
                        int indexSameCard = client.sReader.ReadInt32();
                        PlaySameCard(client, game, indexSameCard);
                        break;

                    case "swapPower":
                        int indexSwapPower1 = client.sReader.ReadInt32();
                        int indexSwapPower2 = client.sReader.ReadInt32();
                        break;

                    case "seePower":
                        int indexSeePower = client.sReader.ReadInt32();
                        bool boolSeePower = client.sReader.ReadBoolean();
                        break;
                }
            

        }
        internal static void CreateNewStockCard(Client client)
        {
            Game game = SearchClientStartedGame(client);
            Card newStockCard = game.GetStockCard();
            GameSender.SendNewStockCard(game, newStockCard);
        }
        internal static void ReceiveBinCard(Client client)
        {
            int index = client.sReader.ReadInt32();
            Console.WriteLine("index");
            Console.WriteLine(index);
            Game currentGame;
            Card card;
            foreach (Game game in createdGames)
                if (game.started)
                    foreach (Player player in game.players)
                        if (player.client.id == client.id)
                        {
                            currentGame = game;
                            card = player.cards[index];
                            card?.owner?.cards.Remove(card);
                            card.owner = null;
                            SetGameStep(client, "haveNewBinCard");
                            card.power.Action(client);
                            break;
                        }
        }
        internal static void SendOtherPlayerCards(Client client)
        {
            List<int> indexList = new List<int>();
            while (client.sReader.ReadString() == "cardIndex")
                indexList.Add(client.sReader.ReadInt32());

            Game game = SearchClientStartedGame(client);
            if(game != null)
                foreach (Player playerInGame in game.players)
                    if (playerInGame.client.id != client.id)
                        foreach(int index in indexList)
                            GameSender.SendOtherPlayerCards(client, playerInGame.cards[index]);
        }
        internal static void PlaySameCard(Client client, Game game, int indexSameCard)
        {
            Card currentBinCard = game.binCards[game.binCardsLength - 1];
            foreach(Player player in game.players)
            {
                if(player.client.id == client.id)
                {

                    if(currentBinCard.value == player.cards[indexSameCard].value)
                    {
                        GameSender.SendPlaySameCard(game, true);
                    }
                    else
                    {
                        GameSender.SendPlaySameCard(game, false);
                    }
                break;
                }
            }
        }
        #endregion

        #region UTILS
        internal static Game SearchClientStartedGame(Client client)
        {
            foreach (Game game in createdGames)
                if (game.started && client.gameID == game.guid.ToString())
                    return game;
            return null;
        }
        internal static Card SearchPlayerCard(Client client, int index)
        {
            foreach (Game game in createdGames)
                if (game.started)
                    foreach (Player player in game.players)
                        if (player.client.id == client.id)
                            return player.cards[index];

            return null;
        }
        #endregion

    }

}

