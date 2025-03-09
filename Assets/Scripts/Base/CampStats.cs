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
    public class CampStats
    {
        public int Max { get; set; }
        public bool Unlocked { get; set; }

        public LoadTime? UnlockCamp { get; set; }
        public List<SoldierType>? Soldiers { get; set; }

        public static List<CampStats> GetInitialCamps()
        {
            List<CampStats> camps = new List<CampStats>();
            camps.Add(new CampStats { Max = 5, Soldiers = new List<SoldierType>() { { SoldierType.Melee }, { SoldierType.Archer }, { SoldierType.Archer }, { SoldierType.Archer }, { SoldierType.Archer } }, Unlocked= true });
            return camps;
        }
    }

}
