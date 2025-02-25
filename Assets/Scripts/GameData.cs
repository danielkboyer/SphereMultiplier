using System.Collections.Generic;

namespace Assets.Scripts
{


    [System.Serializable]
public class GameData
    {

        public static int MAX_LEVEL = 16;
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

        public CannonData SelectedCannon;
        public List<CannonData> UnlockedCannons;

        public MainMenuLevel MainMenuLevel;

        public int Level;
        public GameData(int coins, float cannonFireRate, int smallBallHealth, int bigBallHealth, int smallEnemyHealth, int bigEnemyHealth, int smallBallAttack, int bigBallAttack, int smallEnemyAttack, int bigEnemyAttack, int level, MainMenuLevel mainMenuLevel, CannonData selectedCannon, List<CannonData> unlockedCannons)
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
            MainMenuLevel = mainMenuLevel;
            SelectedCannon = selectedCannon;
            UnlockedCannons = unlockedCannons;
        }
    }
}
