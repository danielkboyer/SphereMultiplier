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

        private int startCoins = 0;


        public float countDownSizeAdjustment = 50;
        private float originalCountdownTextFontSize;
        public void OnNextClicked()
        {
            gameData.Level++;
            GameStorage.GetInstance().SetGameData(gameData);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        private void Start()
        {
            gameData = GameStorage.GetInstance().GetGameData();
            startCoins = gameData.Coins;
            Text.text = gameData.Coins.ToString();
            CountdownText.enabled = false;
            enemyBuildings = FindObjectsByType<EnemyBuilding>(FindObjectsSortMode.None).ToList();
            GoHomeScreen.SetActive(false);
            originalCountdownTextFontSize = CountdownText.fontSize;
        }


        public void OnBuildingAttacked(EnemyBuilding building, Health attacker)
        {
            if (building.IsDestroyed())
            {
                return;
            }
            AddCoins(attacker.Attack);
            
        }
        public void AddCoins(int coins)
        {
            this.gameData.Coins = Mathf.Min(coins + this.gameData.Coins, this.gameData.Storage);
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
                    homeScreenCoinText.text = (this.gameData.Coins - startCoins).ToString();
                    return;

                }

                //Update text if different and animate the size
                var newCountdownFloatText = CountdownTime - countdownTimer;
                var newCountdownText = Mathf.CeilToInt(newCountdownFloatText);

                if (CountdownText.text != newCountdownText.ToString()) {
                    CountdownText.text = newCountdownText.ToString();
                    CountdownText.fontSize = originalCountdownTextFontSize + countDownSizeAdjustment;
                }
                else
                {
                    var subtraction = (originalCountdownTextFontSize) * (newCountdownText - newCountdownFloatText);
                    CountdownText.fontSize = originalCountdownTextFontSize - subtraction;
                }

            }
        }
    }
}
