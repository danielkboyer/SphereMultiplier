using Assets.Scripts;
using TMPro;
using UnityEngine;

public class TeasureBox : MonoBehaviour
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


    private int currentTextCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins = GameStorage.GetInstance().GetGameData().Coins;
        Debug.Log("Coins " + coins.ToString());
        //coins = 1000; // For testing
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin" && currentSpawnTime < TotalCoinSpawnTime + TextUpdateDelay)
        {
            currentTextCount++;
            CoinText.text = Mathf.Min((currentTextCount * 10), coins).ToString() + " COINS";
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
                var rb = Instantiate(CoinObject, position, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)))).GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(0, -ForcePower, 0));
                spawnedCoins++;
            }
          
        
    }
}
