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


        private const string SELECTED_CANNON = "Selected_Cannon";
        private const string UNLOCKED_CANNONS = "Unlocked_Cannons";
        private const string LEVEL = "Level";


        private const string MAIN_MENU_NUMBER_OF_BLOCKS = "Main_Menu_Number_Of_Blocks";
        private const string MAIN_MENU_BUILDING_HEALTH = "Main_Menu_Building_Health";
        private const string MAIN_MENU_LEVEL = "Main_Menu_Level";
        private const string MAIN_MENU_BLOCKS = "Main_Menu_Blocks";

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

                CannonType selectedCannon = (CannonType)PlayerPrefs.GetInt(SELECTED_CANNON, (int)CannonType.RegularGun);
                List<CannonType> unlockedCannons = PlayerPrefs.GetString(UNLOCKED_CANNONS, ((int)CannonType.ShotGun).ToString()).Split(",").Select(cannon=> (CannonType)int.Parse(cannon)).ToList();

                var mainMenuLevel = GetMainMenuLevel();
                data = new GameData(coins, cannonFireRate, smallBallHealth, bigBallHealth,smallEnemyHealth,bigEnemyHealth, smallBallAttack,bigBallAttack,smallEnemyAttack,bigEnemyAttack, level, mainMenuLevel, new CannonData(selectedCannon),unlockedCannons.Select(uC=> new CannonData(uC)).ToList());
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

            PlayerPrefs.SetInt(SELECTED_CANNON, (int)data.SelectedCannon.type);
            PlayerPrefs.SetString(UNLOCKED_CANNONS, string.Join(",", data.UnlockedCannons.Select(cannon => (int)cannon.type).ToList()));

            SaveMainMenuData(data.MainMenuLevel);

            PlayerPrefs.Save();
        }

        private MainMenuLevel GetMainMenuLevel()
        {
            var menuLevel = PlayerPrefs.GetInt(MAIN_MENU_LEVEL, 1);
            var numberOfBlocks = PlayerPrefs.GetInt(MAIN_MENU_LEVEL+ MAIN_MENU_NUMBER_OF_BLOCKS, MainMenuLevel.ROW_SIZE*32);
            var buildingHealth = PlayerPrefs.GetInt(MAIN_MENU_LEVEL + MAIN_MENU_BUILDING_HEALTH, 5000);
            var blocks = new List<int>();
            for (int i = 0; i < numberOfBlocks; i++)
            {
                blocks.Add(PlayerPrefs.GetInt(MAIN_MENU_LEVEL + MAIN_MENU_BLOCKS + i, 1));
            }

            return new MainMenuLevel(menuLevel, blocks, buildingHealth, numberOfBlocks);
        }
        private void SaveMainMenuData(MainMenuLevel mainMenuLevel)
        {
            PlayerPrefs.SetInt(MAIN_MENU_LEVEL, mainMenuLevel.level);
            PlayerPrefs.SetInt(MAIN_MENU_LEVEL + MAIN_MENU_NUMBER_OF_BLOCKS, mainMenuLevel.numberOfBlocks);
            PlayerPrefs.SetInt(MAIN_MENU_LEVEL + MAIN_MENU_BUILDING_HEALTH, mainMenuLevel.BuildingHealth);
            for (int i = 0; i < mainMenuLevel.numberOfBlocks; i++)
            {
                PlayerPrefs.SetInt(MAIN_MENU_LEVEL + MAIN_MENU_BLOCKS + i, mainMenuLevel.blocks[i]);
            }
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
