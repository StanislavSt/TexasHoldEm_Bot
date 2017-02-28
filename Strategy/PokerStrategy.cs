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
        public PokerMove EvaluateBoard(BotState state, HandHoldem hand)
        {
            var handCategory = GetHandCategory(state, hand);

            if (handCategory == HandCategory.Pair)
            {
                if (state.OpponentAction.getAction().Equals("raise"))
                    return new PokerMove(state.MyName, "call", state.AmountToCall);
                else
                    return new PokerMove(state.MyName, "raise", 2 * state.Pot);
            }
            //We have a highcard
            else
            {
                if (PreFlopStrategy.StartingHandEvalute(state, hand) == "raise"
                    && state.AmountToCall < 5 * state.Pot)
                    return new PokerMove(state.MyName, "call", state.AmountToCall);
                else if (!state.OpponentAction.getAction().Equals("raise"))
                    return new PokerMove(state.MyName, "check", 0);
                else if (state.OpponentAction.getAction().Equals("raise") && state.AmountToCall > state.Pot)
                    return new PokerMove(state.MyName, "fold", 0);
            }
            return new PokerMove(state.MyName, "check", 0);
        }
        /// <summary>
        /// Askins the bot to perform a move, depending on the current state of the game
        /// </summary>
        /// <param name="state">Current state of the game</param>
        /// <param name="timeOut">Represents the time we have to perform a move</param>
        /// <returns>Returns a PokerMove</returns>
        public PokerMove GetMove(BotState state, long timeOut)
        {
            HandHoldem hand = state.Hand;
            // We are playing preflop
            if (state.Table.Count == 0)
            {
                string action = PreFlopStrategy.StartingHandEvalute(state, hand);
                switch (action)
                {
                    case "call":
                        return new PokerMove(state.MyName, action, state.AmountToCall);
                    case "raise":
                        return new PokerMove(state.MyName, action, 3 * state.BigBlind);
                    case "fold":
                        return new PokerMove(state.MyName, action, 0);
                    default:
                        return new PokerMove(state.MyName, "check", 0);
                }
            }  
            //We are playing after the flop
            else if(state.Table.Count == 3)
            {
                return EvaluateBoard(state, hand);
            }
            //We are playing after the turn
            else if(state.Table.Count == 4)
            {
                return EvaluateBoard(state, hand);
            }
            //We are playing after the river
            else if(state.Table.Count == 5)
            {
                return EvaluateBoard(state, hand);
            }
            return new PokerMove(state.MyName, "check", 0);
        }
    }
}
