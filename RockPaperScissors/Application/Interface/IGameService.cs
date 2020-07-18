using RockPaperScissors.Domain;


namespace RockPaperScissors.Application.Interface
{
    public interface IGameService
    {
        /// <summary>
        /// This will display the menu options
        /// </summary>
        /// <param name="AnotherRound"></param>
        /// <returns></returns>
        GameOptions GameMenu(bool AnotherRound, string playerName);

        /// <summary>
        /// Sets up a game board
        /// </summary>
        /// <param name="gameOptions"></param>
        /// <returns></returns>
        GameOptions SetupGame(GameOptions gameOptions);

        /// <summary>
        /// Draws the game board header
        /// </summary>
        /// <param name="gameOptions"></param>
        void DrawGameBoard(GameOptions gameOptions);

        /// <summary>
        /// Draws the end of the game
        /// </summary>
        /// <param name="gameOptions"></param>
        void DrawEnd(GameOptions gameOptions);

        /// <summary>
        /// Gets the player selection
        /// </summary>
        /// <returns></returns>
        PlayerOption GetPlayerSelection();

        /// <summary>
        /// Gets the computer selection
        /// </summary>
        /// <returns></returns>
        PlayerOption GetComputerSelection();

        /// <summary>
        /// Runs a player round
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <returns></returns>
        GameOptions PlayGame(GameOptions gameOptions);

        /// <summary>
        /// Calculates the winner
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        GameOptions CalculateWinner(PlayerOption player, PlayerOption computer, GameOptions gameOptions);

        /// <summary>
        /// Simple format method to convert the score into a fixed length 
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        string FormatScore(int score);
    }
}
    