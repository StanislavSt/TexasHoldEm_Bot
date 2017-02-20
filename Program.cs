using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldEm.Bot;
using TexasHoldEm.Strategy;

namespace TexasHoldEm
{
    class Program
    {
        static void Main(string[] args)
        {
            var session = new Session(new PokerStrategy());
            session.Run();
        }
    }
}
