using Assets.Scripts;
using UnityEngine;
#nullable enable
public class Goblin : Attackable
{
    private GoblinStats? goblinStats;
    public override bool IsEnemy => true;
    public override bool CanAttack => true;
    public override bool IsBuilding => false;
    protected override float Health
    {
        get => goblinStats?.Health ?? 0;
        set
        {
            if (goblinStats != null)
            {
                goblinStats.Health = value;
            }
        }
    }

    public override float Speed
    {
        get => goblinStats?.Speed ?? 0;

    }

    public override float AttackSpeed
    {
        get => goblinStats?.AttackSpeed ?? 0;

    }
    public override float AttackDamage
    {
        get => goblinStats?.AttackDamage ?? 0;

    }

    public override float AttackRadius
    {
        get => goblinStats?.AttackRadius ?? 0;

    }
    public override float SightRadius
    {
        get => goblinStats?.SightRadius ?? 0;

    }

    //TODO: Spawn an arrow
    public override void Attack(Attackable enemy)
    {
        enemy.TakeDamage(goblinStats?.AttackDamage ?? 0);
    }

    public override void Init(int goblinLevel)
    {
        goblinStats = GoblinStats.GetGoblin(goblinLevel);
        ParentInit();
    }
    private void FixedUpdate()
    {
        AttackableUpdate();
    }

}
