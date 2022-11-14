using GameServer.ServerUtils;
using GameServer.ServerUtils.DataSenderUtils;
using Models;

namespace GameServer.Models.Power
{
    public class NoPower : AbstractPower
    {
        #region ACTION

        internal override void Action(Client client)
        {
            GameSender.SendHaveNoPower(client);

        }
        #endregion
    }
}
