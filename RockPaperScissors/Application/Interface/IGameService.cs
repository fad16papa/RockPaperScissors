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
        /// Runs a player round
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <returns></returns>
        GameOptionsModel PlayRound(int roundNumber, GameOptionsModel gameOptionsModel);

        /// <summary>
        /// Calculates the winner
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        GameOptionsModel CalculateWinner(PlayerOptionModel player, PlayerOptionModel computer);

        /// <summary>
        /// Sets up a game board
        /// </summary>
        GameOptionsModel SetupGame(GameOptionsModel gameOptionsModel);

        /// <summary>
        /// Draws the game board header
        /// </summary>
        void DrawGameBoard(GameOptionsModel gameOptionsModel);

        /// <summary>
        /// Draws the end of the game
        /// </summary>
        void DrawEnd(GameOptionsModel gameOptionsModel);

        /// <summary>
        /// Simple format method to convert the score into a fixed length 
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        string FormatScore(int score);

        /// <summary>
        /// Gets the computer PlayerOptionModel selection
        /// </summary>
        /// <returns></returns>
        PlayerOptionModel GetComputerPlayerOptionModel();
    }
}
    