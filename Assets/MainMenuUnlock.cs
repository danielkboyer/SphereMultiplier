using Assets.Scripts;
using UnityEngine;

public class MainMenuUnlock : MonoBehaviour
{

    public GameObject ShotGun;
    public GameObject LaserGun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var gameData = GameStorage.GetInstance().GetGameData();
        ShotGun.SetActive(false);
        LaserGun.SetActive(false);

        switch(gameData.MainMenuLevel.level)
        {
            case 1:
                ShotGun.SetActive(true);
                break;
            case 2:
                LaserGun.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
