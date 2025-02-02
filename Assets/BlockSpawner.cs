using Assets.Scripts;
using System.Collections;
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

    public Camera myCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BattleUI.SetActive(false);
        ShootUI.SetActive(true);
        gameData = GameStorage.GetInstance().GetGameData();

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
                if(gameData.MainMenuLevel.blocks[blockId] <= 0)
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

    public void Battle()
    {

        SceneManager.LoadScene("Level" + gameData.Level, LoadSceneMode.Single);
    }


    public void BlockHit(int indexOfBlock)
    {
        gameData.MainMenuLevel.blocks[indexOfBlock] -= 1;

        GameStorage.GetInstance().SetGameData(gameData, true);
    }


    IEnumerator SetButtonActive()
    {

        yield return new WaitForSeconds(1);
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
                    Instantiate(ShootBall, new Vector3(myHit.point.x, 0, -25), Quaternion.identity);
                }

            }
        }
    }
}
