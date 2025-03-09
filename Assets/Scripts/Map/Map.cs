using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{


    public class SoldierWithPosition
    {
        public SoldierType SoldierType { get; set; }
        public Vector3 Position { get; set; }

        public int Level { get; set; }

    }
    public class Level
    {
        public List<SoldierWithPosition> Soldiers;


        public Level(List<SoldierWithPosition> soldiers)
        {
            Soldiers = soldiers;
        }
        public static Level GetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    return Level1();
                default:
                    return Level1();
            }
        }

        static Level Level1()
        {
            return new Level(new List<SoldierWithPosition>() {
                new() { SoldierType = SoldierType.Goblin, Position = new Vector3(-0.0105478168f, 0.370000005f, 4.63999987f), Level = 1 },
                new() { SoldierType = SoldierType.Goblin, Position = new Vector3(0.0105478168f, 0.370000005f, 4.63999987f), Level = 1 },
                new() { SoldierType = SoldierType.Goblin, Position = new Vector3(0f, 0.370000005f, 4.63999987f), Level=1 }
            });
        }
    }



    [System.Serializable]
    public class Map
    {
        public int CurrentLevel { get; set; }

        public static Map GetInitial()
        {
            return new Map() { CurrentLevel = 1 };
        }
    }
}
