
using TexasHoldEm.Poker;

namespace TexasHoldEm.Bot
{
    public interface IBot
    {
        PokerMove GetMove(BotState state, long timeOut);
    }
}
