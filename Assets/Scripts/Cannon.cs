using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public enum CannonType
    {
        RegularGun,
        ShotGun,
        LaserGun,
    }
    [System.Serializable]
    public class CannonData
    {
        public CannonType type;

        public float shotCooldown
        {
            get
            {
                switch (type)
                {
                    case CannonType.RegularGun:
                        return 0.35f;
                    case CannonType.ShotGun:
                        return 0.8f;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public CannonData(CannonType type)
        {
          this.type = type;
        }

    }
}
