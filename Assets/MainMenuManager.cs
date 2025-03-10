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

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out RaycastHit hit))
                {
                    var building = hit.transform.gameObject.GetComponent<Building>();
                    if (building != null)
                    {
                        building.OnClick();
                    }
                }
            }

        }
    }

    public void Battle()
    {
        var gameData = GameStorage.GetInstance().GetGameData();

        SceneManager.LoadScene("Level" + gameData.Level, LoadSceneMode.Single);
    }
}
