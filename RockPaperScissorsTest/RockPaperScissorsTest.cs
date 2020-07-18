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
        public RockPaperScissorsTest()
        {

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
            string choice = "paper";

            //declare bool if the user selected q/quit
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

        [TestMethod]
        public void CheckComputerSelection()
        { 
            
        }
    }
}
