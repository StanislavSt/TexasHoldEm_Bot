using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm.Poker
{
    /// <summary>
    /// Represents a the move performed by a bot 
    /// </summary>
    public class PokerMove
    {
        string player = null;
        string action = null;
        int amount;

        public PokerMove(string botName, string act, int amt)
        {
            player = botName;
            action = act;
            amount = amt;
        }

        public string getPlayer()
        {
            return player;
        }

        public string getAction()
        {
            return action;
        }

        public int getAmount()
        {
            return amount;
        }

        /**
	 * Returns a string representation of the move as a sentence of two words, being the action
	 * string and the action amount. Returning the player name to the engine is not needed
	 */
        public String MoveString()
        {
            return String.Format("%s %d", action, amount);
        }
    }
}
