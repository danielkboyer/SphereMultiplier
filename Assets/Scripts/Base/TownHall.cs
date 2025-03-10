using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
#nullable enable
namespace Assets.Scripts
{


    [System.Serializable]
    public class TownHall
    {
        public int Level { get; set; }

        public LoadTime? UpgradeTime { get; set; }
        public static TownHall GetInitialTownHall()
        {
         
            return new TownHall() { Level = 1};
        }
    }

}
