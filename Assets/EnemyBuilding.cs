using Assets;
using TMPro;
using UnityEngine;


[System.Serializable]
public class EnemyBundle
{
    public int smallAmount;
    public int bigAmount;
    public float spawnTime;

    //number of times to repeat, -1 means infinity
    public float spawnRepeat;

    private int spawnAmountSoFar = 0;
    public bool CanSpawn(float elapsed)
    {
        if (elapsed/spawnTime   > spawnAmountSoFar && (spawnRepeat > spawnAmountSoFar || spawnRepeat == -1))
        {
            spawnAmountSoFar++;
            return true;
        }

        return false;
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
                var renderer = Enemy.GetComponent<MeshRenderer>();
                var diameter = renderer.bounds.size.x;
                for (var y = 0; y < 2; y++)
                {
                    var enemyToSpawn = Enemy;
                    var spawnAmount = bundle.smallAmount;
                    if(y == 1)
                    {
                        enemyToSpawn = BigEnemy;
                        spawnAmount = bundle.bigAmount;
                    }
                    for (var x = 0; x < spawnAmount; x++)
                    {
                        var left = x % 2 == 0;
                        var awayMulti = (x / 2) * diameter / 2;

                        var newPos = SpawnPosition.position;
                        newPos.z += awayMulti;

                        newPos.x += left ? -1 * diameter / 2 : diameter / 2;

                        Instantiate(enemyToSpawn, newPos, Quaternion.identity);
                    }
                }
            }
        }



    }
}
