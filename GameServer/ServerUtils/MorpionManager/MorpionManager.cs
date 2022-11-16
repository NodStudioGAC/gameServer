﻿using GameServer.ServerUtils.DataSenderUtils;
using GameServer.ServerUtils.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.ServerUtils
{
    class MorpionManager
    {
        #region VARIABLES
        internal static List<Game> createdGames = new List<Game>();
        #endregion

        #region FUNCTIONS
        internal static void CreateNewGame(Player player1, Player player2)
        {
            Game game = new Game(new Player[]{ player1, player2 });
            createdGames.Add(game);
        }
        internal static void StartGame(Client client)
        {
            Game currentGame = GetGame(client);
            if(GameVerification(currentGame, Game.STEP.STARTED))
                MorpionSender.SendGameDatas(GetGame(client)); 
        }
        #endregion

        #region UTILS
        static Game GetGame(Client client)
        {
            foreach(Game game in createdGames)
                foreach(Player player in game.players)
                    if(player.client.id == client.id)
                        return game;

            return null;
        }
        static bool GameVerification(Game game, Game.STEP newStep)
        {
            if (game.step == newStep)
            {
                game.step = Game.STEP.INIT;
                return true;
            }

            game.step = newStep;
            return false;
        }
        void EraseGame(Game game)
        {
            game = null;
        }
        #endregion
    }
}
