using GameServer.ServerUtils.DataSenderUtils;
using GameServer.ServerUtils.Models;
using System.Collections.Generic;

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
            {
                currentGame.step = Game.STEP.CANPLAY;
                MorpionSender.SendGameDatas(GetGame(client)); 
            }
        }

        internal static void Play(Client client)
        {
            int[] coords = new int[] { client.sReader.ReadInt32(), client.sReader.ReadInt32()};
            Game currentGame = GetGame(client);
            if (currentGame.Play(client, coords))
                MorpionSender.SendSetCell(currentGame, coords);
        }

        internal static void HasPlayed(Client client)
        {
            Game currentGame = GetGame(client);
            if(GameVerification(currentGame, Game.STEP.PLAYED))
            {
                KeyValuePair<bool, Cell.STATE> endGameResults = currentGame.IsEndGame();
                if (endGameResults.Key)
                {
                    currentGame.step = Game.STEP.END;
                    if (MorpionSender.SendEndGame(endGameResults.Value, currentGame))
                        EraseGame(currentGame);
                }
                else
                {
                    currentGame.ChangeTurn();
                    MorpionSender.SendChangeTurn(currentGame);
                }
            }
        }

        #endregion

        #region UTILS
        internal static Game GetGame(Client client)
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
        internal static void EraseGame(Game game)
        {
            createdGames.Remove(game);
        }
        #endregion
    }
}
