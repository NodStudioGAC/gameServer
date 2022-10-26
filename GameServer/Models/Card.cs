using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    internal class Card
    {
        #region ENUM
        public enum SIGN { HEART, SPADE, DIAMOND, CLUB };
        #endregion

        #region ATTRIBUTES
        internal int value;
        internal SIGN sign;
        internal Player owner;
        #endregion

        #region CONSTRUCTOR
        public Card(SIGN sign, int value)
        {
            this.value = value;
            this.sign = sign;
        }
        #endregion

        #region FUNCTIONS
        public override string ToString()
        {
            return $"{GetTranslatedValue(value)} {sign}";
        }

        public override bool Equals(object obj)
        {
            Card cardObject = (Card)obj;
            return cardObject.value == value && cardObject.sign == sign;
        }

        internal void SetOwner(Player owner)
        {
            this.owner = owner;
            owner.cards.Add(this);
        }
        internal static string GetTranslatedValue(int value)
        {
            switch (value)
            {
                case 1:
                    return "A";

                case 11:
                    return "J";

                case 12:
                    return "Q";

                case 13:
                case 0:
                    return "K";

                default:
                    return value.ToString();
            }
        }
        #endregion

    }
}
