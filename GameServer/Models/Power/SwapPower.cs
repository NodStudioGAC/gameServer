using GameServer.ServerUtils;
using GameServer.ServerUtils.DataSenderUtils;
using Models;

namespace GameServer.Models.Power
{
    public class SwapPower : AbstractPower
    {

        #region ACTION
        internal override void Action(Client client)
        {
            GameSender.SendHaveAPower(client, "seePower");
        }
        #endregion
    }
}
