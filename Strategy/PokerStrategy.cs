using System.Linq;
using TexasHoldEm.Bot;
using TexasHoldEm.Enums;
using TexasHoldEm.Poker;
using System.Collections.Generic;

namespace TexasHoldEm.Strategy
{
    public class PokerStrategy : IBot
    {
        public HandCategory GetHandCategory(BotState state,HandHoldem hand)
        {
            return HandEval.Evaluate(state, hand);
        }
        public PokerMove GetMove(BotState state, long timeOut)
        {
            HandHoldem hand = state.Hand;
            var handCategory = GetHandCategory(state, hand);

            if(state.Table.Count > 0)
            {
                // Get the ordinal values of the cards in your hand
                int[] ordinalHand = { (int)hand.GetCard(0).height, (int)hand.GetCard(1).height };
                // Get the average ordinal value
                double averageOrdinalValue = ordinalHand[0] + ordinalHand[1];

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
            // We are playing preflop
            else
            {
                string action = StarterHandEval.StartingHandEvalute(state, hand);
                switch(action)
                {
                    case "call" :
                        return new PokerMove(state.MyName, action, state.AmountToCall);
                    case "raise" :
                        return new PokerMove(state.MyName, action, 3 * state.BigBlind);
                    case "fold" :
                        return new PokerMove(state.MyName, action, 0);
                    default :
                        return new PokerMove(state.MyName, "check", 0);
                }
            }     
        }
    }
}
