using UnityEngine;

public class ShootBlock : MonoBehaviour
{
    private BlockSpawner blockSpawner;

    private int blockId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        blockSpawner = FindFirstObjectByType<BlockSpawner>();
    }

    public void SetBlockId(int blockId)
    {
        this.blockId = blockId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var shootBall = other.gameObject.GetComponent<ShootBall>();

        if(shootBall != null)
        {
            Destroy(shootBall.gameObject);
            blockSpawner.BlockHit(blockId);
            Destroy(this.gameObject);

        }
    }
}
