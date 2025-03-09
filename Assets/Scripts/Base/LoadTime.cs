using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{


    [System.Serializable]
    public class LoadTime {
        public DateTimeOffset StartTime { get; set; }
        /**
         * The amount of time to spawn this solder
         */
        public float TimeToLoad { get; set; }

        public float GetSecondsRemaining()
        {
            return TimeToLoad - (float)(DateTimeOffset.Now - StartTime).TotalSeconds;
        }
    }


}
