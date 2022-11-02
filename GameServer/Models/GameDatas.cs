using GameServer.Models.Power;
using System.Collections.Generic;

namespace Models
{
    public class GameDatas
    {
        #region STATIC_VARIABLES
        internal static int CARDS_PER_PLAYER = 4;
        internal static int NUMBER_OF_CARDS = 52;
        internal static int NUMBER_OF_SIGNS = 4;

        internal static List<PowerData> POWER_DATAS = new List<PowerData>()
        {
            new PowerData(new SeePower(), new List<Card>()
            {
                new Card(Card.SIGN.DIAMOND, 7),
                new Card(Card.SIGN.SPADE, 7),
                new Card(Card.SIGN.HEART, 7),
                new Card(Card.SIGN.CLUB, 7),
            }),
            new PowerData(new SwapPower(), new List<Card>()
            {
                new Card(Card.SIGN.DIAMOND, 11),
                new Card(Card.SIGN.SPADE, 11),
                new Card(Card.SIGN.HEART, 11),
                new Card(Card.SIGN.CLUB, 11),
            }),
        };

        #endregion

        internal class PowerData
        {
            internal AbstractPower power;
            internal List<Card> cards;

            public PowerData(AbstractPower power, List<Card> cards)
            {
                this.cards = cards;
                this.power = power;
            }
        }

    }
}
