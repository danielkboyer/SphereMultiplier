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
        public Camera MainCamera;

        public GameObject Coin;
        public GameData gameData;
        public TextMeshProUGUI Text;
        public TextMeshProUGUI CountdownText;

        public GameObject Explosion;
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
            Coin.GetComponent<Collider>().enabled = false;
        }


        public void OnBuildingAttacked(EnemyBuilding building, Health attacker)
        {

            if (building.IsDestroyed())
            {
                if(!enemyBuildings.All(t => t.IsDestroyed()))
                {
                    building.AttractsBalls = false;
                    enemyBuildings.Remove(building);
                    var bombExplosion = building.transform.position;
                    bombExplosion.y += 3;
                    Instantiate(Explosion, bombExplosion, Quaternion.identity);
                    Destroy(building.gameObject);
                }
                else
                {
                    var coinSpawnAmount = Mathf.FloorToInt(attacker.Attack / 10);
                    for (var i = 0; i < coinSpawnAmount; i++)
                    {
                        var coin = Instantiate(Coin, building.CoinSpawnPos.position, Quaternion.identity);
                        coin.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f) * 70, UnityEngine.Random.Range(1.0f, 10.0f) * 70, UnityEngine.Random.Range(-1.0f, 1.0f) * 70));
                        coin.transform.Rotate(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                    }

                    AddCoins(attacker.Attack);
                }
                return;
            }
            else
            {
                AddCoins(attacker.Attack);
            }
     
            
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

                //Lerp camera towards last building
                var lastBuilding = enemyBuildings.FirstOrDefault(t => t.AttractsBalls);
                if(lastBuilding != null)
                {
                    var target = new Vector3(lastBuilding.transform.position.x, MainCamera.transform.position.y, lastBuilding.transform.position.z - 18);
                    MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, target, Time.deltaTime);
                }

                //Update text if different and animate the size
                var newCountdownFloatText = CountdownTime - countdownTimer;
                var newCountdownText = Mathf.CeilToInt(newCountdownFloatText);

                if (CountdownText.text != newCountdownText.ToString()) {
                    CountdownText.text = newCountdownText.ToString();
                    CountdownText.fontSize = originalCountdownTextFontSize + countDownSizeAdjustment;
                    CountdownText.gameObject.transform.rotation = Quaternion.identity;
                    //Rotate the text z axis randomly a little bit.
                    CountdownText.gameObject.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-12, 12)));
                    
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
