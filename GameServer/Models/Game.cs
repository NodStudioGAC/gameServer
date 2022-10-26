using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    internal class Game
    {

        #region ATTRIBUTES
        internal List<Card> cards;
        internal Player[] players;
        internal int indexPlayerTurn = 0;
        internal Guid guid = Guid.NewGuid();
        #endregion

        #region CONSTRUCTOR
        public Game(Player player1, Player player2)
        {
            players = new Player[]
            {
                player1,
                player2
            };
            InitGame();
        }
        #endregion

        #region FUNCTIONS
        void InitGame()
        {
            cards = new List<Card>();
            Card card;
            foreach (Card.SIGN sign in Enum.GetValues(typeof(Card.SIGN)))
                for (int i = 1; i <= GameDatas.NUMBER_OF_CARDS / GameDatas.NUMBER_OF_SIGNS; i++)
                {

                    card = new Card(sign, i == GameDatas.NUMBER_OF_CARDS / GameDatas.NUMBER_OF_SIGNS && (sign == Card.SIGN.HEART || sign == Card.SIGN.DIAMOND) ? 0 : i);
                    cards.Add(card);
                }

            DistributeCards();
        }

        void DistributeCards()
        {
            foreach (Player player in players)
                for (int i = 1; i <= GameDatas.CARDS_PER_PLAYER; i++)
                {
                    Random random = new Random();
                    int randomNum = random.Next(0, cards.Count);
                    Card card = GetFromList(randomNum);
                    card.SetOwner(player);
                }
        }

        #endregion

        #region UTILS
        internal Card GetFromList(int index)
        {
            Card theCard = null;
            foreach (Card card in cards)
            {
                if (index == 0)
                {
                    theCard = card;
                    break;
                }
                index--;
            }

            cards.Remove(theCard);
            return theCard;

        }
        #endregion
    }
}
