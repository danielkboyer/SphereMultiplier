using System;
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


    private float LastAttack = 0;
    public float AttackPerSecond = .1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Material material;

    void Awake()
    {
        var gameData = GameStorage.GetInstance().GetGameData();
        switch(Id)
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
        material = this.gameObject.GetComponent<Renderer>().material; //notice, not shared material
    }

    //invoke this function from anywhere outside or even from inside this particular script instance
    public void Darken(float percent)
    {
        percent = Mathf.Clamp01(percent);
        material.color = new Color(material.color.r * (1 - percent), material.color.g * (1 - percent), material.color.b * (1 - percent), material.color.a);
    }

    private bool CanAttack()
    {
        return LastAttack <= 0;
    }

    public void AttackEnemy(Health opp)
    {
        if (CanAttack())
        {
            opp.Health_ -= Attack;
            var darken = (float)Attack / _startingHealth;
            opp.Darken(darken);
            LastAttack = AttackPerSecond;
        }
 
    }

   
    // Update is called once per frame
    void Update()
    {
        LastAttack -= Time.deltaTime;

        if(Health_ <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
}
