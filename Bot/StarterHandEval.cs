using TexasHoldEm.Poker;
using System.Linq;
namespace TexasHoldEm.Bot
{
    public class StarterHandEval
    {
        //Im using a preflop poker chart for evaluation
        //https://tinyurl.com/je4sdav
        public static string StartingHandEvalute(BotState state, HandHoldem hand)
        {
            foreach (var card in hand.Cards)
            {
                Card othercard = hand.Cards.Single(x => x != card);
                // We have a pocket pair
                if (card.getHeight() == othercard.getHeight())
                {
                    //If we have 99 or higher we raise
                    if ((int)card.getHeight() > 7)
                        return "raise";
                    //Pocket pair is 88 or smaller , we flat call
                    else
                        return "call";
                }
                //If we have an Ace we always raise
                else if (card.getHeight() == CardHeight.ACE)
                {
                    return "raise";
                }
                else if (card.getHeight() == CardHeight.KING)
                {
                    //if we have K6s > we raise 
                    if (card.getSuit() == othercard.getSuit() && (int)othercard.getHeight() > 6)
                        return "raise";
                    else
                        return "fold";
                }
                 // We have suited connectors
                else if((int)card.getHeight() - (int)othercard.getHeight() < 2  && card.getSuit() == othercard.getSuit())
                {
                    return "call";
                }
            }
            return "fold";
        }
    }
}
