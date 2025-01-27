using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SharedBall : MonoBehaviour
{
    abstract public bool isEnemy { get; }

    public Dictionary<int, SharedBall> ballsInContact = new Dictionary<int, SharedBall>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

  


    protected void AddToContacts(Collider other)
    {
        var ball = other.gameObject.GetComponent<SharedBall>();
        if (ball != null && ball.isEnemy == this.isEnemy)
        {


            if(!ballsInContact.ContainsKey(ball.GetInstanceID()))
            {
                ballsInContact.Add(ball.GetInstanceID(), ball);
            }
        }
    }

    protected void RemoveFromContacts(Collider other)
    {
        var ball = other.gameObject.GetComponent<SharedBall>();
        if (ball != null && ball.isEnemy == this.isEnemy)
        {
            if (ballsInContact.ContainsKey(ball.GetInstanceID()))
            {
                ballsInContact.Remove(ball.GetInstanceID());

            }
        }
    }

 
}
