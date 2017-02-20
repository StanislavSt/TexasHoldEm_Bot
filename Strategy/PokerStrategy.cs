using System.Linq;
using TexasHoldEm.Bot;
using TexasHoldEm.Enums;
using TexasHoldEm.Poker;

namespace TexasHoldEm.Strategy
{
    public class PokerStrategy : IBot
    {
        public HandCategory GetHandCategory(BotState state,HandHoldem hand)
        {
            return StarterHandEval.Evaluate(state, hand);
        }
        public PokerMove GetMove(BotState state, long timeOut)
        {
            HandHoldem hand = state.Hand;
            var handCategory = GetHandCategory(state, hand);

            // Get the ordinal values of the cards in your hand
            int[] ordinalHand ={ (int)hand.GetCard(0).height, (int)hand.GetCard(1).height };
            // Get the average ordinal value
            double averageOrdinalValue = ordinalHand.Aggregate<int, double>(0, (current, t) => current + t);

            averageOrdinalValue /= ordinalHand.Length;

            // Return the appropriate move according to our amazing strategy
            if (averageOrdinalValue >= 9)
            {
                return new PokerMove(state.MyName, "raise", 2 * state.BigBlind);
            }
            if (averageOrdinalValue >= 5)
            {
                return new PokerMove(state.MyName, "call", state.AmountToCall);
            }
            return new PokerMove(state.MyName, "check", 0);
        }
    }
}
