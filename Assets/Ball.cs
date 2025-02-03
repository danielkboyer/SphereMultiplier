using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : SharedBall
{

    public override bool isEnemy
    {
        get { return false; }
    }
    public bool isBigBall;
    public float speed = 3;
    public float maxSpeed = 5;
    public float forceApplyTime = .1f;
    private float lastAppliedTime;

    public float fastMultiplier = 2.5f;
    public float fastSpeedTime = 1;
    public HashSet<int> portalIds = new HashSet<int>();

    private EnemyBuilding[] enemyBuildings;
    public float enemyBuildingAttractDistance;


    private float cantBePushedBack = .3f;
    private float initialZPos = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastAppliedTime = forceApplyTime;
        var rigidBody = GetComponent<Rigidbody>();
        enemyBuildings = FindObjectsByType<EnemyBuilding>(FindObjectsSortMode.None).ToArray();

        initialZPos = transform.position.z;
    }

    private void OnTriggerExit(Collider other)
    {
        base.RemoveFromContacts(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        base.AddToContacts(other);
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            var enemyHealth = other.gameObject.GetComponent<Health>();
            var myHealth = GetComponent<Health>();

            enemyHealth.AttackEnemy(myHealth);
            myHealth.AttackEnemy(enemyHealth);


        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        var rigidBody = GetComponent<Rigidbody>();
        lastAppliedTime -= Time.deltaTime;




        if (fastSpeedTime > 0)
        {
            fastSpeedTime -= Time.deltaTime;

            rigidBody.maxLinearVelocity = maxSpeed * fastMultiplier;
            rigidBody.linearVelocity = new Vector3(0, 0, maxSpeed * fastMultiplier);

            if (fastSpeedTime <= 0)
            {
                rigidBody.maxLinearVelocity = maxSpeed;
            }
        }

        if (lastAppliedTime <= 0)
        {
            lastAppliedTime = forceApplyTime;

            var distanceBetween = float.MaxValue;
            Transform enemyBuilding = null;
            foreach (var building in enemyBuildings.Where(e => e != null && e.AttractsBalls).Select(e => e.transform))
            {
                var distance = Vector3.Distance(building.transform.position, this.transform.position);
                if (distance < distanceBetween)
                {
                    distanceBetween = distance;
                    enemyBuilding = building;
                }
            }

            if (distanceBetween < enemyBuildingAttractDistance)
            {
                var target = new Vector2(enemyBuilding.position.x, enemyBuilding.position.z);
                var ballPos = new Vector2(transform.position.x, transform.position.z);

                var force = target - ballPos;

                rigidBody.AddForce(new Vector3(force.x, 0, force.y).normalized * speed);
                return;

            }
            rigidBody.AddForce(new Vector3(0, 0, speed));
        }

        if(cantBePushedBack > 0)
        {
            cantBePushedBack -= Time.deltaTime;
            initialZPos = Mathf.Max(initialZPos, this.gameObject.transform.position.z);
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, initialZPos);
        }

    }
}
