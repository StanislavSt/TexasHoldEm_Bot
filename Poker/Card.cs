using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm.Poker
{
    public class Card
    {
        public CardHeight height;
        private CardSuit suit;
        private int number;
        private static Dictionary<string, Card> stringToCard;
        /**
	    * Creates a card object based on a number between 0 and 51
	    */
        public Card(int num)
        {
            //Assign the Suit of the card
            number = num;
            int findSuit = number / 13;
            switch(findSuit)
            {
                case 0 : 
                    suit = CardSuit.SPADES; 
                    break;
                case 1 : 
                    suit = CardSuit.HEARTS; 
                    break;
                case 2 : 
                    suit = CardSuit.CLUBS; 
                    break;
                case 3 : 
                    suit = CardSuit.DIAMONDS;
                    break;
            }
            //Assign the Height
            int findHeight = number % 13;
            switch (findHeight)
            {
                case 0:
                    height = CardHeight.DEUCE;
                    break;
                case 1:
                    height = CardHeight.THREE;
                    break;
                case 2:
                    height = CardHeight.FOUR;
                    break;
                case 3:
                    height = CardHeight.FIVE;
                    break;
                case 4:
                    height = CardHeight.SIX;
                    break;
                case 5:
                    height = CardHeight.SEVEN;
                    break;
                case 6:
                    height = CardHeight.EIGHT;
                    break;
                case 7:
                    height = CardHeight.NINE;
                    break;
                case 8:
                    height = CardHeight.TEN;
                    break;
                case 9:
                    height = CardHeight.JACK;
                    break;
                case 10:
                    height = CardHeight.QUEEN;
                    break;
                case 11:
                    height = CardHeight.KING;
                    break;
                case 12:
                    height = CardHeight.ACE;
                    break;
            }
        }
        /**
	     * Returns the Card object that corresponds with the given card string. The first time this method is called, a
	     * map of all Cards corresponding with correct input strings is created.
	     * @param string : the string to be converted to a Card
	     */
        public static Card getCard(string s)
	    {
		    if(stringToCard == null)
		    {
			    stringToCard = new Dictionary<string,Card>();
			    for(int i = 0; i < 52; ++i)
			    {
				    Card card = new Card(i);
				    stringToCard[card.toString()] = card;
			    }
		    }
            return stringToCard[s];
	    }
        /**
	    * Returns the number of the card as a long.
	    */
        public long getNumber()
        {
            int suitShift = number / 13;
            int heightShift = number % 13;
            return (1L << (16 * suitShift + heightShift));
        }
        /**
	    * Returns the height of this card.
	    */
        public CardHeight getHeight()
        {
            return height;
        }
        /**
	    * Returns the suit of this card.
	    */
        public CardSuit getSuit()
        {
            return suit;
        }
        /**
	 * Returns a String representation of this card.
	 */
        public String toString()
        {
            String str = null;
            int findHeight = number % 13;
            switch (findHeight)
            {
                case 0: 
                    str = "2";
                    break;
                case 1:
                    str = "3"; 
                    break;
                case 2:
                    str = "4";
                    break;
                case 3: 
                    str = "5"; 
                    break;
                case 4: 
                    str = "6"; 
                    break;
                case 5: 
                    str = "7"; 
                    break;
                case 6: 
                    str = "8"; 
                    break;
                case 7: 
                    str = "9"; 
                    break;
                case 8: 
                    str = "T"; 
                    break;
                case 9: 
                    str = "J"; 
                    break;
                case 10: 
                    str = "Q"; 
                    break;
                case 11: 
                    str = "K"; 
                    break;
                case 12: 
                    str = "A"; 
                    break;
            }
            int findSuit = number / 13;
            switch (findSuit)
            {
                case 0: 
                    str += "s"; 
                    break;
                case 1: 
                    str += "h"; 
                    break;
                case 2: 
                    str += "c"; 
                    break;
                default: 
                    str += "d"; 
                    break;
            }
            return str;
        }
    }
}
