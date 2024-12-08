using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = -3;
    public float maxSpeed = -5;
    public float forceApplyTime = .1f;
    private float lastAppliedTime;

    public float fastMultiplier = 2.5f;
    public float fastSpeedTime = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastAppliedTime = forceApplyTime;
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.maxLinearVelocity = maxSpeed * fastMultiplier;
        rigidBody.linearVelocity = new Vector3(0, 0, maxSpeed * fastMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        var rigidBody = GetComponent<Rigidbody>();
        lastAppliedTime -= Time.deltaTime;

        if (fastSpeedTime > 0)
        {
            fastSpeedTime -= Time.deltaTime;

            if (fastSpeedTime <= 0)
            {
                rigidBody.maxLinearVelocity = maxSpeed;
            }
        }


        if (lastAppliedTime <= 0)
        {
            lastAppliedTime = forceApplyTime;
            rigidBody.AddForce(new Vector3(0, 0, speed));
        }
    }
}
