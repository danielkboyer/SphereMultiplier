using System.Linq;
using UnityEngine;
using UnityEngine.AI;
#nullable enable
public abstract class Attackable : MonoBehaviour
{

    private HealthBar _healthBar;
    public bool IsInitialized { get; set; } = false;
    abstract public bool CanAttack { get; }

    abstract public bool IsBuilding { get; }
    abstract public bool IsEnemy { get; }
    abstract protected float Health { get; set; }
    abstract public float Speed { get; }

    abstract public float AttackSpeed { get; }

    abstract public float AttackDamage { get; }

    abstract public float AttackRadius { get; }

    /**
     * The distance at which the attackable can see other attackables
     */
    abstract public float SightRadius { get; }

    abstract public void Attack(Attackable enemy);

    abstract public void Init(int level);
    public bool IsDead => Health <= 0;
    //The current enemy that the attackable is attacking
    private Attackable? currentEnemy;


    private float _attackSpeed;


    private void Start()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.SetMaxHealth(Health);

    }
    public void ParentInit()
    {
        IsInitialized = true;
        NavMeshAgent? agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = Speed;
        }

    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        _healthBar.SetHealth(Health);
    }
    /**
     * Call this in the FixedUpdate method of inheriting classes
     */
    public void AttackableUpdate()
    {



        if (!IsInitialized)
        {
            return;
        }

        if (IsDead)
        {
            Destroy(gameObject);
            return;
        }
        if (currentEnemy != null && currentEnemy.IsDead)
        {
            currentEnemy = null;
        }


        var enemies = FindObjectsByType<Attackable>(FindObjectsSortMode.None).Where(enemy => enemy.IsEnemy != this.IsEnemy && !enemy.IsDead && DistanceBetween(this, enemy) <= SightRadius);

        var sortedEnemies = enemies.OrderBy(enemy => DistanceBetween(this, enemy) + (enemy.CanAttack ? 0 : 1000));

        if (sortedEnemies.Any())
        {
            currentEnemy = sortedEnemies.First();
        }

        if (currentEnemy != null)
        {
            TryAttack();
            return;
        }


    }

    private float DistanceBetween(Attackable a, Attackable b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }


    void TryAttack()
    {
        if (currentEnemy == null)
        {
            throw new System.Exception("currentEnemy is null");
        }

        if (DistanceBetween(this, currentEnemy) > AttackRadius)
        {
            _attackSpeed = AttackSpeed;
            Walk();
            return;
        }

        LoadAttack();


    }

    void LoadAttack()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.destination = transform.position;
        }

        _attackSpeed -= Time.deltaTime;
        if (_attackSpeed <= 0)
        {
            Attack(currentEnemy!);
            _attackSpeed = AttackSpeed;
        }
    }


    void Walk()
    {

        if(IsBuilding)
        {
            return;
        }
        if (currentEnemy != null)
        {
            NavMeshAgent? agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.destination = currentEnemy.transform.position;
            }


            transform.LookAt(currentEnemy.transform, Vector3.up);
        }
    }
}
