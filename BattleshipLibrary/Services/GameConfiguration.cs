using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary.Services
{
    internal class GameConfiguration
    {
        public int MinimumGridColumns { get; private set; }
        public int MaximumGridColumns { get; private set; }
        public int MinimumGridRows { get; private set; }
        public int MaximumGridRows { get; private set; }
        public int MinimumPlayersAmount { get; private set; }
        public int MaximumPlayersAmount { get; private set; }
        public int MinimumShipsAmount { get; private set; }
        public int MaximumShipsAmount { get; private set; }

        public GameConfiguration()
        {
            MinimumGridColumns = 5;
            MaximumGridColumns = 15;
            MinimumGridRows = 5;
            MaximumGridRows = 15;
            MinimumPlayersAmount = 2;
            MaximumPlayersAmount = 10;
            MinimumShipsAmount = 5;
            MaximumShipsAmount = 10;
        }
    }
}
