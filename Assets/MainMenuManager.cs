using TMPro;
using UnityEngine;
using Assets;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Analytics;
using Unity.Services.Core.Environments;

public class MainMenuManager : MonoBehaviour
{


    public TextMeshProUGUI GunRateText;
    public TextMeshProUGUI GunRateCost;
    public TextMeshProUGUI BallHealthText;
    public TextMeshProUGUI BallHealthCost;
    public TextMeshProUGUI BigBallHealthText;
    public TextMeshProUGUI BigBallHealthCost;

    public TextMeshProUGUI StorageText;
    public TextMeshProUGUI StorageCost;

    public Button GunButton;
    public Button BallButton;
    public Button BigBallButton;
    public Button StorageButton;


    private TreasureBox treasureBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


       
        treasureBox = FindFirstObjectByType<TreasureBox>();

        var gameData = GameStorage.GetInstance().GetGameData();

        if(gameData.Level == 1)
        {
            Battle();
            return;
        }
        UpdateTextFromGameData(gameData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdateTextFromGameData(GameData gameData)
    {

        var cannonPurchase = Buyer.GetCannonFirePurchase(gameData.CannonFireRate);
        var ballPurchase = Buyer.GetBallPurchase(gameData.SmallBallHealth);
        var bigBallPurchase = Buyer.GetBigBallPurchase(gameData.BigBallHealth);

        GunButton.interactable = gameData.Coins > cannonPurchase.Cost;
        BallButton.interactable = gameData.Coins > ballPurchase.Cost;
        BigBallButton.interactable = gameData.Coins > bigBallPurchase.Cost;

        GunRateText.text = gameData.CannonFireRate.ToString() + "s CD";
        GunRateCost.text = cannonPurchase.Cost.ToString();

        BallHealthText.text = gameData.SmallBallHealth.ToString() + " Health";
        BallHealthCost.text = ballPurchase.Cost.ToString();

        BigBallHealthText.text = gameData.BigBallHealth.ToString() + " Health";
        BigBallHealthCost.text = bigBallPurchase.Cost.ToString();

    }



    public void UpgradeGun()
    {
        var gameData = GameStorage.GetInstance().GetGameData();

        var cannonPurchase = Buyer.GetCannonFirePurchase(gameData.CannonFireRate);

        if (gameData.Coins < cannonPurchase.Cost)
        {
            return;
        }

        gameData.Coins -= cannonPurchase.Cost;
        gameData.CannonFireRate = cannonPurchase.NewValue;

        GameStorage.GetInstance().SetGameData(gameData);

        UpdateTextFromGameData(gameData);
        treasureBox.UpdateCoins();

    }
    public void UpgradeBall()
    {
        var gameData = GameStorage.GetInstance().GetGameData();

        var smallBallPurchase = Buyer.GetBallPurchase(gameData.SmallBallHealth);

        if (gameData.Coins < smallBallPurchase.Cost)
        {
            return;
        }

        gameData.Coins -= smallBallPurchase.Cost;
        gameData.SmallBallHealth = smallBallPurchase.NewValue;

        GameStorage.GetInstance().SetGameData(gameData);

        UpdateTextFromGameData(gameData);
        treasureBox.UpdateCoins();
    }

    public void UpgradeBigBall()
    {
        var gameData = GameStorage.GetInstance().GetGameData();

        var bigBallPurchase = Buyer.GetBigBallPurchase(gameData.BigBallHealth);

        if (gameData.Coins < bigBallPurchase.Cost)
        {
            return;
        }

        gameData.Coins -= bigBallPurchase.Cost;
        gameData.BigBallHealth = bigBallPurchase.NewValue;

        GameStorage.GetInstance().SetGameData(gameData);

        UpdateTextFromGameData(gameData);
        treasureBox.UpdateCoins();
    }


    public void Battle()
    {
        var gameData = GameStorage.GetInstance().GetGameData();

        SceneManager.LoadScene("Level" + gameData.Level, LoadSceneMode.Single);
    }
}
