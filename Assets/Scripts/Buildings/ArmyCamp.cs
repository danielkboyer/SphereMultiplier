using Assets.Scripts;
using UnityEngine;

public class ArmyCamp : Building
{


    /**
     * The index to choose fromm storage for this army camp
     */
    public int Index;

    private CampStats _stats;
    public override void OnClick()
    {
        Debug.Log("ArmyCamp clicked");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var gameData = GameStorage.GetInstance().GetGameData();
      _stats =  gameData.Base.Camps[Index];
        InitializeFromStats();
    }


    void InitializeFromStats()
    {
        if(!_stats.Unlocked && _stats.UnlockCamp == null)
        {
            this.ChangeColor(this.disabledMaterial);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
