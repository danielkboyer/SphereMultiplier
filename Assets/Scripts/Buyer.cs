using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class Purchase<Value>
    {
        public int Cost;
        public Value NewValue;

        public Purchase(int cost, Value newValue)
        {
            Cost = cost;
            NewValue = newValue;
        }
    }
    public class Buyer
    {
        
        public static Purchase<float> GetCannonFirePurchase(float fireRate)
        {
            var diff = 1.5f - fireRate;
            var split = Mathf.RoundToInt(diff / 0.01f);
            return new Purchase<float>(200 + split * 100, fireRate - 0.01f);
        }

        public static Purchase<int> GetBallPurchase(int ballHealth)
        {
            var diff = ballHealth - 30;
            return new Purchase<int>(200 + diff * 100, ballHealth + 1);
        }

        public static Purchase<int> GetBigBallPurchase(int bigBallHealth)
        {
            var diff = bigBallHealth - 120;
            var split = Mathf.RoundToInt(diff / 5);
            return new Purchase<int>(200 + split * 100, bigBallHealth + 5);
        }

        public static Purchase<int> GetStoragePurchase(int storage)
        {
            var diff = storage - 2000;
            var split = Mathf.RoundToInt(diff / 100);
            return new Purchase<int>(200 + split * 100, storage + 100);
        }
    }
}
