using System;
using BattleshipLibrary.Models;
using BattleshipGame;
using BattleshipLibrary.Services;

namespace BattleshipGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogic.ShowWelcomeMessage();
            GameModel currentGame = ConsoleLogic.NewGame();

            // Initialize game
            GameLogic.IntializeGame(currentGame);

            // Let each player take a shot until only one is left
            do
            {
                Console.Clear();

                // Show current player / opponent
                ConsolePrompts.ShowActivePlayerAndActiveOpponent(currentGame);

                // Show opponents grid
                ConsolePrompts.ShowOpponentGrid(currentGame, currentGame.ActiveOpponent);

                // Ask for a shot
                ConsoleLogic.ProcessShot(currentGame.ActivePlayer, currentGame.ActiveOpponent);

                // Refresh opponents grid
                Console.Clear();
                ConsolePrompts.ShowOpponentGrid(currentGame, currentGame.ActiveOpponent);

                // Show shot result
                ConsolePrompts.ShowShotResult(currentGame.ActivePlayer);

                // Show opponent defeat status if all ships sunk
                ConsolePrompts.ShowOpponentStatus(currentGame.ActiveOpponent);

                // Update game status for active player / opponent / winning player
                GameLogic.UpdateGameStatus(currentGame);

                ConsolePrompts.NotifyPressKeyToContinue();

            } while (currentGame.Winner == null);

            Console.Clear();
            ConsolePrompts.NotifyGameFinished();
            ConsolePrompts.ShowWinningPlayer(currentGame.Winner);
            ConsolePrompts.ShowWinningPlayerStatistics(currentGame.Winner);

            Console.ReadLine();
        }
    }
}
