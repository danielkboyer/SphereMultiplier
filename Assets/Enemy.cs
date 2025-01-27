using Unity.VisualScripting;
using UnityEngine;

public class Enemy : SharedBall
{
    public override bool isEnemy
    {
        get { return true; }
    }
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
    private void OnTriggerExit(Collider other)
    {
        base.RemoveFromContacts(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        base.AddToContacts(other);
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
