using RockPaperScissors.Application.Respository;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Start();
        }
    }

    public class Game
    {
        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            try
            {
                #region Properties
                //Instantiate GameService
                GameService gameService = new GameService();

                //Instantiate GameOptionsModel
                GameOptionsModel gameOptionsModel = new GameOptionsModel();

                string playerName = string.Empty;
                #endregion

                while (string.IsNullOrEmpty(gameOptionsModel.PlayerName))
                {
                    Console.Clear();
                    Console.Write("Welcome to Rock, Paper, Scissors.\r\n\r\nPlease enter your nickname... ");
                    playerName = Console.ReadLine();
                    gameOptionsModel.PlayerName = playerName;
                }

                Console.Write($"Welcome {gameOptionsModel.PlayerName}...\r\n\r\nHow many games do you wish to play? Please enter an odd number... ");
                string gameStr = Console.ReadLine();
                int numOfGames = 0;
                int.TryParse(gameStr, out numOfGames);

                while (numOfGames <= 0 || numOfGames % 2 == 0)
                {
                    Console.Write($"{gameStr} is not a valid input.\r\nPlease enter the amount of games you wish to play. The number you enter must be an odd number... ");
                    gameStr = Console.ReadLine();
                    int.TryParse(gameStr, out numOfGames);
                }

                gameOptionsModel.NumberOfGames = numOfGames;

                gameOptionsModel = gameService.SetupGame(gameOptionsModel);
                gameService.DrawGameBoard(gameOptionsModel);

                Console.WriteLine("The rules are very simple, you will be asked to make your selection. The computer will choose first, this is so it cant cheat. You can then enter Rock, Paper or Scissors. The winner will be determined against the standard Rock, Paper, Scissors rule book.\r\n\r\nWhen you are ready press [Enter] to start the game.");
                Console.ReadLine();


                for (int i = 0; i < numOfGames; i++)
                {
                    gameOptionsModel.PlayerName = playerName;
                    gameOptionsModel = gameService.PlayRound(i, gameOptionsModel);

                    if (!gameOptionsModel.AnotherRound)
                    {
                        break;
                    }
                }
                    
                gameService.DrawEnd(gameOptionsModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadLine();
            }    
        }
    }
 }
