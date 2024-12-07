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

        private GameData data;
        public GameData GetGameData()
        {
            if (data == null)
            {
                var coins = PlayerPrefs.GetInt(COIN_KEY, 0);
                data = new GameData(coins);
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
