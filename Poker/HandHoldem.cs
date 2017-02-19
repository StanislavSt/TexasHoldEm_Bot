using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm.Poker
{
	public class HandHoldem : Hand
	{
		/**
	 * A hand containing two cards
	 * @param firstCard : the first card
	 * @param secondCard : the second card
	 */
		public HandHoldem(Card firstCard, Card secondCard)
		{
			Cards = new List<Card> { firstCard, secondCard };
		}
	}
}
