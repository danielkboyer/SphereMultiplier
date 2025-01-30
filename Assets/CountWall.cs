using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountWall : MonoBehaviour
{
    public TextMeshPro text;
    public int amountNeeded;

    public float timeBetweenAttacks = .3f;
    public Dictionary<int, float> ballAttackTime = new Dictionary<int, float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (text != null)
            text.text = amountNeeded.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            if(!ballAttackTime.ContainsKey(ball.GetInstanceID()))
            {
                ballAttackTime.Add(ball.GetInstanceID(), Time.time);
            }
            else
            {
                var lastAttackTime = ballAttackTime[ball.GetInstanceID()];

                if(Time.time - lastAttackTime < timeBetweenAttacks)
                {
                    return;
                }


                ballAttackTime[ball.GetInstanceID()] = Time.time;
            }

            var health = ball.GetComponent<Health>();

            health.GetAttacked(10);

            amountNeeded--;
            if(text != null)
                text.text = amountNeeded.ToString();
            if (amountNeeded == 0)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
