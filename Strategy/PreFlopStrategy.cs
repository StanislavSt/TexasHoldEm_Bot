using TexasHoldEm.Poker;
using System.Linq;
namespace TexasHoldEm.Bot
{
    /// <summary>
    /// In this class we evaluate a starting hand
    /// </summary>
    public class PreFlopStrategy
    {
        //Im using a preflop poker chart for evaluation
        //https://tinyurl.com/je4sdav
        public static string StartingHandEvalute(BotState state, HandHoldem hand)
        {
                Card card = hand.GetCard(0);
                //Store the 2nd card in our hand
                Card othercard = hand.GetCard(1);
                // We have a pocket pair
                if (card.getHeight() == othercard.getHeight())
                {
                    //If we have 99 or higher we raise
                    if ((int)card.getHeight() > 7)
                        return "raise";
                    //If we have 88 or smaller , we flat call
                    else
                        return "call";
                }
                //If we have an Ace we always raise
                else if (card.getHeight() == CardHeight.ACE)
                {//If the opponent raised , we flat call , otherwise we raise
                    if (state.OpponentAction != null)
                    {
                        if (state.OpponentAction.getAction().Equals("raise"))
                            return "call";
                    }     
                    else
                        return "raise";
                }
                else if (card.getHeight() == CardHeight.KING)
                {
                    //IF we have K6 suited or better
                    if (card.getSuit() == othercard.getSuit() && (int)othercard.getHeight() > 6)
                        //If the opponent raised , we flat call , otherwise we raise
                        if (state.OpponentAction != null)
                        {
                            if (state.OpponentAction.getAction().Equals("raise"))
                                return "call";
                        }     
                        else 
                            return "raise";
                    else
                        return "fold";
                }
                 // We have suited connectors
                else if((int)card.getHeight() - (int)othercard.getHeight() < 2  && card.getSuit() == othercard.getSuit())
                {
                    //If the opponent raised , we flat call , otherwise we raise
                    if (state.OpponentAction != null)
                    {
                        if (state.OpponentAction.getAction().Equals("raise"))
                            return "call";
                    }
                    else
                        return "raise";
                }
                return "fold";
                   
        }
    }
}
