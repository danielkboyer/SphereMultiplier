using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class LevelFinished : Unity.Services.Analytics.Event
    {
        public LevelFinished() : base("LevelFinished")
        {
        }

        public bool UserWonLevel { set { SetParameter("UserWonLevel", value); } }
        public int Level { set { SetParameter("Level", value); } }
    }
}
