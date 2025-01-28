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



    /*
     * 1 means double
     */
    public int newBallMultiplier = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(totalMoveTime > 0)
        {
            endPos = end.position;
            StartCoroutine(WaitAndMove(startMoveTime));
        }
        portalId = GetInstanceID();
        var textPro = gameObject.GetComponentInChildren<TextMeshPro>();
        textPro.text = "X" + newBallMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        var ball = other.GetComponent<Ball>();
        if (ball != null && ball.portalIds.Add(portalId))
        {
            var renderer = ball.GetComponent<MeshRenderer>();
            var diameter = renderer.bounds.size.x;
            for (var x = 2; x < newBallMultiplier + 1; x++)
            {

             
               
                var awayMulti = (x / 2) * diameter / 2;

                var newPos = ball.transform.position;
                newPos.z += awayMulti;

                var movement = UnityEngine.Random.Range(-1f, 1f);
                if (newBallMultiplier + 2 != 3)
                {
                    newPos.x +=  movement;
                }
                var newBall = Instantiate(ball.gameObject, newPos, Quaternion.identity);
                var newBallScript = newBall.GetComponent<Ball>();
                //newBallScript.fastSpeedTime= ball.fastSpeedTime;
                //newBallScript.fastMultiplier = ball.fastMultiplier;
                newBallScript.portalIds = new HashSet<int>(ball.portalIds);
            }
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
       if(MovingInfo != null && MovingInfo.enabled)
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
