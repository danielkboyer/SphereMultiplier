using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{


    public class GameStorage
    {
        private static GameStorage INSTANCE;
        private const string COIN_KEY = "Coins";
        private const string CANNON_FIRE_RATE = "Cannon_Fire_Rate";
        private const string SMALL_BALL_HEALTH = "Small_Ball_Health";
        private const string BIG_BALL_HEALTH = "Big_Ball_Health";
        private const string SMALL_ENEMY_HEALTH = "Small_Enemy_Health";
        private const string BIG_ENEMY_HEALTH = "Big_Enemy_Health";

        private const string SMALL_BALL_ATTACK = "Small_Ball_Attack";
        private const string BIG_BALL_ATTACK = "Big_Ball_Attack";
        private const string SMALL_ENEMY_ATTACK = "Small_Enemy_Attack";
        private const string BIG_ENEMY_ATTACK = "Big_Enemy_Attack";

        private const string LEVEL = "Level";

        private const string STORAGE = "Storage";

        private GameData data;
        public GameData GetGameData()
        {
            if (data == null)
            {
                var coins = PlayerPrefs.GetInt(COIN_KEY, 0);
                var cannonFireRate = PlayerPrefs.GetFloat(CANNON_FIRE_RATE, 1.5f);
                var smallBallHealth = PlayerPrefs.GetInt(SMALL_BALL_HEALTH, 30);
                var bigBallHealth = PlayerPrefs.GetInt(BIG_BALL_HEALTH, 120);
                var smallEnemyHealth = PlayerPrefs.GetInt(SMALL_ENEMY_HEALTH, 30);
                var bigEnemyHealth = PlayerPrefs.GetInt(BIG_ENEMY_HEALTH, 120);

                var smallBallAttack = PlayerPrefs.GetInt(SMALL_BALL_ATTACK, 10);
                var bigBallAttack = PlayerPrefs.GetInt(BIG_BALL_ATTACK, 30);
                var smallEnemyAttack = PlayerPrefs.GetInt(SMALL_ENEMY_ATTACK, 10);
                var bigEnemyAttack = PlayerPrefs.GetInt(BIG_ENEMY_ATTACK, 30);

                var level = PlayerPrefs.GetInt(LEVEL, 1);

                var storage = PlayerPrefs.GetInt(STORAGE, 2000);
                data = new GameData(coins, cannonFireRate, smallBallHealth, bigBallHealth,smallEnemyHealth,bigEnemyHealth, smallBallAttack,bigBallAttack,smallEnemyAttack,bigEnemyAttack, level, storage);
            }
            return data;
        }
        public void SetGameData(GameData data, bool save = true)
        {
            this.data = data;
            if (save)
            {
                SaveGameData();
            }
        }
        public void SaveGameData()
        {
            if(data == null)
            {
                return;
            }
            PlayerPrefs.SetInt(COIN_KEY, data.Coins);
            PlayerPrefs.SetFloat(CANNON_FIRE_RATE, data.CannonFireRate);
            PlayerPrefs.SetInt(SMALL_BALL_HEALTH, data.SmallBallHealth);
            PlayerPrefs.SetInt(BIG_BALL_HEALTH, data.BigBallHealth);
            PlayerPrefs.SetInt(SMALL_ENEMY_HEALTH, data.SmallEnemyHealth);
            PlayerPrefs.SetInt(BIG_ENEMY_HEALTH, data.BigEnemyHealth);

            PlayerPrefs.SetInt(SMALL_BALL_ATTACK, data.SmallBallAttack);
            PlayerPrefs.SetInt(BIG_BALL_ATTACK, data.BigBallAttack);
            PlayerPrefs.SetInt(SMALL_ENEMY_ATTACK, data.SmallEnemyAttack);
            PlayerPrefs.SetInt(BIG_ENEMY_ATTACK, data.BigEnemyAttack);

            PlayerPrefs.SetInt(LEVEL, data.Level);

            PlayerPrefs.SetInt(STORAGE, data.Storage);

            PlayerPrefs.Save();
        }


        public static GameStorage GetInstance()
        {
            if(GameStorage.INSTANCE == null)
            {
                GameStorage.INSTANCE = new GameStorage();
            }

            return GameStorage.INSTANCE;
        }


    }
}
