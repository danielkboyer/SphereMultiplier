using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class MainMenuProgress : Unity.Services.Analytics.Event
    {
        public MainMenuProgress() : base("MainMenuProgress")
        {
        }

        public int GridDestroyed { set { SetParameter("GridDestroyed", value); } }
        public int TowerDestroyed { set { SetParameter("TowerDestroyed", value); } }
    }
}
