using GameServer.ServerUtils;
using Models;

namespace GameServer.Models.Power
{
    public class NoPower : AbstractPower
    {
        #region ACTION

        internal override void Action(Client client)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
