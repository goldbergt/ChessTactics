using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTactics
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (ChessTactics game = new ChessTactics())
                game.Run();
        }
    }
#endif
}
