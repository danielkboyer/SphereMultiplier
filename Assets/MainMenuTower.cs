using UnityEngine;

public class MainMenuTower : MonoBehaviour
{

    private BlockSpawner blockSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockSpawner = FindAnyObjectByType<BlockSpawner>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var shootBall = other.GetComponent<ShootBall>();

        if (shootBall != null)
        {
            Destroy(shootBall.gameObject);
            blockSpawner.BuildingHit();
        }
    }
}
