using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = -3;
    public float maxSpeed = -5;
    public float forceApplyTime = .1f;
    private float lastAppliedTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastAppliedTime = forceApplyTime;
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.maxLinearVelocity = maxSpeed;
        rigidBody.linearVelocity = new Vector3(0, 0, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        var rigidBody = GetComponent<Rigidbody>();
        lastAppliedTime -= Time.deltaTime;




        if (lastAppliedTime <= 0)
        {
            lastAppliedTime = forceApplyTime;
            rigidBody.AddForce(new Vector3(0, 0, speed));
        }
    }
}
