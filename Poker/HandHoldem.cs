using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm.Poker
{
	public class HandHoldem : Hand
	{
        /// <summary>
        /// A hand containing two cards
        /// </summary>
        /// <param name="firstCard">the first card</param>
        /// <param name="secondCard">the second card</param>
		public HandHoldem(Card firstCard, Card secondCard)
		{
			Cards = new List<Card> { firstCard, secondCard };
		}
	}
}
