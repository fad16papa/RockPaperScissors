using RockPaperScissors.Application.Interface;
using RockPaperScissors.Domain;
using RockPaperScissors.Helper;
using System;
using System.Collections.Generic;

namespace RockPaperScissors.Application.Respository
{
    public class GameService : IGameService
    {
        /// <summary>
        /// This will display the menu options
        /// </summary>
        /// <param name="AnotherRound"></param>
        /// <returns></returns>
        public GameOptions GameMenu(bool AnotherRound, string playerName)
        {
            //Instantiate GameService
            GameService gameService = new GameService();

            //Instantiate GameOptionsModel
            GameOptions gameOptions = new GameOptions();
            gameOptions.PlayerName = playerName;

            while (AnotherRound)
            {
                //Ask the gamer some options 
                //Player vs Computer 
                //Computer vs Computer 
                Console.Write("\r\nPlease choose Game Option. \r\n Press [1] Player VS Computer \r\n Press [2] Computer VS Computer \r\n");
                string gamerSelection = Console.ReadLine();

                if (gamerSelection.Equals("1"))
                {
                    gameOptions.computerOnly = false;
                    gameOptions = gameService.SetupGame(gameOptions);
                    gameService.DrawGameBoard(gameOptions);
                    Console.WriteLine("The rules is simple, you will be asked to make your selection. The computer will choose first. You can then enter Rock, Paper or Scissors.\r\n\r\nPress [Enter] to start the game.");
                    Console.ReadLine();
                    gameOptions.AnotherRound = false;
                    break;
                }
                if (gamerSelection.Equals("2"))
                {
                    gameOptions.computerOnly = true;
                    gameOptions = gameService.SetupGame(gameOptions);
                    gameService.DrawGameBoard(gameOptions);
                    Console.WriteLine("The two computer will play each other stay relax and enjoy the game.\r\n\r\nPress [Enter] to start the game.");
                    Console.ReadLine();
                    gameOptions.AnotherRound = false;
                    break;
                }
                else
                {
                    Console.WriteLine($"{gamerSelection} is not a valid. Please [Enter] try again.\r\n");
                    Console.ReadLine();
                }
            }

            return gameOptions;
        }

        /// <summary>
        /// Sets up a game board
        /// </summary>
        public GameOptions SetupGame(GameOptions gameOptions)
        {
            /*  Template
            *  Score: {playerName}: {playerWins, 2 spaces}   Computer: {computerWins, 2 spaces} 
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
                Console.Write($"Scores: {gameOptions.PlayerName}: {FormatScore(gameOptions.PlayerWins)}  Computer: {FormatScore(gameOptions.ComputerWins)} ");
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
        }

        /// <summary>
        /// Gets the player selection
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
        /// Gets the computer selection
        /// </summary>
        /// <returns></returns>
        public PlayerOption GetComputerSelection()
        {
            PlayerOption computerSelection = EnumHelper.Of<PlayerOption>();
            
            while(computerSelection == PlayerOption.Invalid)
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

            return computerSelection;
        }

        /// <summary>
        /// Runs a player round
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <returns></returns>
        public GameOptions PlayGame(GameOptions gameOptions)
        {
            DrawGameBoard(gameOptions);

            if (gameOptions.computerOnly)
            {
                //Calculate who will be the winner computer vs computer
                PlayerOption firstComputerChoice = GetComputerSelection();
                Console.WriteLine($"The computer choosed: {firstComputerChoice}");

                PlayerOption secondComputerChoice = GetComputerSelection();
                Console.WriteLine($"The computer choosed: {secondComputerChoice}");

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
