using System.Collections.Generic;

namespace Game
{
    public class BlackjackHandCalculator
    {
        public static int CalculateHandValue(List<Card> hand)
        {
            int totalValue = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                if (card.Type == CardType.Ace)
                {
                    aceCount++;
                    totalValue += 11;
                }
                else if (card.Type == CardType.Jack)
                {
                    totalValue += 10;
                }
                else if (card.Type == CardType.Queen)
                {
                    totalValue += 10;
                }
                else if (card.Type == CardType.King)
                {
                    totalValue += 10;
                }
                else
                {
                    totalValue += (int)card.Type;
                }
            }

            while (totalValue > 21 && aceCount > 0)
            {
                totalValue -= 10;
                aceCount--;
            }

            return totalValue;
        }
    }
}
