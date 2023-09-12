using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary.Models;
using BattleshipLibrary.Services;

namespace BattleshipGame
{
    public static class ConsoleLogic
    {
        public static void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Battleship Game!");
        }

        public static GameModel NewGame()
        {
            GameModel output;
            int numberOfPlayers;
            bool customConfiguration;

            // Create initial game object
            output = new GameModel();

            // Ask if default configuration or custom
            customConfiguration = ConsolePrompts.AskForCustomConfiguration();

            // Set number of players number of players
            SetPlayersAmount(output);

            if (customConfiguration == true)
            {
                // Set amount of letter rows
                SetAllowedGridSpotRows(output);

                // Set amount of number columns
                SetAllowedGridSpotColumns(output);

                // Set amount of ships to place
                SetPlayersShipsAmount(output);  
            }

            // Create players
            CreatePlayers(output);

            return output;
        }

        private static void CreatePlayers(GameModel game)
        {
            for (var i = 0; i < game.NumberOfPlayers; i++)
            {
                Console.Clear();
                bool playerCreated = false;

                do
                {
                    try
                    {
                        // Get user name
                        string playerName = ConsolePrompts.AskPlayerName();

                        // Initialize ship placement grid
                        PlayerInfoModel player = GameLogic.CreatePlayer(playerName, game);

                        // Ask for ship placemenet
                        PlaceShips(player, game);

                        playerCreated = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Something went wrong when saving the data. Please try again. Error message: {ex.Message}");

                    }
                } while (playerCreated == false);
            }
        }

        static void SetPlayersAmount(GameModel game)
        {
            bool playersAmountConfigured = false;

            do
            {
                // Ask for players amount
                string playersAmount = ConsolePrompts.AskForPlayersAmount();

                // Try to setup amount of players
                try
                {
                    GameLogic.SetPlayersAmount(game, playersAmount);
                    playersAmountConfigured = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong when saving the data. Please try again. Error message: {ex.Message}");
                }
            } while (playersAmountConfigured == false);
        }

        static void SetAllowedGridSpotRows(GameModel game)
        {
            bool allowedGridSpotRowsConfigured = false;

            do
            {
                // Ask for players amount
                string allowedGridSpotRows = ConsolePrompts.AskForGridRowsAmount();

                // Try to setup amount of players
                try
                {
                    GameLogic.SetAllowedGridSpotRows(game, allowedGridSpotRows);
                    allowedGridSpotRowsConfigured = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong when saving the data. Please try again. Error message: {ex.Message}");
                }
            } while (allowedGridSpotRowsConfigured == false);
        }

        static void SetAllowedGridSpotColumns(GameModel game)
        {
            bool allowedGridSpotColumnsConfigured = false;

            do
            {
                // Ask for players amount
                string allowedGridSpotColumns = ConsolePrompts.AskForGridColumnsAmount();

                // Try to setup amount of players
                try
                {
                    GameLogic.SetAllowedGridSpotColumns(game, allowedGridSpotColumns);
                    allowedGridSpotColumnsConfigured = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong when saving the data. Please try again. Error message: {ex.Message}");
                }
            } while (allowedGridSpotColumnsConfigured == false);
        }

        static void SetPlayersShipsAmount(GameModel game)
        {
            bool playersShipsAmountConfigured = false;

            do
            {
                // Ask for players amount
                string playersShipsAmount = ConsolePrompts.AskForPlayersShipsAmount();

                // Try to setup amount of players
                try
                {
                    GameLogic.SetPlayersShipsAmount(game, playersShipsAmount);
                    playersShipsAmountConfigured = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong when saving the data. Please try again. Error message: {ex.Message}");
                }
            } while (playersShipsAmountConfigured == false);
        }

        internal static void ProcessShot(PlayerInfoModel activePlayer, PlayerInfoModel currentOpponent)
        {
            string shotSpot;
            bool shotSpotIsValid = false;

            do
            {
                // Ask for target
                shotSpot = ConsolePrompts.AskForShotPlacement();

                try
                {

                    // Try to take a shot
                    GameLogic.TakeShot(activePlayer, currentOpponent, shotSpot);

                    // Mark shot as valid
                    shotSpotIsValid = true;
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Something went wrong when saving the data. Please try again. Error message: {ex.Message}");
                } 
            } while (shotSpotIsValid == false);
        }

        public static void PlaceShips(PlayerInfoModel player, GameModel game)
        {
            string[] placedShips;
            string shipSpot;

            do
            {
                placedShips = GameLogic.GetPlacedShipSpots(player);

                if (placedShips.Length < 5)
                {
                    shipSpot = ConsolePrompts.AskForShipPlacement(placedShips.Length + 1, placedShips);

                    try
                    {
                        GameLogic.PlaceShip(shipSpot, player);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Something went wrong when saving the data. Please try again. Error message: {ex.Message}");
                    }
                }
                
            } while (placedShips.Length < game.PlayerShipsAmount);
        }
    }
}
