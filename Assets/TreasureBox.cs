using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public TextMeshPro CoinText;
    public GameObject CoinObject;
    public Transform SpawnPoint;
    public float TotalCoinSpawnTime = 3;
    public float TextUpdateDelay = 1;
    public float ForcePower = 20;
    public float Randomness = .5f;
    private float currentSpawnTime = 0;
    private int coins = 0;
    private int spawnedCoins = 0;

    private List<GameObject> coinObjects = new List<GameObject>();
    private int currentTextCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var gameData = GameStorage.GetInstance().GetGameData();
       

        //gameData.Coins = 10000; // For testing
        //GameStorage.GetInstance().SetGameData(gameData); // For testing

        coins = gameData.Coins;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin" && currentSpawnTime < TotalCoinSpawnTime + TextUpdateDelay)
        {
            currentTextCount++;
            CoinText.text = Mathf.Min((currentTextCount * 10), coins).ToString() + " COINS";
        }
    }


    public void UpdateCoins()
    {
        var gameData = GameStorage.GetInstance().GetGameData();
        var diff = gameData.Coins - coins;
        coins = gameData.Coins;
        if (diff > 0)
        {
            currentSpawnTime = 0;
        }
        else
        {
            if(currentSpawnTime >= TotalCoinSpawnTime)
            {
                var deletionCount = Mathf.Min(Mathf.FloorToInt(-diff / 10), coinObjects.Count);
                for (int i = 0; i < deletionCount; i++)
                {
                    Destroy(coinObjects[0]);
                    coinObjects.RemoveAt(0);
                    CoinText.text = coins.ToString() + " COINS";
                }
            }
           
        }

    }
    //// Update is called once per frame
    void Update()
    {

        if (currentSpawnTime >= TotalCoinSpawnTime)
        {

            if (currentTextCount != coins)
            {
                currentSpawnTime += Time.deltaTime;
                if (currentSpawnTime >= TotalCoinSpawnTime + TextUpdateDelay)
                {
                    currentTextCount = coins;
                    CoinText.text = currentTextCount.ToString() + " COINS";
                }
            }
            return;
        }
        if(coins == 0)
        {
            return;
        }
        var totalCoinsNeeded = coins / 10;
        var coinsPerSecond =  TotalCoinSpawnTime / totalCoinsNeeded;
        currentSpawnTime += Time.deltaTime;
        var coinsNeeded = Mathf.FloorToInt(currentSpawnTime / coinsPerSecond);

            while (spawnedCoins < coinsNeeded)
            {
                var random = Random.Range(0 - Randomness, Randomness);
                var position = SpawnPoint.position;
                position.x += random;
                position.y += random;
                position.z += random;
                var coin = Instantiate(CoinObject, position, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            coinObjects.Add(coin);
            coin.GetComponent<Rigidbody>().AddForce(new Vector3(0, -ForcePower, 0));
                spawnedCoins++;
            }
          
        
    }
}
