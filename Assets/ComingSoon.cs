using UnityEngine;
using UnityEngine.SceneManagement;

public class ComingSoon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
