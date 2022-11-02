using GameServer.ServerUtils;
using Models;
using static Models.GameDatas;

namespace GameServer.Models.Power
{
    public abstract class AbstractPower
    {
        #region ATTRIBUTES
        internal abstract void Action(Client client);
        #endregion

        #region ABSTRACT_FUNCTIONS
        internal static AbstractPower GetPower(Card card)
        {
            foreach (PowerData powerData in POWER_DATAS)
                if (powerData.cards.Contains(card))
                    return powerData.power;

            return new NoPower();
        }
        #endregion
    }
}
