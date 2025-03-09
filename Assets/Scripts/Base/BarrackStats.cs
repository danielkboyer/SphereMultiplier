using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{


    [System.Serializable]
    public class SoldierSpawn {
        public SoldierType SoldierType { get; set; }
    public LoadTime LoadTime { get; set; }
    }


    [System.Serializable]
    public class BarrackStats
    {   
        public bool Unlocked { get; set; }

        public LoadTime UnlockBarrack { get; set; }
        public List<SoldierSpawn> SoldierSpawns { get; set; }

        public static List<BarrackStats> GetInitialBarracks()
        {
            List<BarrackStats> barracks = new List<BarrackStats>
            {
                new() { SoldierSpawns = new List<SoldierSpawn>(), Unlocked = true }
            };
            return barracks;
        }

    }

}
