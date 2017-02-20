using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm.Poker
{
    /// <summary>
    /// Holds the information about our hand.
    /// </summary>
    public abstract class Hand
    {
        public List<Card> Cards { get; protected set; }
        /// <summary>
        /// Returns an obj type Card  at the given index
        /// </summary>
        public Card GetCard(int index)
        {
            if (index >= 0 && index < this.Cards.Count)
                return this.Cards[index];
            return null;
        }
        public string HandString()
        {
            string str = "[";
            for (int i = 0; i < this.Cards.Count - 1; i++)
                str += this.Cards[i].toString() + ",";

            str += this.Cards[this.Cards.Count - 1].toString();
            return str;
        }
    }
}
