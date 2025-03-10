using System.Collections.Generic;
#nullable enable
namespace Assets.Scripts
{


    [System.Serializable]
    public class SoldierSpawn
    {
        public SoldierType SoldierType { get; set; }
        public LoadTime LoadTime { get; set; }
    }


    [System.Serializable]
    public class BarrackStats
    {
        public bool Unlocked { get; set; }

        public LoadTime? UnlockBarrack { get; set; }
        public List<SoldierSpawn>? SoldierSpawns { get; set; }

        public static List<BarrackStats> GetInitialBarracks()
        {
            List<BarrackStats> barracks = new List<BarrackStats>
            {
                new() { SoldierSpawns = new List<SoldierSpawn>(), Unlocked = false }
            };
            return barracks;
        }

    }

}
