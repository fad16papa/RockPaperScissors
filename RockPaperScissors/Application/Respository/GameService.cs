using RockPaperScissors.Application.Interface;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Application.Respository
{
    public class GameService : IGameService
    {
        #region Properties
        //Instantiate GameOptionsModel
        GameOptionsModel gameOptionsModel = new GameOptionsModel();

        //declare random property
        static Random random = new Random();

        /// <summary>
        /// Dictionary of winners
        /// </summary>
        Dictionary<PlayerOptionModel, PlayerOptionModel> winners = new Dictionary<PlayerOptionModel, PlayerOptionModel>
        {
            { PlayerOptionModel.Rock, PlayerOptionModel.Scissors },
            { PlayerOptionModel.Scissors, PlayerOptionModel.Paper },
            { PlayerOptionModel.Paper, PlayerOptionModel.Rock }
        };
        #endregion

        /// <summary>
        /// Draws the end of the game
        /// </summary>
        public void DrawEnd(GameOptionsModel gameOptionsModel)
        {
            DrawGameBoard(gameOptionsModel);
            Console.WriteLine("Thank you for playing the game.");
            Console.WriteLine("\r\n\r\nThe final score was:");
            Console.WriteLine($"\r\n{gameOptionsModel.PlayerName}: {gameOptionsModel.PlayerWins}");
            Console.WriteLine($"\r\nComputer: {gameOptionsModel.ComputerWins}");

            if (gameOptionsModel.PlayerWins > gameOptionsModel.ComputerWins)
            {
                Console.WriteLine("\r\nCongratulations, you have won this round.");
            }
            else
            {
                Console.WriteLine("\r\nBetter luck next time.");
            }
                
            Console.Write("\r\nPress [Enter] to finish the game.");
            Console.ReadLine();
        }

        /// <summary>
        /// Draws the game board header
        /// </summary>
        public void DrawGameBoard(GameOptionsModel gameOptionsModel)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }  

            Console.Write("ROCK, PAPER, SCISSORS");
            Console.SetCursorPosition(gameOptionsModel.ScoreXAxis, gameOptionsModel.ScoreYAxis);
            Console.Write($"Scores: {gameOptionsModel.PlayerName} Player: {FormatScore(gameOptionsModel.PlayerWins)}  Computer: {FormatScore(gameOptionsModel.ComputerWins)} ");
            Console.SetCursorPosition(0, gameOptionsModel.ScoreYAxis + 1);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
                
            Console.SetCursorPosition(0, gameOptionsModel.ScoreYAxis + 2);
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

        /// <summary>
        /// Gets the computer PlayerOptionModel selection
        /// </summary>
        /// <returns></returns>
        public PlayerOptionModel GetComputerPlayerOptionModel()
        {
            int range = random.Next(0, 3);

            return (PlayerOptionModel)range;
        }

        /// <summary>
        /// Runs a player round
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <returns></returns>
        public GameOptionsModel PlayRound(int roundNumber, GameOptionsModel gameOptionsModel)
        {

            DrawGameBoard(gameOptionsModel);

            PlayerOptionModel computerChoice = GetComputerPlayerOptionModel();
            PlayerOptionModel playerChoice = PlayerOptionModel.Invalid;
            Console.WriteLine($"Round {roundNumber + 1}:");
            bool quit = false;
            while (playerChoice == PlayerOptionModel.Invalid)
            {

                Console.Write("Please choose. R - Rock, P - Paper, S - Scissors, Q - Quit ");
                string choice = Console.ReadLine();
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

                if (quit)
                {
                    break;
                }                  
            }

            if (quit)
            {
                //set the property AnotherRound to false
                //exit the game
                gameOptionsModel.AnotherRound = false;

                return gameOptionsModel;
            }

            Console.WriteLine($"The computer choosed: {computerChoice}");
            Console.WriteLine($"You choosed: {playerChoice}");

            //Calaculate who will be the winner and increment the resutls
            gameOptionsModel = CalculateWinner(playerChoice, computerChoice);

            Console.Write("Press [Enter] to start the next game.");
            Console.ReadLine();

            //set the porperty AnotherRound to true
            //to continue the game
            gameOptionsModel.AnotherRound = true;

            return gameOptionsModel;
        }

        /// <summary>
        /// Sets up a game board
        /// </summary>
        public GameOptionsModel SetupGame(GameOptionsModel gameOptionsModel)
        {
            /*  Template
            *  Score: {playerName}: {playerWins, 2 spaces}      Computer: {computerWins, 2 spaces} 
            */

            int requiredWidth = "Score: ".Length +
                               gameOptionsModel.PlayerName.Length +
                               1 +
                               1 +
                               4 +
                               4 +
                               "Computer".Length +
                               1 +
                               1 +
                               4 +
                               1;

            int right = Console.WindowWidth - requiredWidth;
            int top = 1;
            if (right < "ROCK, PAPER, SCISSORS...".Length)
                top++;

            gameOptionsModel.ScoreXAxis = right;
            gameOptionsModel.ScoreYAxis = top;

            return gameOptionsModel;
        }

        /// <summary>
        /// Calculates the winner
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        public GameOptionsModel CalculateWinner(PlayerOptionModel player, PlayerOptionModel computer)
        {
            //tie game
            if (player == computer)
            {
                Console.WriteLine($"The game is a tie.");
                gameOptionsModel.PlayerWins++;
                gameOptionsModel.ComputerWins++;
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
                var choice = winners[player];
                if (choice == computer)
                {
                    Console.WriteLine($"Congratulations you won. {player} beats {computer}.");
                    gameOptionsModel.PlayerWins++;
                }
                else
                {
                    Console.WriteLine($"Computer Wins. {computer} beats {player}.");
                    gameOptionsModel.ComputerWins++;
                }
            }
            return gameOptionsModel;
        }
    }
}
