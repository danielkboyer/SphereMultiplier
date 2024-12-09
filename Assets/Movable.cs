using UnityEngine;

public class Movable : MonoBehaviour
{
    public Transform To;
    public Transform From;

    public float Speed = 1;
    public float PauseTime = 0;
    private float currentPauseTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPauseTime > 0)
        {
            currentPauseTime -= Time.deltaTime;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, To.position, Speed * Time.deltaTime);
        if (transform.position == To.position)
        {
            var temp = To;
            To =From;
            From = temp;
            currentPauseTime = PauseTime;
        }

    }
}
