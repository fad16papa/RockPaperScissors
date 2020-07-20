using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors.Domain;
using RockPaperScissors.Helper;
using RockPaperScissors.Application.Respository;

namespace RockPaperScissorsTest
{
    /// <summary>
    /// Summary description for RockPaperScissorsTest
    /// </summary>
    [TestClass]
    public class RockPaperScissorsTest
    {
        [TestMethod]
        public void Drawboard()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.FirstComputerWins++;
            gameOptions.SecondComputerWins++;
            gameOptions.PlayerName = "fad";
            gameOptions.ScoreXAxis = 85;
            gameOptions.ScoreYAxis = 1;
            gameOptions.PlayerWins++;
            gameOptions.ComputerWins++;
            gameOptions.computerOnly = true;

            Console.Clear();
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }

            Console.Write("ROCK, PAPER, SCISSORS");
            Console.SetCursorPosition(gameOptions.ScoreXAxis, gameOptions.ScoreYAxis);
            if (gameOptions.computerOnly)
            {
                Console.Write($"Scores: Computer1: {FormatScore(gameOptions.FirstComputerWins)} Computer2: {FormatScore(gameOptions.SecondComputerWins)} ");
            }
            else
            {
                Console.Write($"Scores: {gameOptions.PlayerName}: {FormatScore(gameOptions.PlayerWins)}  Computer: {FormatScore(gameOptions.ComputerWins)} ");
            }

            Console.SetCursorPosition(0, gameOptions.ScoreYAxis + 1);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }

            Console.SetCursorPosition(0, gameOptions.ScoreYAxis + 2);
        }

        [TestMethod]
        public string FormatScore(int score)
        {
            string format = "   " + score.ToString();
            return format.Substring(format.Length - 2);
        }

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
        public PlayerOption GetComputerSelection()
        {
            PlayerOption computerSelection = EnumHelper.Of<PlayerOption>();

            while (computerSelection == PlayerOption.Invalid)
            {
                if (computerSelection != PlayerOption.Invalid)
                {
                    break;
                }
                else
                {
                    computerSelection = EnumHelper.Of<PlayerOption>();
                }
            }
            Console.WriteLine(computerSelection);
            Assert.AreNotEqual(computerSelection, PlayerOption.Invalid);

            return computerSelection;
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

            //if true the game will play computer vs computer 
            //if false the game will play player vs computer
            gameOptions.computerOnly = false;

          
            if (gameOptions.computerOnly)
            {
                PlayerOption firstComputerChoice = GetComputerSelection();
                Console.WriteLine($"The computer choosed: {firstComputerChoice}");

                PlayerOption secondComputerChoice = GetComputerSelection();
                Console.WriteLine($"The computer choosed: {secondComputerChoice}");


                //tie game
                if (firstComputerChoice == secondComputerChoice)
                {
                    
                    gameOptions.FirstComputerWins++;
                    gameOptions.SecondComputerWins++;
                    Console.WriteLine($"The game is a tie.");
                    Assert.IsTrue(firstComputerChoice == secondComputerChoice);
                }
                else
                {
                    //if the result equals the computers roll then the player wins
                    //otherwise the computer wins.
                    var result = winners[firstComputerChoice];
                    if (result == secondComputerChoice)
                    {
                        gameOptions.FirstComputerWins++;
                        Console.WriteLine($"Computer1 wins {firstComputerChoice} beats {secondComputerChoice}.");
                        Assert.IsTrue(result == secondComputerChoice);
                    }
                    else
                    {
                        gameOptions.SecondComputerWins++;
                        Console.WriteLine($"Computer2 wins {secondComputerChoice} beats {firstComputerChoice}.");
                        Assert.IsTrue(result == secondComputerChoice);
                    }
                }
            }
            else
            {
                PlayerOption computerOption = EnumHelper.Of<PlayerOption>();
                PlayerOption playerOption = EnumHelper.Of<PlayerOption>();

                //tie game
                if (playerOption == computerOption)
                {
                    gameOptions.PlayerWins++;
                    gameOptions.ComputerWins++;
                    Console.WriteLine($"The game is a tie.");
                    Assert.IsTrue(playerOption == computerOption);
                }
                else
                {
                    //if the result equals the computers roll then the player wins
                    //otherwise the computer wins.
                    var result = winners[playerOption];
                    if (result == computerOption)
                    {
                        gameOptions.PlayerWins++;
                        Console.WriteLine($"Congratulations you won. {playerOption} beats {computerOption}.");
                        Assert.IsTrue(result == computerOption);
                    }
                    else
                    {
                        gameOptions.ComputerWins++;
                        Console.WriteLine($"Computer Wins. {computerOption} beats {playerOption}.");
                        Assert.IsTrue(result != computerOption);
                    }
                }
            }
        }

        [TestMethod]
        public void PlayGame()
        {
            GameOptions gameOptions = new GameOptions();
            GameService gameService = new GameService();

            if (gameOptions.computerOnly)
            {
                //Calculate who will be the winner computer vs computer
                PlayerOption firstComputerChoice = GetComputerSelection();
                Console.WriteLine($"The Computer1 choosed: {firstComputerChoice}");

                PlayerOption secondComputerChoice = GetComputerSelection();
                Console.WriteLine($"The Computer2 choosed: {secondComputerChoice}");

                gameOptions = gameService.CalculateWinner(firstComputerChoice, secondComputerChoice, gameOptions);
                gameOptions.computerOnly = true;

                if(gameOptions.FirstComputerWins != 0)
                {
                    Assert.AreEqual(gameOptions.FirstComputerWins, 1);
                }
                if(gameOptions.SecondComputerWins != 0)
                {
                    Assert.AreEqual(gameOptions.SecondComputerWins, 1);
                }
            }
            else
            {
                //Calaculate who will be the winner player vs computer 
                PlayerOption computerChoice = GetComputerSelection();
                PlayerOption playerChoice = GetComputerSelection();

                //show the computer selected
                Console.WriteLine($"The computer choosed: {computerChoice}");
                //show the player selected 
                Console.WriteLine($"You choosed: {playerChoice}");

                gameOptions = gameService.CalculateWinner(playerChoice, computerChoice, gameOptions);
                gameOptions.computerOnly = false;

                if (gameOptions.PlayerWins != 0)
                {
                    Assert.AreEqual(gameOptions.PlayerWins, 1);
                }
                if (gameOptions.ComputerWins != 0)
                {
                    Assert.AreEqual(gameOptions.ComputerWins, 1);
                }
            }
        }
    }
}
