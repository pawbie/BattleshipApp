using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleshipLibrary.Models
{
    public class PlayerInfoModel
    {
        public string Name { get; set; }
        public PlayerStatus Status { get; set; }
        public int Id { get; set; }
        public List<GridSpotModel> ShipList { get; set; }
        public List<(PlayerInfoModel, GridSpotModel)> ShotList { get; set; }

        public PlayerInfoModel()
        {
            ShipList = new();
            ShotList = new();
            Status = PlayerStatus.Active;
        }
    }
}
