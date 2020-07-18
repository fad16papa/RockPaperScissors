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

        [TestMethod]
        public void CheckPlayerSelection()
        {
            //Instantiate GameOptionsModel
            GameOptionsModel gameOptionsModel = new GameOptionsModel();

            // R - Rock, P - Paper, S - Scissors, Q - Quit
            PlayerOptionModel playerChoice = PlayerOptionModel.Invalid;

            //declare string choice to check the player selection 
            //compare the string choice base to PlayerOptionModel enum model
            //using switch case 
            string choice = "r";

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
                        break;

                    default:
                        // choice is not valid
                        break;
                }
            }

            //Change the PlayerOptionModel to Rock, Paper and/or scissors to check if the assert result is correct
            Assert.AreEqual(playerChoice, PlayerOptionModel.Rock);
        }

        [TestMethod]
        public void CheckComputerSelection()
        { 
            
        }
    }
}
