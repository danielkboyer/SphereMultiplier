using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public enum PlayerId
{
    SmallEnemy,
    BigEnemy,
    SmallBall,
    BigBall,
}

public class Health : MonoBehaviour
{



    public PlayerId Id;
    private int Health_ = 100;
    private int _startingHealth;
    public int Attack = 10;

    private Coroutine currentCoroutine;
    private float LastAttack = 0;
    public float AttackPerSecond = .1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Material material;


    public Material flashMaterial;

    private readonly Queue<IEnumerator> _queue = new Queue<IEnumerator>();
    void Awake()
    {
        var gameData = GameStorage.GetInstance().GetGameData();
        switch (Id)
        {
            case PlayerId.SmallEnemy:
                Health_ = gameData.SmallEnemyHealth;
                Attack = gameData.SmallEnemyAttack;
                break;
            case PlayerId.BigEnemy:
                Health_ = gameData.BigEnemyHealth;
                Attack = gameData.BigEnemyAttack;
                break;
            case PlayerId.SmallBall:
                Health_ = gameData.SmallBallHealth;
                Attack = gameData.SmallBallAttack;
                break;
            case PlayerId.BigBall:
                Health_ = gameData.BigBallHealth;
                Attack = gameData.BigBallAttack;
                break;
        }
        _startingHealth = Health_;

        this.gameObject.GetComponent<Renderer>().material = material;
        StartCoroutine(FlashQueue());
    }


    public IEnumerator FlashQueue()
    {
        while (true)
        {
            if(_queue.Count == 0)
            {
                yield return new WaitForSeconds(.01f);
            }
            else
            {
                yield return StartCoroutine(_queue.Dequeue());
                _queue.Clear();
            }
           
        }

    }

    public void StartFlash()
    {
        _queue.Enqueue(Flash());
    }

    public IEnumerator Flash()
    {
        this.gameObject.GetComponent<Renderer>().material = flashMaterial;
        yield return new WaitForSeconds(.05f);
        this.gameObject.GetComponent<Renderer>().material = material;
    }


    private bool CanAttack()
    {
        return LastAttack <= 0;
    }

    public void GetAttacked(int dmg)
    {
        Health_ -= dmg;
        StartFlash();
    }

    public void AttackEnemy(Health opp)
    {
        if (CanAttack())
        {
            opp.Health_ -= Attack;
        
            opp.StartFlash();
            
            LastAttack = AttackPerSecond;
        }

    }


    // Update is called once per frame
    void Update()
    {
        LastAttack -= Time.deltaTime;

        if (Health_ <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
