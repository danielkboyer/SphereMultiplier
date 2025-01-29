using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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


        public MainMenuLevel(int level, List<int> blocks, int buildingHealth, int numberOfBlocks)
        {
            this.level = level;
            this.blocks = blocks;
            BuildingHealth = buildingHealth;
            this.numberOfBlocks = numberOfBlocks;
        }

    }
}
