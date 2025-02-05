using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public float Speed = 1;

    private float randomX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomX = Random.Range(-1f, 1f);
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x + randomX * Time.deltaTime, this.transform.position.y, this.transform.position.z + Speed * Time.deltaTime);
    }
}
