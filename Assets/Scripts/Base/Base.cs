using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
  





    [System.Serializable]
    public class Base
    {
        public int BarrackLevel { get; set; }
        public Dictionary<SoldierType, int> SoldierLevels { get; set; }
        public List<BarrackStats> Barracks { get; set; }
        public List<CampStats> Camps { get; set; }

        public Base()
        {

        }

        protected Base(Dictionary<SoldierType,int> soldierLevels, List<BarrackStats> barracks, List<CampStats> camps)
        {
            this.Barracks = barracks;
            this.Camps = camps;
            this.SoldierLevels = soldierLevels;
        }

        public static Base GetInitial()
        {
            return new Base(Base.GetInitialSoldierLevels(), BarrackStats.GetInitialBarracks(), CampStats.GetInitialCamps());
        }

        private static Dictionary<SoldierType, int> GetInitialSoldierLevels()
        {
            Dictionary<SoldierType, int> soldierLevels = new Dictionary<SoldierType, int>
            {
                { SoldierType.Melee, 1 },
                { SoldierType.Archer, 1 }
            };
            return soldierLevels;
        }

       

       
    }
}
