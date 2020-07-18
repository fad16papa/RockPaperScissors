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
        #endregion

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

            GameOptions.ScoreXAxis = right;
            GameOptions.ScoreYAxis = top;

            return GameOptions;
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
            if (GameOptions.computerOnly)
            {
                Console.Write($"Scores: Computer 1: {FormatScore(GameOptions.FirstComputerWins)} Computer 2: {FormatScore(GameOptions.SecondComputerWins)} ");
            }
            else
            {
                Console.Write($"Scores: {GameOptions.PlayerName} Player: {FormatScore(GameOptions.PlayerWins)}  Computer: {FormatScore(GameOptions.ComputerWins)} ");
            }
            
            Console.SetCursorPosition(0, GameOptions.ScoreYAxis + 1);

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
                
            Console.SetCursorPosition(0, GameOptions.ScoreYAxis + 2);
        }

        /// <summary>
        /// Draws the end of the game
        /// </summary>
        public void DrawEnd(GameOptions gameOptions)
        {
            DrawGameBoard(gameOptions);
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
            if(GameOptions.PlayerWins < GameOptions.ComputerWins)
            {
                Console.WriteLine("\r\nYou lost! Better luck next time!.");
            }

            Console.Write("\r\nPress [Enter] to finish the game.");
            Console.ReadLine();
        }

        /// <summary>
        /// Getst the current player PlayerOption selection
        /// </summary>
        /// <returns></returns>
        public PlayerOption GetPlayerSelection()
        {
            PlayerOption playerOption = PlayerOption.Invalid;
            while (playerOption == PlayerOption.Invalid)
            {
                Console.Write("Please choose. R/r - Rock, P/p - Paper, S/s - Scissors");
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
        public GameOptions PlayRound(GameOptions GameOptions)
        {

            DrawGameBoard(GameOptions);

            PlayerOption computerChoice = GetComputerSelection();
            PlayerOption playerChoice = GetPlayerSelection();

            Console.WriteLine($"The computer choosed: {computerChoice}");
            Console.WriteLine($"You choosed: {playerChoice}");

            //Calaculate who will be the winner and increment the resutls
            GameOptions = CalculateWinner(playerChoice, computerChoice);

            Console.Write("Press [Enter] to exit the game.");
            Console.ReadLine();

            //set the porperty AnotherRound to true
            //to continue the game
            GameOptions.AnotherRound = true;

            return GameOptions;
        }

        /// <summary>
        /// Calculates the winner
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        public GameOptions CalculateWinner(PlayerOption player, PlayerOption computer)
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
                var result = winners[player];
                if (result == computer)
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
