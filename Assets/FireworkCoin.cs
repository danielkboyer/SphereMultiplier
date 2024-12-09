using UnityEngine;

public class FireworkCoin : MonoBehaviour
{

    public float DestroyTime = .6f;
    private float currentTime = 0;
    public GameObject Firework;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= DestroyTime)
        {
            Instantiate(Firework, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
