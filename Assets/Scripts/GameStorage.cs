using System.Collections.Generic;
using UnityEngine;

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
                        mainMenuLevel: MainMenuLevel.GetDefaultLevel(1),
                        selectedCannon: new CannonData(CannonType.RegularGun),
                        unlockedCannons: new List<CannonData> { new CannonData(CannonType.ShotGun),
                        }
                        , Base.GetInitial(), Map.GetInitial()

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