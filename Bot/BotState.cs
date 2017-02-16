using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldEm.Poker;

namespace TexasHoldEm.Bot
{
    public class BotState
    {
        private int _timebank;
        private int _handsPerLevel;
        private int _timePerMove;
        public int Round { get; private set; }
        public int SmallBlind { get; private set; }
        public int BigBlind { get; private set; }
        public bool OnButton { get; private set; }
        public int MyStack { get; private set; }
        public int OpponentStack { get; private set; }
        public int Pot { get; private set; }
        public PokerMove OpponentAction { get; private set; }
        public int CurrentBet { get; private set; }
        public HandHoldem Hand { get; private set; }
        public List<Card> Table { get; private set; }

        public List<int> Sidepots { get; private set; }
        public string MyName { get; private set; }
        public int AmountToCall { get; private set; }
        public string GetSettings(string key)
        {
            return this._settings[key];
        }

        private readonly Dictionary<string, string> _settings = new Dictionary<string, string>();
        public BotState()
        {
            this.MyName = string.Empty;
            this.Sidepots = new List<int>();
            this.Table = new List<Card>();
        }
        /// <summary>
        /// Reset all variables for the new round
        /// </summary>
        private void ResetRoundVariables()
        {
            this.SmallBlind = 0;
            this.BigBlind = 0;
            this.Pot = 0;
            this.OpponentAction = null;
            this.AmountToCall = 0;
            this.Hand = null;
            this.Table = new List<Card>();
        }
        /// <summary>
        /// Parse the input from the game to Card obj
        /// </summary>
        /// <param name="value">input</param>
        /// <returns>List of Card obj</returns>
        private List<Card> ParseCards(string value)
        {
            if (value.StartsWith("[") && value.EndsWith("]"))
                value = value.Trim('[',']');
            if (value.Length == 0)
                return new List<Card>();

            var parts = value.Split(',');
            var cards = new List<Card>();
            foreach(var part in parts)
            {
                cards.Add(Card.getCard(part));
            }
            return cards;
        }
        /// <summary>
        /// Updates the settings for this game
        /// </summary>
        /// <param name="key">key of the information given</param>
        /// <param name="value">value to be set for the key</param>
        public void UpdateSettings(string key, string value)
        {
            this._settings.Add(key, value);
            switch(key)
            {
                //Bot ID
                case "your_bot":
                    this.MyName = value;
                    break;
                //Your time bank starts ticking away, after your time_per_move has finished
                case "time_bank":
                    this._timebank = int.Parse(value);
                    break;
                //Amount of time you get for each move
                case "time_per_move":
                    this._timePerMove = int.Parse(value);
                    break;
                //Number of hands, before the blind is increased
                case "hands_per_level" :
                    this._handsPerLevel = int.Parse(value);
                    break;
                //Starting stack each bot
                case "starting_stack":
                    this.MyStack = int.Parse(value);
                    this.OpponentStack = int.Parse(value);
                    break;
                //If there is no such key
                default:
                    Console.WriteLine("Uknown settings combination : {0} {1}",key,value);
                    break;
            }
        }
        /// <summary>
        /// Parses the match infromation
        /// </summary>
        /// <param name="key">key of the information</param>
        /// <param name="value">value to be set for the given key</param>
        public void UpdateMatch(string key, string value)
        {
            switch(key)
            {
                //Round number
                case "round":
                    this.Round = int.Parse(value);
                    this.ResetRoundVariables();
                    break;
                //Small blind price
                case "small_blind":
                    this.SmallBlind = int.Parse(value);
                    break;
                //Big blind price
                case "big_blind":
                    this.BigBlind = int.Parse(value);
                    break;
                //Which bot is the dealer 
                case "on_button" :
                    this.OnButton = value.Equals(this.MyName);
                    break;
                //Size of the current pot
                case "max_win_pot":
                    this.Pot = int.Parse(value);
                    break;
                //How much you need to call
                case "amount_to_call":
                    this.AmountToCall = int.Parse(value);
                    break;
                //Cards on the board
                case "table":
                    this.Table = this.ParseCards(value);
                    break;
                default :
                    Console.Error.WriteLine("Unknown match command: {0} {1}", key, value);
                    break;
            }
        }

        /// <summary>
        /// Parses tbhe information about blinds,stacks and moves
        /// </summary>
        /// <param name="bot">Which bot is performing the move</param>
        /// <param name="key">key of the given information</param>
        /// <param name="amount">value to be set for the key</param>
        public void UpdateMove(string bot, string key, string amount)
        {
            //Our bot is performing the move
            if(bot.Equals(this.MyName))
            {
                switch(key)
                {
                    //The amount of your starting stack
                    case "stack":
                        this.MyStack = int.Parse(amount);
                        break;
                    //Pay for the blind
                    case "post":
                        this.MyStack -= int.Parse(amount);
                        break;
                    //Your hand
                    case "hand":
                        var cards = this.ParseCards(amount);
                        this.Hand = new HandHoldem(cards[0], cards[1]);
                        break;
                    //Your winnings
                    case "wins":
                        //To do: if you want to store your winnings
                        break;
                }
            }
            //Its the opponent
            else
            {
                switch (key)
                {
                    //The amount of your oppent's starting stack
                    case "stack":
                        this.OpponentStack = int.Parse(amount);
                        break;
                    //Pay for the blind
                    case "post":
                        this.OpponentStack -= int.Parse(amount);
                        break;
                    //Hand of opponent on showdown
                    case "hand":
                        //To do : if you want to store the hand
                        break;
                    case "wins":
                        //To do: if you want to store opponent winnings
                        break;
                    //The move your opponent did
                    default :
                        this.OpponentAction = new PokerMove(bot, key, int.Parse(amount));
                        break;
                }
            }
        }
    }
}
