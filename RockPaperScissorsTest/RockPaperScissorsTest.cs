using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;
using RockPaperScissors.Domain;
using System.Runtime.InteropServices;

namespace RockPaperScissorsTest
{
    /// <summary>
    /// Summary description for RockPaperScissorsTest
    /// </summary>
    [TestClass]
    public class RockPaperScissorsTest
    {
        /// <summary>
        /// This test method will check and compare the player selection 
        /// </summary>
        [TestMethod]
        public void CheckPlayerSelection()
        {
            // R - Rock, P - Paper, S - Scissors, Q - Quit
            PlayerOption playerChoice = PlayerOption.Invalid;

            //declare string choice to check the player selection 
            //compare the string choice base to PlayerOption enum model
            //using switch case 
            string choice = "r";

            //use while loop 
            //if the playerChoice will be equals to invalid continue loop 
            //check the value of choice string by switch case and compare base on the case 
            while (playerChoice == PlayerOption.Invalid)
            {
                switch (choice.ToLowerInvariant().Trim())
                {
                    case "rock":
                    case "r":
                        playerChoice = PlayerOption.Rock;
                        break;
                    case "paper":
                    case "p":
                        playerChoice = PlayerOption.Paper;
                        break;
                    case "scissors":
                    case "s":
                        playerChoice = PlayerOption.Scissors;
                        break;

                    default:
                        //choice is not valid
                        break;
                }

                //call the CalculateWinner function 
            }

            //Nested if for assert results 
            if (playerChoice != PlayerOption.Invalid)
            {
                if (choice.Equals("r") || choice.Equals("rock"))
                {
                    Assert.AreEqual(playerChoice, PlayerOption.Rock);
                }

                if (choice.Equals("p") || choice.Equals("paper"))
                {
                    Assert.AreEqual(playerChoice, PlayerOption.Paper);
                }

                if (choice.Equals("s") || choice.Equals("scissors"))
                {
                    Assert.AreEqual(playerChoice, PlayerOption.Scissors);
                }
            }
        }

        /// <summary>
        /// This test method will check and compare the computer selection 
        /// </summary>
        [TestMethod]
        public void GetComputerSelection()
        {
            //declare random property
            Random random = new Random();

            int range = random.Next(0, 3);

            PlayerOption computerChoice;

            //Nested if checking the range and PlayerOption
            //if its rock, paper or scissors 
            if (range == (int)PlayerOption.Rock)
            {
                computerChoice = PlayerOption.Rock;
                Assert.AreEqual(computerChoice, PlayerOption.Rock);
            }

            if (range == (int)PlayerOption.Paper)
            {
                computerChoice = PlayerOption.Paper;
                Assert.AreEqual(computerChoice, PlayerOption.Paper);
            }

            if (range == (int)PlayerOption.Scissors)
            {
                computerChoice = PlayerOption.Scissors;
                Assert.AreEqual(computerChoice, PlayerOption.Scissors);
            }
        }

        [TestMethod]
        public void CalculateWinner()
        {
            /// <summary>
            /// Declare Dictionary for comparing of selection 
            /// </summary>
            Dictionary<PlayerOption, PlayerOption> winners = new Dictionary<PlayerOption, PlayerOption>
            {
                { PlayerOption.Rock, PlayerOption.Scissors },
                { PlayerOption.Scissors, PlayerOption.Paper },
                { PlayerOption.Paper, PlayerOption.Rock }
            };

            //Instantiate GameOptions
            GameOptions gameOptions = new GameOptions();

            //Manual declare both plaayerOption and computerOption 
            //From PlayerOption Enum 
            //May change the value of PlayerOption from Rock, Paper or Scissors
            PlayerOption computerOption = PlayerOption.Scissors;
            PlayerOption playerOption = PlayerOption.Scissors; 

            if(playerOption == computerOption)
            {
                //Its a tie 
                gameOptions.ComputerWins++;
                gameOptions.PlayerWins++;

                Assert.IsTrue(playerOption == computerOption);
            }
            else
            {
                //calculating the winner is simple, simply get the
                //winning combination for the player
                //if the result equals the computers roll then the player wins
                //otherwise the computer wins.
                // such as player calls rock, winners[rock] == scissors. If computer == scissors then 
                // player wins otherwise the computer wins as the only other option is paper 
                // remeber the options of the computer has a rock is negated in the tie selection
                var result = winners[playerOption];
                if (result == computerOption)
                {
                    //Congratulations you won beats computer
                    gameOptions.PlayerWins++;

                    Assert.IsTrue(result == computerOption);
                }
                else
                {
                    //Computer Wins beats player 
                    gameOptions.ComputerWins++;

                    Assert.IsTrue(result != computerOption);
                }
            }
        }
    }
}
