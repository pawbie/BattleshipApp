using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary.Models
{
    public class GameModel
    {
        public int NumberOfPlayers { get; set; }
        public List<PlayerInfoModel> Players { get; set; }
        public PlayerInfoModel? Winner { get; set; }
        public PlayerInfoModel ActivePlayer { get; set; }
        public PlayerInfoModel ActiveOpponent { get; set; }
        public string[] AllowedGridSpotRows { get; set; }
        public int[] AllowedGridSpotColumns { get; set; }
        public int PlayerShipsAmount { get; set; }
        public TimeSpan TurnTimeLimit { get; set; }

        public GameModel()
        {
            Players = new();

            AllowedGridSpotRows = new string[] { "A", "B", "C", "D", "E" };
            AllowedGridSpotColumns = new int[] { 1, 2, 3, 4, 5 };
            PlayerShipsAmount = 5;
            NumberOfPlayers = 2;
        }
    }
}
