using Assets.Scripts;
using System.Linq;
using UnityEngine;

public class BattleGameManager : MonoBehaviour
{

    public GameObject ArcherPrefab;
    public GameObject GoblinPrefab;
    public GameObject MeleePrefab;
    public GameObject EnemyCampPrefab;


    public float GoodGuySpawnZ = -24;
    public float GoodGuySpawnX = 7.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var gameData = GameStorage.GetInstance().GetGameData();

        var level = Level.GetLevel(gameData.Map.CurrentLevel);

        //For enemies
        foreach (var soldierWithPosition in level.Soldiers)
        {
            SpawnSoldier(soldierWithPosition);
        }
        //Good guys
        var goodSoldiers = gameData.Base.Camps.SelectMany(camp => camp.Soldiers);
        foreach (var goodSoldier in goodSoldiers)
        {
            SpawnSoldier(new SoldierWithPosition() { SoldierType = goodSoldier, Position = new Vector3(Random.Range(-GoodGuySpawnX, GoodGuySpawnX),0 , GoodGuySpawnZ), Level=1 });
        }


    }

    void SpawnSoldier(SoldierWithPosition soldierWithPosition)
    {
        GameObject prefab = null;
        switch (soldierWithPosition.SoldierType)
        {
            case SoldierType.Archer:
                prefab = ArcherPrefab;
                break;
            case SoldierType.Goblin:
                prefab = GoblinPrefab;
                break;
            case SoldierType.Melee:
                prefab = MeleePrefab;
                break;
            case SoldierType.EnemyCamp:
                prefab = EnemyCampPrefab;
                break;
            default:
                Debug.LogError("Unknown soldier type");
                break;
        }
       var attackable =  Instantiate(prefab, soldierWithPosition.Position, Quaternion.identity).GetComponent<Attackable>();

        attackable.Init(soldierWithPosition.Level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
