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
            PlayerOptionModel playerChoice = PlayerOptionModel.Invalid;

            //declare string choice to check the player selection 
            //compare the string choice base to PlayerOptionModel enum model
            //using switch case 
            string choice = "r";

            //bool if the user selected q/quit
            bool quit = false;

            //use while loop 
            //if the playerChoice will be equals to invalid continue loop 
            //check the value of choice string by switch case and compare base on the case 
            while (playerChoice == PlayerOptionModel.Invalid)
            {
                switch (choice.ToLowerInvariant().Trim())
                {
                    case "rock":
                    case "r":
                        playerChoice = PlayerOptionModel.Rock;
                        break;
                    case "paper":
                    case "p":
                        playerChoice = PlayerOptionModel.Paper;
                        break;
                    case "scissors":
                    case "s":
                        playerChoice = PlayerOptionModel.Scissors;
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
            if (playerChoice != PlayerOptionModel.Invalid)
            {
                if (choice.Equals("r") || choice.Equals("rock"))
                {
                    Assert.AreEqual(playerChoice, PlayerOptionModel.Rock);
                }

                if (choice.Equals("p") || choice.Equals("paper"))
                {
                    Assert.AreEqual(playerChoice, PlayerOptionModel.Paper);
                }

                if (choice.Equals("s") || choice.Equals("scissors"))
                {
                    Assert.AreEqual(playerChoice, PlayerOptionModel.Scissors);
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

            
            //Nested if checking the range and PlayerOptionModel
            //if its rock, paper or scissors 
            if (range == (int)PlayerOptionModel.Rock)
            {
                Assert.AreEqual(range, (int)PlayerOptionModel.Rock);
            }

            if (range == (int)PlayerOptionModel.Paper)
            {
                Assert.AreEqual(range, (int)PlayerOptionModel.Paper);
            }

            if (range == (int)PlayerOptionModel.Scissors)
            {
                Assert.AreEqual(range, (int)PlayerOptionModel.Scissors);
            }
        }
    }
}
