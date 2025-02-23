
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public TextMeshPro text;
    public int amountNeeded;
    public int coinsToGive = 10;

    public float timeBetweenAttacks = .3f;
    public Dictionary<int, float> ballAttackTime = new Dictionary<int, float>();

    void Start()
    {
        if (text != null)
            text.text = amountNeeded.ToString();
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
                var gameData = GameStorage.GetInstance().GetGameData();
                gameData.Coins += coinsToGive;
                GameStorage.GetInstance().SetGameData(gameData);
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
