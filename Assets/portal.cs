using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



[Serializable]
public class MovingInfo
{
    public Transform to;
    public Transform from;

    public float speed = 2;

    public bool enabled = false;
}

public class portal : MonoBehaviour
{

    public Transform end;

    public float startMoveTime;
    public float totalMoveTime;


    public MovingInfo MovingInfo;
    private int portalId;

    private Vector3 endPos;


    public GameObject BigBall;
    public GameObject SmallBall;



    /*
     * 1 means double
     */
    public int newBallMultiplier = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (totalMoveTime > 0)
        {
            endPos = end.position;
            StartCoroutine(WaitAndMove(startMoveTime));
        }
        portalId = GetInstanceID();
        var textPro = gameObject.GetComponentInChildren<TextMeshPro>();
        textPro.text = "X" + newBallMultiplier;
    }

    GameObject SpawnBallAtPosition(GameObject ball, Vector3 position, HashSet<int> portalIds, float fastSpeed, Vector3 currentVelocity)
    {
        var newBall = Instantiate(ball.gameObject, position, Quaternion.identity);
        var newBallScript = newBall.GetComponent<Ball>();
        newBallScript.portalIds = new HashSet<int>(portalIds);
        newBallScript.fastSpeedTime = fastSpeed;
        newBallScript.GetComponent<Rigidbody>().linearVelocity = currentVelocity;
        return newBall;
    }


    IEnumerator SpawnMoreBalls(int number, Ball ball)
    {
        var renderer = ball.GetComponent<MeshRenderer>();
        var diameter = renderer.bounds.size.x;
        var halfDiameter = diameter / 2;
        var position = ball.transform.position;

        var numberOfFive = Math.Min(number, 5);

        var portalIds = new HashSet<int>(ball.portalIds);
        var spawnObject = ball.isBigBall ? BigBall : SmallBall;
        var fastSpeed = ball.fastSpeedTime;
        var currentVelocity = ball.GetComponent<Rigidbody>().linearVelocity;

        Destroy(ball.gameObject);
        SpawnBallAtPosition(spawnObject, new Vector3(position.x - halfDiameter, position.y, position.z), portalIds, fastSpeed, currentVelocity);
        SpawnBallAtPosition(spawnObject, new Vector3(position.x + halfDiameter, position.y, position.z), portalIds, fastSpeed, currentVelocity);

        if (numberOfFive == 3)
        {
            SpawnBallAtPosition(spawnObject, new Vector3(position.x, position.y, position.z + diameter), portalIds, fastSpeed, currentVelocity);
        }
        else if (numberOfFive == 4)
        {
            SpawnBallAtPosition(spawnObject, new Vector3(position.x - halfDiameter, position.y, position.z + diameter), portalIds, fastSpeed, currentVelocity);
            SpawnBallAtPosition(spawnObject, new Vector3(position.x + halfDiameter, position.y, position.z + diameter), portalIds, fastSpeed, currentVelocity);
        }
        else if (numberOfFive == 5)
        {
            SpawnBallAtPosition(spawnObject, new Vector3(position.x - diameter, position.y, position.z + diameter), portalIds, fastSpeed, currentVelocity);
            SpawnBallAtPosition(spawnObject, new Vector3(position.x + diameter, position.y, position.z + diameter), portalIds, fastSpeed, currentVelocity);
            SpawnBallAtPosition(spawnObject, new Vector3(position.x, position.y, position.z + diameter), portalIds, fastSpeed, currentVelocity);
        }

        var moveBack = halfDiameter/2;
        for (var y = 5; y < number; y += 2)
        {
            yield return new WaitForSeconds(0.1f);
            var diff = Math.Min(number - y, 2);
            if(diff == 1)
            {
                SpawnBallAtPosition(spawnObject, new Vector3(position.x, position.y, position.z - moveBack), portalIds, fastSpeed, currentVelocity);
            }
            else
            {
            SpawnBallAtPosition(spawnObject, new Vector3(position.x - halfDiameter, position.y, position.z - moveBack), portalIds, fastSpeed, currentVelocity);
            SpawnBallAtPosition(spawnObject, new Vector3(position.x + halfDiameter, position.y, position.z - moveBack), portalIds, fastSpeed, currentVelocity);
            }

            moveBack += moveBack/2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        var ball = other.GetComponent<Ball>();
        if (ball != null && ball.portalIds.Add(portalId))
        {
            var renderer = ball.GetComponent<MeshRenderer>();
            var diameter = renderer.bounds.size.x;
            var position = ball.transform.position;


            StartCoroutine(SpawnMoreBalls(newBallMultiplier, ball));
        }
    }



    IEnumerator WaitAndMove(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= totalMoveTime)
        { // until one second passed
            transform.position = Vector3.Lerp(this.transform.position, endPos, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MovingInfo != null && MovingInfo.enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovingInfo.to.position, MovingInfo.speed * Time.deltaTime);
            if (transform.position == MovingInfo.to.position)
            {
                var temp = MovingInfo.to;
                MovingInfo.to = MovingInfo.from;
                MovingInfo.from = temp;
            }
        }

    }
}
