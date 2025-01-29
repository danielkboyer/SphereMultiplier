using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class GameData
    {
        public int Coins;
        public float CannonFireRate;
        public int SmallBallHealth;
        public int BigBallHealth;
        public int SmallEnemyHealth;
        public int BigEnemyHealth;

        public int SmallBallAttack;
        public int BigBallAttack;
        public int SmallEnemyAttack;
        public int BigEnemyAttack;

        public MainMenuLevel MainMenuLevel;

        public int Storage;
        public int Level;
        public GameData(int coins, float cannonFireRate, int smallBallHealth, int bigBallHealth, int smallEnemyHealth, int bigEnemyHealth, int smallBallAttack, int bigBallAttack, int smallEnemyAttack, int bigEnemyAttack, int level, int storage, MainMenuLevel mainMenuLevel)
        {
            this.Coins = coins;
            CannonFireRate = cannonFireRate;
            SmallBallHealth = smallBallHealth;
            BigBallHealth = bigBallHealth;
            SmallEnemyHealth = smallEnemyHealth;
            BigEnemyHealth = bigEnemyHealth;
            SmallBallAttack = smallBallAttack;
            BigBallAttack = bigBallAttack;
            SmallEnemyAttack = smallEnemyAttack;
            BigEnemyAttack = bigEnemyAttack;
            Level = level;
            Storage = storage;
            MainMenuLevel = mainMenuLevel;
        }
    }
}
