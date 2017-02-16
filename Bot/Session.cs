using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TexasHoldEm.Poker;

namespace TexasHoldEm.Bot
{
    public class Session
    {
        public class BotParser
    {
        private readonly IBot _bot;

        public BotParser(IBot bot)
        {
            this._bot = bot;
        }
        public void Run()
        {
            var currentState = new BotState();
            while (true)
            {
                var line = Console.ReadLine();
                if (line == null)
                    break;

                line = line.Trim();

                if (line.Length == 0)
                {
                    continue;
                }
                var parts = line.Split(' ');
                if (parts.Length == 3 && parts[0].Equals("Action"))
                {
                    // we need to move
                    PokerMove move = this._bot.GetMove(currentState, long.Parse(parts[2]));
                    Console.WriteLine(move.MoveString());
                }
                else if (parts.Length == 3 && parts[0].Equals("Settings"))
                {
                    // Update the state with settings info
                    currentState.UpdateSettings(parts[1], parts[2]);
                }
                else if (parts.Length == 3 && parts[0].Equals("Match"))
                {
                    // Update the state with match info
                    currentState.UpdateMatch(parts[1], parts[2]);
                }
                else if (parts.Length == 3 && parts[0].StartsWith("player"))
                {
                    // Update the state with info about the moves
                    currentState.UpdateMove(parts[0], parts[1], parts[2]);
                }
                else
                {
                    Console.Error.WriteLine("Unable to parse line {0}", line);
                }
            }
        }
    }
    }
}
