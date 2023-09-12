using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BattleshipLibrary.Services
{
    internal class GameInputValidationLogic
    {
        readonly static GameConfiguration _configuration = new GameConfiguration();

        internal void ValidatePlayersShipsAmount(int playersShipsAmount)
        {
            if (playersShipsAmount < _configuration.MinimumShipsAmount || playersShipsAmount > _configuration.MaximumShipsAmount)
            {
                throw new ArgumentOutOfRangeException(null, $"Ships amount cannot be lower than {_configuration.MinimumShipsAmount} and higher than {_configuration.MaximumShipsAmount}.");
            }
        }

        internal void ValidatePlayerNameFormat(string playerName)
        {
            // Check if not empty
            if (string.IsNullOrWhiteSpace(playerName)) 
            {
                throw new ArgumentNullException("playerName", "Player name cannot be empty.");
            }
        }

        internal void ValidateIfPlayerNameNotUsed(string playerName, GameModel game)
        {
            PlayerInfoModel? player = game.Players.Find(x => x.Name == playerName);
            if (player != null)
            {
                throw new ArgumentException($"Name {playerName} is already used.");
            }
        }

        internal void ValidateGridRowsCount(int gridRowsCount)
        {
            if (gridRowsCount < _configuration.MinimumGridRows || gridRowsCount > _configuration.MaximumGridRows)
            {
                throw new ArgumentOutOfRangeException(null, $"Grid rows amount cannot be lower than {_configuration.MinimumGridRows} and higher than {_configuration.MaximumGridRows}.");
            }
        }

        internal void ValidatePlayersAmount(int playersAmount)
        {
            if (playersAmount < _configuration.MinimumPlayersAmount || playersAmount > _configuration.MaximumPlayersAmount)
            {
                throw new ArgumentOutOfRangeException(null, $"Players amount cannot be lower than {_configuration.MinimumPlayersAmount} and higher than {_configuration.MaximumPlayersAmount}.");
            }
        }

        internal void ValidateGridColumnsCount(int gridColumnsCount)
        {
            if (gridColumnsCount < _configuration.MinimumGridColumns || gridColumnsCount > _configuration.MaximumGridColumns)
            {
                throw new ArgumentOutOfRangeException(null, $"Grid columns cannot be lower than {_configuration.MinimumGridColumns} and higher than {_configuration.MaximumGridColumns}.");
            }
        }

        internal void ValidateGridSpotSelection(GridSpotModel gridSpot, GridSpotStatus[] forbiddenGridSpotStatuses)
        {
            // Check if grid spot match one of forbidden statuses
            foreach (var forbiddenStatus in forbiddenGridSpotStatuses)
            {
                if (gridSpot.Status == forbiddenStatus) throw new ArgumentException($"Selected spot cannot be used. Current status: {forbiddenStatus}");
            }
        }
    }
}
