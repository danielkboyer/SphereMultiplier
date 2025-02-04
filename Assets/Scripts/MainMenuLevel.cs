using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts
{
    public class MainMenuLevel
    {
        public static int ROW_SIZE = 28;
        public int level;

        public int numberOfBlocks;
        //Int represents block health
        public List<int> blocks;

        public int BuildingHealth;


        public CannonType cannonToUnlock
        {
            get
            {
                switch (level)
                {
                    case 1:
                        return CannonType.ShotGun;
                    case 2:
                        return CannonType.LaserGun;
                    default:
                        throw new Exception("Level not found");

                }
            }
        }
        public MainMenuLevel(int level, List<int> blocks, int buildingHealth, int numberOfBlocks)
        {
            this.level = level;
            this.blocks = blocks;
            BuildingHealth = buildingHealth;
            this.numberOfBlocks = numberOfBlocks;
        }

        public static MainMenuLevel GetDefaultLevel(int level)
        {
            var newBlocks = Enumerable.Repeat(1, MainMenuLevel.ROW_SIZE * 32).ToList();
            switch (level)
            {
                case 2:
                    return new MainMenuLevel(2, newBlocks, 5000, newBlocks.Count);

            }

            throw new Exception("Level not found "+level);
        }

    }

    
}
