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


            // We are playing preflop
            if (state.Table.Count == 0)
            {
                string action = StarterHandEval.StartingHandEvalute(state, hand);
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
                if(handCategory == HandCategory.Pair)
                    if(state.OpponentAction.getAction().Equals("raise"))
                        return new PokerMove(state.MyName, "call", state.AmountToCall);
                    else 
                        return new PokerMove(state.MyName, "raise",2 * state.Pot);
                //We have a highcard
                else
                {
                    if(StarterHandEval.StartingHandEvalute(state, hand) == "raise"
                        && state.AmountToCall < 5 * state.Pot)
                        return new PokerMove(state.MyName, "call", state.AmountToCall);
                    else if(!state.OpponentAction.getAction().Equals("raise"))
                        return new PokerMove(state.MyName, "check", 0);
                    else
                        return new PokerMove(state.MyName, "fold", 0);
                }

            }
            //We are playing after the turn
            //else if(state.Table.Count == 4)
            //{
            //    if (handCategory == HandCategory.Pair)
            //    {
            //        if (state.OpponentAction.getAction().Equals("raise"))
            //            return new PokerMove(state.MyName, "call", state.AmountToCall);
            //        else
            //            return new PokerMove(state.MyName, "raise", 2 * state.Pot);
            //    }
                    
            //    //We have a highcard
            //    else
            //    {
            //        if (StarterHandEval.StartingHandEvalute(state, hand) == "raise"
            //            && state.AmountToCall < 5 * state.Pot)
            //            return new PokerMove(state.MyName, "call", state.AmountToCall);
            //        if (!state.OpponentAction.getAction().Equals("raise"))
            //            return new PokerMove(state.MyName, "check", 0);
            //        else
            //            return new PokerMove(state.MyName, "fold", 0);
            //    } 
            //}
            ////We are playing after the river
            //else if(state.Table.Count == 5)
            //{
            //    if (handCategory == HandCategory.Pair)
            //        if (state.OpponentAction.getAction().Equals("raise"))
            //            return new PokerMove(state.MyName, "call", state.AmountToCall);
            //        else
            //            return new PokerMove(state.MyName, "raise", 2 * state.Pot);
            //    //We have a highcard
            //    else
            //    {
            //        if (StarterHandEval.StartingHandEvalute(state, hand) == "raise"
            //            && state.AmountToCall < 5 * state.Pot)
            //            return new PokerMove(state.MyName, "call", state.AmountToCall);
            //        if (!state.OpponentAction.getAction().Equals("raise"))
            //            return new PokerMove(state.MyName, "check", 0);
            //        else
            //            return new PokerMove(state.MyName, "fold", 0);
            //    } 
            //}  
        }
    }
}
