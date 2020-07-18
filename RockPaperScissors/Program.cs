using RockPaperScissors.Application.Respository;
using RockPaperScissors.Domain;
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
                GameOptions gameOptions = new GameOptions();

                //declare string.empty playerName for container name of the player 
                string playerName = string.Empty;

                //declare string.empty totalGameRound for cointaner of total game round specify by player
                string totalGameRound = string.Empty;

                //declaret string.empty for container of gamerSelection
                string gamerSelection = string.Empty;
                #endregion

                while (string.IsNullOrEmpty(gameOptions.PlayerName))
                {
                    Console.Clear();
                    Console.Write("Welcome to Rock, Paper, Scissors.\r\n\r\nPlease enter your nickname. ");
                    playerName = Console.ReadLine();
                    gameOptions.PlayerName = playerName;
                }

                //Ask the gamer some options 
                //Player vs Computer 
                //Computer vs Computer 
                Console.Write("\r\nPlease choose Game Option. \r\n Press [1] Player VS Computer \r\n Press [2] Computer VS Computer \r\n");
                gamerSelection = Console.ReadLine();

                if (gamerSelection.Equals("1"))
                {
                    gameOptions.PlayerName = playerName;
                    gameOptions.computerOnly = false;
                    gameOptions = gameService.SetupGame(gameOptions);
                    gameService.DrawGameBoard(gameOptions);
                    Console.WriteLine("The rules is simple, you will be asked to make your selection. The computer will choose first. You can then enter Rock, Paper or Scissors.\r\n\r\nPress [Enter] to start the game.");
                    Console.ReadLine();
                }
                if (gamerSelection.Equals("2"))
                {
                    gameOptions.PlayerName = playerName;
                    gameOptions.computerOnly = true;
                    gameOptions = gameService.SetupGame(gameOptions);
                    gameService.DrawGameBoard(gameOptions);
                    Console.WriteLine("The two computer will play each other stay relax and enjoy the game.\r\n\r\nPress [Enter] to start the game.");
                    Console.ReadLine();
                }

                gameOptions.PlayerName = playerName;
                gameOptions = gameService.PlayRound(gameOptions);


                gameOptions.PlayerName = playerName;
                gameService.DrawEnd(gameOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Error Message: {ex.Message}");
                Console.ReadLine();
            }    
        }
    }
 }
