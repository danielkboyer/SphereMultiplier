using Assets.Scripts;
using UnityEngine;
#nullable enable
public class EnemyCamp : Attackable
{
    private EnemyCampStats? enemyCampStats;
    public override bool IsEnemy => true;
    public override bool CanAttack => false;
    public override bool IsBuilding => true;
    protected override float Health
    {
        get => enemyCampStats?.Health ?? 0;
        set
        {
            if (enemyCampStats != null)
            {
                enemyCampStats.Health = value;
            }
        }
    }

    public override float Speed
    {
        get => enemyCampStats?.Speed ?? 0;

    }

    public override float AttackSpeed
    {
        get => enemyCampStats?.AttackSpeed ?? 0;

    }
    public override float AttackDamage
    {
        get => enemyCampStats?.AttackDamage ?? 0;

    }

    public override float AttackRadius
    {
        get => enemyCampStats?.AttackRadius ?? 0;

    }
    public override float SightRadius
    {
        get => enemyCampStats?.SightRadius ?? 0;

    }

    //TODO: Spawn an arrow
    public override void Attack(Attackable enemy)
    {
        enemy.TakeDamage(enemyCampStats?.AttackDamage ?? 0);
    }

    public override void Init(int enemyCamp)
    {
        enemyCampStats = EnemyCampStats.GetEnemyCamp(enemyCamp);
        ParentInit();
    }
    private void FixedUpdate()
    {
        AttackableUpdate();
    }

}
