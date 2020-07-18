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
        #region Properties
        //Instantiate GameOptions
        GameOptions GameOptions = new GameOptions();


        /// <summary>
        /// Dictionary of winners
        /// </summary>
        Dictionary<PlayerOption, PlayerOption> winners = new Dictionary<PlayerOption, PlayerOption>
        {
            { PlayerOption.Rock, PlayerOption.Scissors },
            { PlayerOption.Scissors, PlayerOption.Paper },
            { PlayerOption.Paper, PlayerOption.Rock }
        };
        #endregion

        /// <summary>
        /// Draws the end of the game
        /// </summary>
        public void DrawEnd(GameOptions GameOptions)
        {
            DrawGameBoard(GameOptions);
            Console.WriteLine("Thank you for playing the game.");
            Console.WriteLine("\r\n\r\nThe final score was:");
            Console.WriteLine($"\r\n{GameOptions.PlayerName}: {GameOptions.PlayerWins}");
            Console.WriteLine($"\r\nComputer: {GameOptions.ComputerWins}");

            if (GameOptions.PlayerWins > GameOptions.ComputerWins)
            {
                Console.WriteLine("\r\nCongratulations!, you have won this round.");
            }
            if (GameOptions.PlayerWins == GameOptions.ComputerWins)
            {
                Console.WriteLine("\r\nIts a tie!!!");
            }
            else
            {
                Console.WriteLine("\r\nBetter luck next time!.");
            }
                
            Console.Write("\r\nPress [Enter] to finish the game.");
            Console.ReadLine();
        }

        /// <summary>
        /// Draws the game board header
        /// </summary>
        public void DrawGameBoard(GameOptions GameOptions)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }  

            Console.Write("ROCK, PAPER, SCISSORS");
            Console.SetCursorPosition(GameOptions.ScoreXAxis, GameOptions.ScoreYAxis);
            Console.Write($"Scores: {GameOptions.PlayerName} Player: {FormatScore(GameOptions.PlayerWins)}  Computer: {FormatScore(GameOptions.ComputerWins)} ");
            Console.SetCursorPosition(0, GameOptions.ScoreYAxis + 1);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
                
            Console.SetCursorPosition(0, GameOptions.ScoreYAxis + 2);
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
        /// Gets the computer PlayerOption selection
        /// </summary>
        /// <returns></returns>
        public PlayerOption GetComputerPlayerOption()
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
        public GameOptions PlayRound(int roundNumber, GameOptions GameOptions)
        {

            DrawGameBoard(GameOptions);

            PlayerOption computerChoice = GetComputerPlayerOption();
            PlayerOption playerChoice = PlayerOption.Invalid;
            Console.WriteLine($"Round {roundNumber + 1}:");
            bool quit = false;
            while (playerChoice == PlayerOption.Invalid)
            {

                Console.Write("Please choose. R/r - Rock, P/p - Paper, S/s - Scissors, Q/q - Quit ");
                string choice = Console.ReadLine();
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

                if (quit)
                {
                    break;
                }                  
            }

            if (quit)
            {
                //set the property AnotherRound to false
                //exit the game
                GameOptions.AnotherRound = false;

                return GameOptions;
            }

            Console.WriteLine($"The computer choosed: {computerChoice}");
            Console.WriteLine($"You choosed: {playerChoice}");

            //Calaculate who will be the winner and increment the resutls
            GameOptions = CalculateWinner(playerChoice, computerChoice);

            Console.Write("Press [Enter] to start the next game.");
            Console.ReadLine();

            //set the porperty AnotherRound to true
            //to continue the game
            GameOptions.AnotherRound = true;

            return GameOptions;
        }

        /// <summary>
        /// Sets up a game board
        /// </summary>
        public GameOptions SetupGame(GameOptions GameOptions)
        {
            /*  Template
            *  Score: {playerName}: {playerWins, 2 spaces}      Computer: {computerWins, 2 spaces} 
            */

            int requiredWidth = "Score: ".Length +
                               GameOptions.PlayerName.Length +
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

            GameOptions.ScoreXAxis = right;
            GameOptions.ScoreYAxis = top;

            return GameOptions;
        }

        /// <summary>
        /// Calculates the winner
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        public GameOptions CalculateWinner(PlayerOption player, PlayerOption computer)
        {
            //tie game
            if (player == computer)
            {
                Console.WriteLine($"The game is a tie.");
                GameOptions.PlayerWins++;
                GameOptions.ComputerWins++;
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
                    GameOptions.PlayerWins++;
                }
                else
                {
                    Console.WriteLine($"Computer Wins. {computer} beats {player}.");
                    GameOptions.ComputerWins++;
                }
            }
            return GameOptions;
        }
    }
}
