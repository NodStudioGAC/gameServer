
using GameServer.ServerUtils.Models;

namespace GameServer.ServerUtils.DataSenderUtils
{
    class MorpionSender
    {
        internal static void SendGameDatas(Game game)
        {
            foreach(Player player in game.players)
            {
                player.client.Write("gameDatas");
                player.client.Write(game.players[0].client.id == player.client.id ? game.players[1].playername : game.players[0].playername);
                player.client.Write(player.state.ToString());
            }
        }
        internal static void SendSetCell(Game game, int[] coords)
        {
            foreach(Player player in game.players)
            {
                player.client.Write("setCell");
                player.client.Write(game.currentPlayer.state.ToString());
                player.client.Write(coords[0]);
                player.client.Write(coords[1]);
            }
        }
        internal static void SendChangeTurn(Game game)
        {
            foreach(Player player in game.players)
            {
                player.client.Write("turnChanged");
                player.client.Write(player.client.id == game.currentPlayer.client.id);
            }
        }
        internal static bool SendEndGame(Cell.STATE state, Game game)
        {
            foreach (Player player in game.players)
            {
                player.client.Write("endGame");
                player.client.Write(state.ToString());
            }
            return true;
        }
    }
}
