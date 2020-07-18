using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Domain
{
    public class GameOptions
    {
        [DisplayName("Computer Wins")]
        public int ComputerWins { get; set; }

        [DisplayName("First COmputer Wins")]
        public int FirstComputerWins { get; set; }

        [DisplayName("Second COmputer Wins")]
        public int SecondComputerWins { get; set; }

        [DisplayName("Player Wins")]
        public int PlayerWins { get; set; }

        [DisplayName("Player Name")]
        public string PlayerName { get; set; }

        [DisplayName("Score X")]
        public int ScoreXAxis { get; set; }

        [DisplayName("Score Y")]
        public int ScoreYAxis { get; set; }

        [DisplayName("Number of Games")]
        public int NumberOfGames { get; set; }

        [DisplayName("Another Round")]
        public bool AnotherRound { get; set; }
        
        [DisplayName("Computer Only")]
        public bool computerOnly { get; set; }
    }
}
