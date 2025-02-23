using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public class GameStorage
    {
        private static GameStorage INSTANCE;
        private const string GAME_DATA_KEY = "GameData";
        private GameData data;

        public GameData GetGameData()
        {
            if (data == null)
            {
                string jsonData = PlayerPrefs.GetString(GAME_DATA_KEY, string.Empty);

                if (string.IsNullOrEmpty(jsonData))
                {
                    // Create default data
                    data = new GameData(
                        coins: 0,
                        cannonFireRate: 1.5f,
                        smallBallHealth: 30,
                        bigBallHealth: 120,
                        smallEnemyHealth: 30,
                        bigEnemyHealth: 120,
                        smallBallAttack: 10,
                        bigBallAttack: 30,
                        smallEnemyAttack: 10,
                        bigEnemyAttack: 30,
                        level: 1,
                        mainMenuLevel: GetMainMenuLevel(),
                        selectedCannon: new CannonData(CannonType.RegularGun),
                        unlockedCannons: new List<CannonData> { new CannonData(CannonType.ShotGun) }
                    );
                }
                else
                {
                    data = JsonUtility.FromJson<GameData>(jsonData);
                }
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
            if (data == null)
            {
                return;
            }
            string jsonData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(GAME_DATA_KEY, jsonData);
            PlayerPrefs.Save();
        }

        private MainMenuLevel GetMainMenuLevel()
        {
            const string MAIN_MENU_LEVEL = "Main_Menu_Level";
            const string MAIN_MENU_NUMBER_OF_BLOCKS = "Main_Menu_Number_Of_Blocks";
            const string MAIN_MENU_BUILDING_HEALTH = "Main_Menu_Building_Health";
            const string MAIN_MENU_BLOCKS = "Main_Menu_Blocks";

            var menuLevel = PlayerPrefs.GetInt(MAIN_MENU_LEVEL, 1);
            var numberOfBlocks = PlayerPrefs.GetInt(MAIN_MENU_LEVEL + MAIN_MENU_NUMBER_OF_BLOCKS, 16);
            var buildingHealth = PlayerPrefs.GetInt(MAIN_MENU_LEVEL + MAIN_MENU_BUILDING_HEALTH, 1000);
            var blocks = new List<int>();

            for (int i = 0; i < numberOfBlocks; i++)
            {
                blocks.Add(PlayerPrefs.GetInt(MAIN_MENU_LEVEL + MAIN_MENU_BLOCKS + i, 100));
            }

            return new MainMenuLevel(menuLevel, blocks, buildingHealth, numberOfBlocks);
        }

        private void SaveMainMenuData(MainMenuLevel mainMenuLevel)
        {
            const string MAIN_MENU_LEVEL = "Main_Menu_Level";
            const string MAIN_MENU_NUMBER_OF_BLOCKS = "Main_Menu_Number_Of_Blocks";
            const string MAIN_MENU_BUILDING_HEALTH = "Main_Menu_Building_Health";
            const string MAIN_MENU_BLOCKS = "Main_Menu_Blocks";

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
            if (GameStorage.INSTANCE == null)
            {
                GameStorage.INSTANCE = new GameStorage();
            }
            return GameStorage.INSTANCE;
        }
    }
}