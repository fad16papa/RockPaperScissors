using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class GameOptionsModel
    {
        public int computerWins { get; set; }
        public int firstComputerWins { get; set; }
        public int secondComputerWins { get; set; }
        public int playerWins { get; set; }
        public string playerName { get; set; }
        public int scoreX { get; set; }
        public int scoreY { get; set; }
        public int numberOfGames { get; set; }
    }
}
