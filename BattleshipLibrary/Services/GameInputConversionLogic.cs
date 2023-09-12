using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary.Services
{
    internal class GameInputConversionLogic
    {
        internal int ConvertStringToInt(string playersAmount)
        {
            return int.Parse(playersAmount);
        }

        internal (string gridSpotRow, int gridSpotColumn) ConvertToGridSpotPlacement(string shotInfo)
        {
            string rowOutput;
            int columnOutput;

            // Split string and return value as tuple
            rowOutput = shotInfo[0].ToString().ToUpper();
            columnOutput = int.Parse(shotInfo[1].ToString());

            return (rowOutput, columnOutput);
        }
    }
}
