using RockPaperScissors.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Application.Interface
{
    public interface IGameService
    {
        /// <summary>
        /// Sets up a game board
        /// </summary>
        GameOptions SetupGame(GameOptions gameOptions);

        /// <summary>
        /// Draws the game board header
        /// </summary>
        void DrawGameBoard(GameOptions gameOptions);

        /// <summary>
        /// Draws the end of the game
        /// </summary>
        void DrawEnd(GameOptions gameOptions);

        PlayerOption GetPlayerSelection();

        /// <summary>
        /// Gets the computer GetComputerPlayerOption selection
        /// </summary>
        /// <returns></returns>
        PlayerOption GetComputerSelection();

        /// <summary>
        /// Runs a player round
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <returns></returns>
        GameOptions PlayRound(GameOptions gameOptions);

        /// <summary>
        /// Calculates the winner
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        GameOptions CalculateWinner(PlayerOption player, PlayerOption computer);

        /// <summary>
        /// Simple format method to convert the score into a fixed length 
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        string FormatScore(int score);
    }
}
    