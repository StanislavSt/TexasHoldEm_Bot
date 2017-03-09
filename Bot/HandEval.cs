using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldEm.Poker;
using TexasHoldEm.Enums;

namespace TexasHoldEm.Bot
{
        public static class HandEval
        {
            public static HandCategory Evaluate(BotState state, HandHoldem hand)
            {
                return CheckRoyalFlush(state, hand);
            }
            private static HandCategory CheckRoyalFlush(BotState state, HandHoldem hand)
            {
                return CheckStraightFlush(state, hand);
            }
            private static HandCategory CheckStraightFlush(BotState state, HandHoldem hand)
            {
                return CheckFourOfAKind(state, hand);
            }
            private static HandCategory CheckFourOfAKind(BotState state, HandHoldem hand)
            {
                return CheckFullHouse(state, hand);
            }
            private static HandCategory CheckFullHouse(BotState state, HandHoldem hand)
            {
                return CheckFlush(state, hand);
            }
            private static HandCategory CheckFlush(BotState state, HandHoldem hand)
            {
                return CheckStraight(state, hand);
            }
            private static HandCategory CheckStraight(BotState state, HandHoldem hand)
            {
                return CheckThreeOfAKind(state, hand);
            }
            private static HandCategory CheckThreeOfAKind(BotState state, HandHoldem hand)
            {
                return CheckTwoPair(state, hand);
            }
            private static HandCategory CheckTwoPair(BotState state, HandHoldem hand)
            {
                if(hand.Cards.Concat(state.Table)
                .GroupBy(x => x.getSuit())
                .Count(group => group.Count() == 2) > 1)
                    return HandCategory.TwoPair;
                else
                    return CheckPair(state, hand);
            }
            private static HandCategory CheckPair(BotState state, HandHoldem hand)
            {
                foreach (var card in hand.Cards)
                {
                    foreach (var otherCard in hand.Cards.Where(c => c != card).Union(state.Table))
                    {
                        if (card.height == otherCard.height)
                            return HandCategory.Pair;
                    }
                }
                return HandCategory.NoPair;
            }
        }
    }
    List<Cards>
