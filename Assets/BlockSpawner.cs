using Assets.Scripts;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSpawner : MonoBehaviour
{


    public float StartSpawnZ;
    public float EndSpawnZ;
    public GameObject Plane;

    public GameObject BlockToSpawn;

    private GameData gameData;

    public GameObject ShootBall;

    public GameObject BattleUI;
    public GameObject ShootUI;
    public GameObject TutorialUI;
    public GameObject BurnEffects;
    public GameObject UnlockCannonUI;

    public TextMeshPro EnemyBuildingHealth;

    public TextMeshProUGUI DisplayCoins;

    public Camera myCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var options = new InitializationOptions();
        if (!Debug.isDebugBuild)
        {
            options.SetEnvironmentName("production");
        }
        else
        {
            options.SetEnvironmentName("dev");
        }



        UnityServices.InitializeAsync(options);
        AnalyticsService.Instance.StartDataCollection();

        BattleUI.SetActive(false);
        ShootUI.SetActive(true);
        TutorialUI.SetActive(false);
        gameData = GameStorage.GetInstance().GetGameData();

        if(gameData.Level < 3)
        {
            TutorialUI.SetActive(true);
        }
        if(gameData.Coins < 100)
        {
            BattleUI.SetActive(true);
            ShootUI.SetActive(false);
        }
        SetupMainMenuLevel();
    }

    void SetupMainMenuLevel()
    {
        DisplayCoins.text = gameData.Coins.ToString();
        EnemyBuildingHealth.text = gameData.MainMenuLevel.BuildingHealth.ToString();
        float planeWidth = Plane.GetComponent<Renderer>().bounds.size.x;
        float blockWidth = BlockToSpawn.GetComponent<Renderer>().bounds.size.x;
        int numberOfBlocks = gameData.MainMenuLevel.numberOfBlocks;

        int rowSize = MainMenuLevel.ROW_SIZE;
        int columnSize = numberOfBlocks / rowSize;

        float spaceBetweenBlocksX = (planeWidth - (rowSize * blockWidth)) / (rowSize - 1);
        float spaceBetweenBlocksZ = (EndSpawnZ - StartSpawnZ - columnSize * blockWidth) / (columnSize - 1);
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                var blockId = i + j * rowSize;
                if (gameData.MainMenuLevel.blocks[blockId] <= 0)
                {
                    continue;
                }
                var xPos = i * (blockWidth + spaceBetweenBlocksX);
                var zPos = StartSpawnZ + j * (blockWidth + spaceBetweenBlocksZ);
                var spawnPosition = new Vector3(xPos - planeWidth / 2 + blockWidth / 2, Plane.transform.position.y + blockWidth / 2, zPos);
                var shootBlock = Instantiate(BlockToSpawn, spawnPosition, Quaternion.identity).GetComponent<ShootBlock>();
                shootBlock.SetBlockId(i + j * rowSize);
            }
        }
    }

    public void BuildingHit()
    {
        gameData.MainMenuLevel.BuildingHealth -= 100;
        EnemyBuildingHealth.text = gameData.MainMenuLevel.BuildingHealth.ToString();
        if(gameData.MainMenuLevel.BuildingHealth <= 0)
        {
            var unlockCannon = new CannonData(gameData.MainMenuLevel.cannonToUnlock);
            gameData.UnlockedCannons.Add(unlockCannon);
            gameData.SelectedCannon = unlockCannon;
            gameData.MainMenuLevel = MainMenuLevel.GetDefaultLevel(gameData.MainMenuLevel.level + 1);

            FindObjectsByType<ShootBall>(FindObjectsSortMode.None).ToList().ForEach(x => { Destroy(x.gameObject); gameData.Coins += 100; });
            GameStorage.GetInstance().SetGameData(gameData, true);
            BurnEffects.SetActive(true);
            
          

            StartCoroutine(TriggerExlosions());

        }
      
       
    }


    public void Battle()
    {
        var mainMenuProgressEvent = new MainMenuProgress();
        mainMenuProgressEvent.GridDestroyed = gameData.MainMenuLevel.blocks.FindAll(x => x <= 0).Count;
        mainMenuProgressEvent.TowerDestroyed = gameData.MainMenuLevel.BuildingHealth;
       
        mainMenuProgressEvent.Level = gameData.Level;
        AnalyticsService.Instance.RecordEvent(mainMenuProgressEvent);
        GameStorage.GetInstance().SetGameData(gameData, true);
        SceneManager.LoadScene("Level" + gameData.Level, LoadSceneMode.Single);
    }


    public void BlockHit(int indexOfBlock)
    {
        gameData.MainMenuLevel.blocks[indexOfBlock] -= 1;
    }

    IEnumerator TriggerExlosions()
    {
        // Randomly spawns explosions between plane start and zEnd zStart
        for(int i = 0; i < 15; i++)
        {
            float planeWidth = Plane.GetComponent<Renderer>().bounds.size.x;
            float randomX = Random.Range(-planeWidth / 2, planeWidth / 2);
            float randomZ = Random.Range(StartSpawnZ, EndSpawnZ);
            Vector3 spawnPosition = new Vector3(randomX, Plane.transform.position.y, randomZ);

            // Assuming you have an explosion prefab to instantiate

            Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, 10);

            foreach(var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<ShootBlock>() != null)
                {
                    var block = hitCollider.gameObject.GetComponent<Rigidbody>();
                    block.AddExplosionForce(2000, spawnPosition, 5);
                }
            }

            if(i == 10)
            {
                foreach (var item in FindObjectsByType<MoveObjectTo>(FindObjectsSortMode.None))
                {
                    item.MoveIt();
                }
            }

            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(2f);

        UnlockCannonUI.SetActive(true);
    }


    IEnumerator SetButtonActive()
    {
        while(FindAnyObjectByType<ShootBall>() != null)
        {
            yield return new WaitForSeconds(1f);
        }

        BattleUI.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        var coinCost = 100;
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch detected");
            ShootUI.SetActive(false);
            if (gameData.Coins < coinCost)
            {
                StartCoroutine(SetButtonActive());
                Debug.Log("Not enough coins");
                return;
            }

            Touch touch = Input.GetTouch(0);

            Debug.Log("Touch phase: " + touch.phase);
            if (touch.phase == TouchPhase.Began)
            {

                Ray myRay = myCamera.ScreenPointToRay(touch.position);
                RaycastHit myHit;

                if (Physics.Raycast(myRay, out myHit))
                {
                    gameData.Coins -= coinCost;
                    DisplayCoins.text = gameData.Coins.ToString();
                    Instantiate(ShootBall, new Vector3(myHit.point.x, 0, -25), Quaternion.identity);
                    TutorialUI.SetActive(false);
                }

            }
        }
    }
}
