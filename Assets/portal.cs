using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class portal : MonoBehaviour
{

    public Transform end;

    public float startMoveTime;
    public float totalMoveTime;

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
        var textPro = gameObject.GetComponent<TextMeshPro>();
        textPro.text = "X" + newBallMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        var ball = other.GetComponent<Ball>();
        if (ball != null && ball.portalIds.Add(portalId))
        {
            var renderer = ball.GetComponent<MeshRenderer>();
            var diameter = renderer.bounds.size.x;
            for (var x = 2; x < newBallMultiplier + 2; x++)
            {
                var left = x % 2 == 0;
                var awayMulti = (x / 2) * diameter / 2;

                var newPos = ball.transform.position;
                newPos.z += awayMulti;

                if (newBallMultiplier + 2 != 3)
                {
                    newPos.x += left ? -1 * diameter / 2 : diameter / 2;
                }
                var newBall = Instantiate(ball.gameObject, newPos, Quaternion.identity);
                var newBallScript = newBall.GetComponent<Ball>();
                newBallScript.fastMultiplier = 1;
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
       
       
    }
}
