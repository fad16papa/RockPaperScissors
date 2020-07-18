using RockPaperScissors.Application.Interface;
using RockPaperScissors.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Application.Respository
{
    public class GameService : IGameService
    {
        /// <summary>
        /// Sets up a game board
        /// </summary>
        public GameOptions SetupGame(GameOptions gameOptions)
        {
            /*  Template
            *  Score: {playerName}: {playerWins, 2 spaces}      Computer: {computerWins, 2 spaces} 
            */
            int requiredWidth = 0;
            if (gameOptions.computerOnly)
            {
                requiredWidth = "Score: ".Length +
                  "Computer".Length +
                  1 +
                  1 +
                  4 +
                  4 +
                  "Computer".Length +
                  1 +
                  1 +
                  4 +
                  1;
            }
            else
            {
                requiredWidth = "Score: ".Length +
                  gameOptions.PlayerName.Length +
                  1 +
                  1 +
                  4 +
                  4 +
                  "Computer".Length +
                  1 +
                  1 +
                  4 +
                  1;
            }



            int right = Console.WindowWidth - requiredWidth;
            int top = 1;
            if (right < "ROCK, PAPER, SCISSORS...".Length)
                top++;

            gameOptions.ScoreXAxis = right;
            gameOptions.ScoreYAxis = top;

            return gameOptions;
        }

        /// <summary>
        /// Draws the game board header
        /// </summary>
        public void DrawGameBoard(GameOptions gameOptions)
        {
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
                Console.Write($"Scores: {gameOptions.PlayerName} Player: {FormatScore(gameOptions.PlayerWins)}  Computer: {FormatScore(gameOptions.ComputerWins)} ");
            }
            
            Console.SetCursorPosition(0, gameOptions.ScoreYAxis + 1);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
                
            Console.SetCursorPosition(0, gameOptions.ScoreYAxis + 2);
        }

        /// <summary>
        /// Draws the end of the game
        /// </summary>
        public void DrawEnd(GameOptions gameOptions)
        {
            DrawGameBoard(gameOptions);
            Console.WriteLine("Thank you for playing the game.");
            Console.WriteLine("\r\n\r\nThe final score was:");

            if(gameOptions.computerOnly)
            {
                Console.WriteLine($"\r\nComputer1: {gameOptions.FirstComputerWins}");
                Console.WriteLine($"\r\nComputer2: {gameOptions.SecondComputerWins}");
            }
            else
            {
                Console.WriteLine($"\r\n{gameOptions.PlayerName}: {gameOptions.PlayerWins}");
                Console.WriteLine($"\r\nComputer: {gameOptions.ComputerWins}");
            }
            

            if (gameOptions.PlayerWins > gameOptions.ComputerWins)
            {
                Console.WriteLine("\r\nCongratulations!, you have won this game.");
            }
            if (gameOptions.PlayerWins == gameOptions.ComputerWins)
            {
                Console.WriteLine("\r\nIts a tie!!!");
            }
            if(gameOptions.PlayerWins < gameOptions.ComputerWins)
            {
                Console.WriteLine("\r\nYou lost! Better luck next time!.");
            }

            Console.Write("\r\nPress [Enter] to finish the game.");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets the current player PlayerOption selection
        /// </summary>
        /// <returns></returns>
        public PlayerOption GetPlayerSelection()
        {
            PlayerOption playerOption = PlayerOption.Invalid;
            while (playerOption == PlayerOption.Invalid)
            {
                Console.Write("Please choose. R/r - Rock, P/p - Paper, S/s - Scissors ");
                string choice = Console.ReadLine();
                switch (choice.ToLowerInvariant().Trim())
                {
                    case "rock":
                    case "r":
                        playerOption = PlayerOption.Rock;
                        break;
                    case "paper":
                    case "p":
                        playerOption = PlayerOption.Paper;
                        break;
                    case "scissors":
                    case "s":
                        playerOption = PlayerOption.Scissors;
                        break;

                    default:
                        Console.WriteLine($"{choice} is not a valid. Please try again.\r\n");
                        break;
                }
            }

            return playerOption;
        }

        /// <summary>
        /// Gets the computer PlayerOption selection
        /// </summary>
        /// <returns></returns>
        public PlayerOption GetComputerSelection()
        {
            Random random = new Random();

            int range = random.Next(0, 3);

            return (PlayerOption)range;
        }

        /// <summary>
        /// Runs a player round
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <returns></returns>
        public GameOptions PlayRound(GameOptions gameOptions)
        {

            DrawGameBoard(gameOptions);

            if (gameOptions.computerOnly)
            {
                //Calculate who will be the winner computer vs computer
                PlayerOption firstComputerChoice = GetComputerSelection();
                PlayerOption secondComputerChoice = GetComputerSelection();               

                gameOptions = CalculateWinner(firstComputerChoice, secondComputerChoice, gameOptions);
                gameOptions.computerOnly = true;
            }
            else
            { 

                //Calaculate who will be the winner player vs computer 
                PlayerOption computerChoice = GetComputerSelection();
                Console.WriteLine($"The computer choosed: {computerChoice}");

                PlayerOption playerChoice = GetPlayerSelection();
                Console.WriteLine($"You choosed: {playerChoice}");               

                gameOptions = CalculateWinner(playerChoice, computerChoice, gameOptions);
                gameOptions.computerOnly = false;
            }
            
            Console.Write("Press [Enter] to exit the game.");
            Console.ReadLine();

            return gameOptions;
        }

        /// <summary>
        /// Calculates the winner
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        public GameOptions CalculateWinner(PlayerOption playerOption, PlayerOption computerOption, GameOptions gameOptions)
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

            //if true the game will play computer vs computer 
            //if false the game will play player vs computer
            if (gameOptions.computerOnly)
            {
                //tie game
                if (playerOption == computerOption)
                {
                    Console.WriteLine($"The game is a tie.");
                    gameOptions.FirstComputerWins++;
                    gameOptions.SecondComputerWins++;
                }
                else
                {
                    //if the result equals the computers roll then the player wins
                    //otherwise the computer wins.
                    var result = winners[playerOption];
                    if (result == computerOption)
                    {
                        Console.WriteLine($"Computer1 wins {playerOption} beats {computerOption}.");
                        gameOptions.FirstComputerWins++;
                    }
                    else
                    {
                        Console.WriteLine($"Computer2 wins {computerOption} beats {playerOption}.");
                        gameOptions.SecondComputerWins++;
                    }
                }
            }
            else
            {
                //tie game
                if (playerOption == computerOption)
                {
                    Console.WriteLine($"The game is a tie.");
                    gameOptions.PlayerWins++;
                    gameOptions.ComputerWins++;
                }
                else
                {
                    //if the result equals the computers roll then the player wins
                    //otherwise the computer wins.
                    var result = winners[playerOption];
                    if (result == computerOption)
                    {
                        Console.WriteLine($"Congratulations you won. {playerOption} beats {computerOption}.");
                        gameOptions.PlayerWins++;
                    }
                    else
                    {
                        Console.WriteLine($"Computer Wins. {computerOption} beats {playerOption}.");
                        gameOptions.ComputerWins++;
                    }
                }
            }

            return gameOptions;
        }

        /// <summary>
        /// Simple format method to convert the score into a fixed length 
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public string FormatScore(int score)
        {
            string format = "   " + score.ToString();
            return format.Substring(format.Length - 2);
        }
    }
}
