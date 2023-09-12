using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary.Models;
using System.Text.RegularExpressions;

namespace BattleshipLibrary.Services
{
    public class GameLogic
    {
        readonly static GameInputValidationLogic _inputValidation = new GameInputValidationLogic();
        readonly static GameInputConversionLogic _inputConversion = new GameInputConversionLogic();

        public static void InitializePlayerGrid(PlayerInfoModel model, GameModel game)
        {
            var gridLetters = game.AllowedGridSpotRows;
            var gridNumbers = game.AllowedGridSpotColumns;

            foreach (var letter in gridLetters)
            {
                foreach (var number in gridNumbers)
                {
                    AddGridSpot(letter, number, model);
                }
            }
        }

        #region Public

        public static void IntializeGame(GameModel game)
        {
            try
            {
                // Setup first player and opponent
                game.ActivePlayer = game.Players[0];
                game.ActiveOpponent = game.Players[1];
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string[] GetPlacedShipSpots(PlayerInfoModel player)
        {
            try
            {
                string[] output;

                // Get placed ships from the player
                output = player.ShipList.FindAll(x => x.Status == GridSpotStatus.Ship).Select(x => $"{x.SpotLetter}{x.SpotNumber}").ToArray();

                return output;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateGameStatus(GameModel game)
        {
            try
            {
                // Mark opponent as Defeated if no ships left
                if (game.ActiveOpponent.ShipList.FindAll(x => x.Status == GridSpotStatus.Ship).Count == 0)
                {
                    game.ActiveOpponent.Status = PlayerStatus.Defeated;
                }

                // Count all active players
                int activePlayersCount = game.Players.FindAll(x => x.Status == PlayerStatus.Active).Count;

                // Select winner if only one player left
                if (activePlayersCount == 1)
                {
                    game.Winner = game.ActivePlayer;
                }
                else
                {
                    // Get index of current active player and move to next
                    int activePlayerIndex = game.Players.FindIndex(x => x.Name == game.ActivePlayer.Name);
                    game.ActivePlayer = GetNextPlayer(game, activePlayerIndex);

                    // Get index of current active opponent and move to next
                    int activeOpponentIndex = game.Players.FindIndex(x => x.Name == game.ActiveOpponent.Name);
                    game.ActiveOpponent = GetNextPlayer(game, activeOpponentIndex);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static PlayerInfoModel CreatePlayer(string playerName, GameModel game)
        {
            try
            {
                PlayerInfoModel output = new PlayerInfoModel();

                // Validate user name
                _inputValidation.ValidatePlayerNameFormat(playerName);
                _inputValidation.ValidateIfPlayerNameNotUsed(playerName, game);

                // Setup new player
                output.Name = playerName;

                // Generate grid for player
                InitializePlayerGrid(output, game);

                // Add player to the game
                game.Players.Add(output);

                return output;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SetPlayersAmount(GameModel game, string playersAmount)
        {
            try
            {
                // Convert input to integer
                int amountInteger = _inputConversion.ConvertStringToInt(playersAmount);
                _inputValidation.ValidatePlayersAmount(amountInteger);

                game.NumberOfPlayers = amountInteger;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SetAllowedGridSpotRows(GameModel game, string gridRowsCount)
        {
            try
            {
                // Convert input to string array and set game rows array
                int amountInteger = _inputConversion.ConvertStringToInt(gridRowsCount);
                _inputValidation.ValidateGridRowsCount(amountInteger);

                string[] rowsArray = GetGridRowsArray(amountInteger);
                game.AllowedGridSpotRows = rowsArray;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SetAllowedGridSpotColumns(GameModel game, string gridColumnsCount)
        {
            try
            {
                // Convert input to string array
                int amountInteger = _inputConversion.ConvertStringToInt(gridColumnsCount);
                _inputValidation.ValidateGridRowsCount(amountInteger);

                int[] columnsArray = GetGridColumnsArray(amountInteger);
                game.AllowedGridSpotColumns = columnsArray;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SetPlayersShipsAmount(GameModel game, string playersShipsAmount)
        {
            try
            {
                // Convert input to integer
                int amountInteger = _inputConversion.ConvertStringToInt(playersShipsAmount);
                _inputValidation.ValidatePlayersShipsAmount(amountInteger);

                game.PlayerShipsAmount = amountInteger;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void TakeShot(PlayerInfoModel activePlayer, PlayerInfoModel activeOpponent, string shotInfo)
        {
            GridSpotModel opponentGridSpot;

            try
            {
                // Convert shot info to spot row and column
                (string gridSpotRow, int gridSpotColumn) = _inputConversion.ConvertToGridSpotPlacement(shotInfo);

                // Check if grid spot exists and not shot at
                opponentGridSpot = GetGridSpot(activeOpponent, gridSpotRow, gridSpotColumn);

                GridSpotStatus[] forbiddenGridSpotStatuses = new GridSpotStatus[] { GridSpotStatus.Hit, GridSpotStatus.Miss };
                _inputValidation.ValidateGridSpotSelection(opponentGridSpot, forbiddenGridSpotStatuses);

                // If grid spot marked as ship, change status to Hit, else to Miss
                opponentGridSpot.Status = opponentGridSpot.Status == GridSpotStatus.Ship ? GridSpotStatus.Hit : GridSpotStatus.Miss;

                // Add shot and opponent info to player shot list
                activePlayer.ShotList.Add((activeOpponent, opponentGridSpot));

            }
            catch (Exception)
            {
                throw;

            }
        }
        public static void PlaceShip(string shipSpot, PlayerInfoModel player)
        {
            GridSpotModel playerGridSpot;

            try
            {
                // Convert shot info to spot row and column
                (string gridSpotRow, int gridSpotColumn) = _inputConversion.ConvertToGridSpotPlacement(shipSpot);

                // Check if grid spot exists and not used yet
                playerGridSpot = GetGridSpot(player, gridSpotRow, gridSpotColumn);

                GridSpotStatus[] forbiddenGridSpotStatuses = new GridSpotStatus[] { GridSpotStatus.Ship, GridSpotStatus.Hit, GridSpotStatus.Miss };
                _inputValidation.ValidateGridSpotSelection(playerGridSpot, forbiddenGridSpotStatuses);

                // Set spot status to Ship
                playerGridSpot.Status = GridSpotStatus.Ship;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Public

        #region Private

        private static GridSpotModel GetGridSpot(PlayerInfoModel player, string gridSpotRow, int gridSpotColumn)
        {
            try
            {
                GridSpotModel output;

                output = player.ShipList.First(x => x.SpotLetter == gridSpotRow && x.SpotNumber == gridSpotColumn);
                return output;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string[] GetGridRowsArray(int allowedGridRowsCount)
        {
            string[] output = new string[allowedGridRowsCount];

            // Setup initial char
            char rowLetter = 'A';

            for (var i = 0; i < allowedGridRowsCount; i++)
            {
                output[i] = rowLetter.ToString();
                rowLetter++;
            }

            return output;
        }

        private static int[] GetGridColumnsArray(int allowedGridColumnsCount)
        {
            int[] output = new int[allowedGridColumnsCount];

            // Setup initial number
            int columnNumber = 1;

            for (var i = 0; i < allowedGridColumnsCount; i++)
            {
                output[i] = columnNumber;
                columnNumber++;

            }

            return output;
        }

        private static PlayerInfoModel GetNextPlayer(GameModel game, int i)
        {
            PlayerInfoModel? output = null;

            // Count all active players and throw exception if none
            int activePlayersCount = game.Players.FindAll(x => x.Status == PlayerStatus.Active).Count;

            if (activePlayersCount == 1)
            {
                throw new InvalidOperationException("Only one active player is left, game cannot be continued");
            }

            // Otherwise, loop until next active player has been found
            do
            {
                try
                {
                    i++;
                    output = game.Players.ElementAt(i).Status == PlayerStatus.Active ? game.Players.ElementAt(i) : null;
                }
                catch (ArgumentOutOfRangeException)
                {

                    i = 0;
                    output = game.Players.ElementAt(i).Status == PlayerStatus.Active ? game.Players.ElementAt(i) : null;
                }
                catch (Exception)
                {
                    throw;
                }
            } while (output == null);

            return output;
        }

        private static void AddGridSpot(string letter, int number, PlayerInfoModel model)
        {
            GridSpotModel gridSpot = new GridSpotModel()
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShipList.Add(gridSpot);
        }
        #endregion Private















        

        

        

        

       
    }
}
