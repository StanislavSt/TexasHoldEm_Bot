using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldEm.Poker;
using TexasHoldEm.Enums;

namespace TexasHoldEm.Bot
{
    //http://codereview.stackexchange.com/questions/36841/poker-hand-evaluator-challenge
        public static class HandEval
        {
            
            public static HandCategory Evaluate(BotState state, HandHoldem hand)
            {
                //Merge your cards with the cards on the board
                IEnumerable<Card> CardsToEvaluateToEvaluate = hand.Cards.Concat(state.Table);
                return CheckRoyalFlush(CardsToEvaluateToEvaluate, hand);
            }
            private static HandCategory CheckRoyalFlush(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if (CardsToEvaluate
                    .Min(x => (int)x.getHeight()) == (int)CardHeight.TEN
                    && CheckStraightFlush(CardsToEvaluate, hand) == HandCategory.StraightFlush)
                    return HandCategory.RoyalFlush;
                else
                    return CheckStraightFlush(CardsToEvaluate, hand);
            }
            private static HandCategory CheckStraightFlush(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if (CheckFlush(CardsToEvaluate, hand) == HandCategory.Flush
                    && CheckStraight(CardsToEvaluate, hand) == HandCategory.Straight)
                    return HandCategory.StraightFlush;
                else
                    return CheckFourOfAKind(CardsToEvaluate, hand);
            }
            private static HandCategory CheckFourOfAKind(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if(CardsToEvaluate
                    .GroupBy(card => card.getHeight())
                    .Any(group => group.Count() == 4))
                    return HandCategory.FourOfAKind;
                else
                    return CheckFullHouse(CardsToEvaluate, hand);
            }
            private static HandCategory CheckFullHouse(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if (CheckPair(CardsToEvaluate, hand) == HandCategory.Pair 
                    && CheckThreeOfAKind(CardsToEvaluate, hand) == HandCategory.ThreeOfAKind)
                    return HandCategory.FullHouse;
                else
                    return CheckFlush(CardsToEvaluate, hand);
            }
            private static HandCategory CheckFlush(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if(CardsToEvaluate
                    .GroupBy(x => x.getSuit())
                    .Count()==1)
                    return HandCategory.Flush;
                else
                    return CheckStraight(CardsToEvaluate, hand);
            }
            private static HandCategory CheckStraight(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if (CardsToEvaluate
                    .GroupBy(x => x.getHeight())
                    .Count() >= 5
                    && CardsToEvaluate.Max(x => (int)x.getHeight())
                    - CardsToEvaluate.Min(x => (int)x.getHeight()) == 4)
                    return HandCategory.Straight;
                else
                    return CheckThreeOfAKind(CardsToEvaluate, hand);
            }
            private static HandCategory CheckThreeOfAKind(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if(CardsToEvaluate
                    .GroupBy(x => x.getHeight())
                    .Any(group => group.Count() == 3))
                    return HandCategory.ThreeOfAKind;
                else
                    return CheckTwoPair(CardsToEvaluate, hand);
            }
            private static HandCategory CheckTwoPair(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if(CardsToEvaluate
                    .GroupBy(x => x.getSuit())
                    .Count(group => group.Count() >= 2) > 1)
                    return HandCategory.TwoPair;
                else
                    return CheckPair(CardsToEvaluate, hand);
            }
            private static HandCategory CheckPair(IEnumerable<Card> CardsToEvaluate, HandHoldem hand)
            {
                if (CardsToEvaluate
                    .GroupBy(x => x.getHeight())
                    .Count(group => group.Count() == 2)==1)
                    return HandCategory.Pair;
                else 
                    return HandCategory.NoPair;
            }
        }
}
