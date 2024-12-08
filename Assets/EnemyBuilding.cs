using System.Collections;
using Assets;
using TMPro;
using UnityEngine;


[System.Serializable]
public class EnemyBundle
{
    public int smallAmount;
    public int bigAmount;
    public float spawnTime;

    public float repeatingTime;

    //number of times to repeat, -1 means infinity
    public float spawnRepeat;

    private int spawnAmountSoFar = 0;

    private bool spawnInProgress = false;
    private float totalSpawningTime = 0;
    public bool CanSpawn(float elapsed)
    {
        var elapsedMinusSpawn = elapsed - totalSpawningTime;
        if(spawnAmountSoFar == 0)
        {
            if (elapsedMinusSpawn / spawnTime > (spawnAmountSoFar + 1))
            {
                spawnAmountSoFar++;
                return true;
            }

            return false;
        }
        elapsedMinusSpawn -= spawnTime;
        if (!spawnInProgress && elapsedMinusSpawn / repeatingTime   > (spawnAmountSoFar+1) && (spawnRepeat > spawnAmountSoFar || spawnRepeat == -1))
        {
            spawnAmountSoFar++;
            return true;
        }

        return false;
    }

    public void IsSpawning()
    {
        spawnInProgress = true;
    }

    public void DoneSpawning(float timeSpawning)
    {
        spawnInProgress = false;
        totalSpawningTime += timeSpawning;
    }


}
public class EnemyBuilding : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject BigEnemy;
    public Transform SpawnPosition;
    public Color TextSuccessColor;
    public GameManager gameManager;
    public float buildingHealth = 1000;
    public EnemyBundle[] enemyBundles;
    private float currentSpawnTime = 0;
    public float SpawnRandomness = 0.5f;
    public float delayTimePerEnemy = 0.1f;
    private bool gameOver = false;
    public TextMeshPro textMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh.text = buildingHealth.ToString();
    }

    public bool IsDestroyed()
    {
        return buildingHealth <= 0;
    }

    public void SetGameOver()
    {
        gameOver = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameOver)
        {
            return;
        }
        var ball = other.GetComponent<Ball>();

        if(ball != null)
        {
            var health = other.GetComponent<Health>();
            buildingHealth -= health.Attack;
            gameManager.AddCoins(health.Attack);

            
            textMesh.text = buildingHealth.ToString();
            if(buildingHealth < 0)
            {
                textMesh.color = TextSuccessColor;
                textMesh.text = (buildingHealth * -1).ToString();
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator SpawnBundle(EnemyBundle bundle)
    {
        bundle.IsSpawning();
        var start = Time.time;
        var renderer = Enemy.GetComponent<MeshRenderer>();
        var diameter = renderer.bounds.size.x;
        for (var y = 0; y < 2; y++)
        {
            var enemyToSpawn = Enemy;
            var spawnAmount = bundle.smallAmount;
            if (y == 0)
            {
                enemyToSpawn = BigEnemy;
                spawnAmount = bundle.bigAmount;
            }
            for (var x = 0; x < spawnAmount; x++)
            {
                var horizontalPos = x % 5;
                var awayMulti = diameter / 2;

                var newPos = SpawnPosition.position;

                //Add a little bit of randomness so it's blob like
                newPos.z += awayMulti + Random.Range(-SpawnRandomness, SpawnRandomness);

                newPos.x += (2 - horizontalPos) * (diameter / 2);

                Instantiate(enemyToSpawn, newPos, Quaternion.identity);
                yield return new WaitForSeconds(delayTimePerEnemy); // start at time X
            }
        }

        bundle.DoneSpawning(Time.time - start);


    }

    // Update is called once per frame
    void Update()
    {
     

        if (IsDestroyed() || gameOver)
        {
            return;
        }

        currentSpawnTime += Time.deltaTime;
        foreach (var bundle in enemyBundles)
        {
            if (bundle.CanSpawn(currentSpawnTime))
            {
               StartCoroutine(SpawnBundle(bundle));
            }
        }



    }
}
