using System;
using TexasHoldEm.Poker;

namespace TexasHoldEm.Bot
{
    public class Session
    {
        private readonly IBot _bot;

        public Session(IBot bot)
        {
            this._bot = bot;
        }
        /// <summary>
        /// Runs the bot and starts waiting for input
        /// </summary>
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

                switch(parts[0])
                {
                    case "Action" :
                        // we need to move
                        PokerMove move = this._bot.GetMove(currentState, long.Parse(parts[2]));
                        Console.WriteLine(move.MoveString());
                        break;                   
                    case "Settings" :
                        // Update the state with settings info
                        currentState.UpdateSettings(parts[1], parts[2]);
                        break;
                    case "Match" :
                        // Update the state with match info
                        currentState.UpdateMatch(parts[1], parts[2]);
                        break;
                    case "player1" : 
                        // Update the state with info about the moves
                        currentState.UpdateMove(parts[0], parts[1], parts[2]);
                        break;
                    case "player2" :
                        // Update the state with info about the moves
                        currentState.UpdateMove(parts[0], parts[1], parts[2]);
                        break;
                    default : 
                        Console.Error.WriteLine("Unable to parse line {0}", line);
                        break;
                }
            }
        }
    }
}

