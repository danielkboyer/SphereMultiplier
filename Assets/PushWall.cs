using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PushWall : MonoBehaviour
{
    public float distanceToPush = 1.5f;

    private float speedModifier = .02f;

    public float pushDifficulty = 1;
    public TextMeshPro text;
    private float startDistance;

    private bool stopCalculating = false;

    private int percentage = 0;


    //for optimization
    private float checkBallCount = .2f;
    private float timePassed = 0;

    public float currentBallsTouching = 0;

    private Dictionary<int, SharedBall> ballsTouchingWallDirectly = new Dictionary<int, SharedBall>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        text.text = "0%";
        startDistance = this.transform.position.z;
        text.transform.parent = this.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<SharedBall>();
        if (ball != null)
        {
            if (!ballsTouchingWallDirectly.ContainsKey(ball.GetInstanceID()))
            {
                ballsTouchingWallDirectly.Add(ball.GetInstanceID(), ball);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        var ball = other.GetComponent<SharedBall>();
        if (ball != null)
        {
            ballsTouchingWallDirectly.Remove(ball.GetInstanceID());
        }
    }

    HashSet<int> RecurseBalls(SharedBall ball, HashSet<int> ballsAlreadyAdded)
    {


        if (!ballsAlreadyAdded.Add(ball.GetInstanceID()))
        {
            return ballsAlreadyAdded;
        }



        foreach (var ballInContact in ball.ballsInContact.Values)
        {
            ballsAlreadyAdded.AddRange(RecurseBalls(ballInContact, new HashSet<int>(ballsAlreadyAdded)));
        }

        return ballsAlreadyAdded;
    }


    // Update is called once per frame
    void Update()
    {


        if (stopCalculating)
        {
            var shrinkAmount = Time.deltaTime;
            var newScale = this.transform.localScale - new Vector3(0, 0, shrinkAmount);
            newScale.z = Mathf.Max(newScale.z, 0);
            this.transform.localScale = newScale;
            return;
        }

        timePassed += Time.deltaTime;
        if (timePassed > checkBallCount)
        {



            var goodBallsInContact = new HashSet<int>();
            var badBallsInContact = new HashSet<int>();
            foreach (var ball in ballsTouchingWallDirectly.Values)
            {
                if (ball.isEnemy)
                {
                    badBallsInContact.AddRange(RecurseBalls(ball, new HashSet<int>(badBallsInContact)));
                }
                else
                {
                    goodBallsInContact.AddRange(RecurseBalls(ball, new HashSet<int>(goodBallsInContact)));

                }
            }
            Debug.Log($"Good balls: {goodBallsInContact.Count}, Bad balls: {badBallsInContact.Count}");

            var goodBalls = goodBallsInContact.Count / pushDifficulty;
            var badBalls = badBallsInContact.Count / pushDifficulty;

         
            var diff = goodBalls - badBalls;

            currentBallsTouching = diff;

            timePassed = 0;

        }
      


        float speed = currentBallsTouching * speedModifier * Time.deltaTime;


        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);

        var newPercentage = Mathf.RoundToInt((transform.position.z - startDistance) / distanceToPush * 100);
        if (newPercentage != percentage && !stopCalculating)
        {
            percentage = newPercentage;
            text.text = percentage.ToString() + "%";
        }
        if (Mathf.Abs(newPercentage) >= 100)
        {
            stopCalculating = true;

            Destroy(gameObject, 1);
        }
    }
}
