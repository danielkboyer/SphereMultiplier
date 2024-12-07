using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class GameManager : MonoBehaviour
    {

        public GameData gameData;
        public TextMeshProUGUI Text;
        public TextMeshProUGUI CountdownText;
        private List<EnemyBuilding> enemyBuildings;

        public GameObject GoHomeScreen;
        public TextMeshProUGUI homeScreenCoinText;
        public float CountdownTime = 4.99f;
        private float countdownTimer = 0;
        private bool gameOver = false;


        public void OnNextClicked()
        {
            GameStorage.GetInstance().SetGameData(gameData);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        private void Start()
        {
            gameData = GameStorage.GetInstance().GetGameData();
            Text.text = gameData.Coins.ToString();
            CountdownText.enabled = false;
            enemyBuildings = FindObjectsByType<EnemyBuilding>(FindObjectsSortMode.None).ToList();
            GoHomeScreen.SetActive(false);
        }

        public void AddCoins(int coins)
        {
            this.gameData.Coins += coins;
            Text.text = this.gameData.Coins.ToString();
            
        }

        public void Update()
        {
            if(!gameOver && enemyBuildings.All(t => t.IsDestroyed()))
            {
                CountdownText.enabled = true;

                countdownTimer += Time.deltaTime;

                
                if(countdownTimer >= CountdownTime)
                {
                    CountdownText.text = "0";
                    Debug.Log("You won");
                    CountdownText.enabled = false;
                    GoHomeScreen.SetActive(true);
                    enemyBuildings.ForEach(building => { building.textMesh.enabled = false; building.SetGameOver(); });
                    gameOver = true;
                    homeScreenCoinText.text = this.gameData.Coins.ToString();
                    return;

                }

                CountdownText.text = Mathf.CeilToInt(CountdownTime - countdownTimer).ToString();

            }
        }
    }
}
