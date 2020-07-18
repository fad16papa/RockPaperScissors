using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;
using RockPaperScissors.Domain;

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

            //bool if the user selected q/quit
            bool quit = false;

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
                    case "quit":
                    case "q":
                        quit = true;
                        break;

                    default:
                        Console.WriteLine($"{choice} is not a valid. Please try again.\r\n");
                        break;
                }

                //exit the loop if the player selected q/quit
                if (quit)
                {
                    break;
                }
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

            if (choice.Equals("q") || choice.Equals("quit"))
            {
                Assert.IsTrue(quit);
            }
        }

        /// <summary>
        /// This test method will check and compare the computer selection 
        /// </summary>
        [TestMethod]
        public void CheckComputerSelection()
        {
            //declare random property
            Random random = new Random();

            int range = random.Next(0, 3);

            
            //Nested if checking the range and PlayerOption
            //if its rock, paper or scissors 
            if (range == (int)PlayerOption.Rock)
            {
                Assert.AreEqual(range, (int)PlayerOption.Rock);
            }

            if (range == (int)PlayerOption.Paper)
            {
                Assert.AreEqual(range, (int)PlayerOption.Paper);
            }

            if (range == (int)PlayerOption.Scissors)
            {
                Assert.AreEqual(range, (int)PlayerOption.Scissors);
            }
        }

        [TestMethod]
        public void CalculateWinner()
        { 
            
        }
    }
}
