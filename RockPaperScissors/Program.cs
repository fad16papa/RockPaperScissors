using RockPaperScissors.Application.Respository;
using RockPaperScissors.Domain;
using System;

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

                //declare string.empty for container of gamerSelection
                string gamerSelection = string.Empty;
                #endregion
                
                gameOptions.AnotherRound = true;

                while(gameOptions.AnotherRound)
                {
                    while (string.IsNullOrEmpty(gameOptions.PlayerName))
                    {
                        Console.Clear();
                        Console.Write("Welcome to Rock, Paper, Scissors.\r\n\r\nPlease enter your nickname. ");
                        playerName = Console.ReadLine();
                        gameOptions.PlayerName = playerName;
                    }

                    //this will display the game menu 
                    gameOptions = gameService.GameMenu(gameOptions.AnotherRound, gameOptions.PlayerName);

                    //this will do the main computation of rock paper and scissors
                    //this will check and compare all the selection 
                    //Player VS Computer 
                    //Computer VS Computer
                    gameOptions = gameService.PlayGame(gameOptions);
                    //gameOptions.PlayerName = playerName;

                    //this will display the final score board
                    gameService.DrawEnd(gameOptions);

                    Console.Write("\r\nPlease choose Game Option. \r\n Press [1] to try again \r\n Press any key except [1] to exit the game \r\n");
                    gamerSelection = Console.ReadLine();

                    if (gamerSelection.Equals("1"))
                    {
                        Console.Clear();
                        gameOptions.AnotherRound = true;
                        continue;
                    }
                    else
                    {
                        gameOptions.AnotherRound = false;
                        break;
                    }
                }
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
