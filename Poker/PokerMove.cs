using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm.Poker
{
    public class PokerMove
    {
        string player = null;
        string action = null;
        int amount;
        /// <summary>
        /// Represents a the move performed by a bot 
        /// </summary>
        /// <param name="botName">Which bot is doing the move</param>
        /// <param name="act">What move</param>
        /// <param name="amt">Chips amount</param>
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
        /// <summary>
        /// Returns a string representation of the move as a sentence of two words, 
        /// being the action string and the action amount. 
        /// </summary>
        public String MoveString()
        {
            return String.Format("{0} {1}", this.action, this.amount);
        }
    }
}
