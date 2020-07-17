using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class GameOptionsModel
    {
        public int ComputerWins { get; set; }
        public int FirstComputerWins { get; set; }
        public int SecondComputerWins { get; set; }
        public int PlayerWins { get; set; }
        public string PlayerName { get; set; }
        public int ScoreX { get; set; }
        public int ScoreY { get; set; }
        public int NumberOfGames { get; set; }
        public bool AnotherRound { get; set; }
    }
}
