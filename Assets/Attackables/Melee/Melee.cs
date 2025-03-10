using Assets.Scripts;
using UnityEngine;
#nullable enable
public class Melee : Attackable
{
    private MeleeStats? meleeStats;
    public override bool IsEnemy => false;
    public override bool CanAttack => true;
    public override bool IsBuilding => false;
    protected override float Health
    {
        get => meleeStats?.Health ?? 0;
        set
        {
            if (meleeStats != null)
            {
                meleeStats.Health = value;
            }
        }
    }

    public override float Speed
    {
        get => meleeStats?.Speed ?? 0;

    }

    public override float AttackSpeed
    {
        get => meleeStats?.AttackSpeed ?? 0;

    }
    public override float AttackDamage
    {
        get => meleeStats?.AttackDamage ?? 0;

    }

    public override float AttackRadius
    {
        get => meleeStats?.AttackRadius ?? 0;

    }
    public override float SightRadius
    {
        get => meleeStats?.SightRadius ?? 0;

    }

    public override void Attack(Attackable enemy)
    {
       enemy.TakeDamage(meleeStats?.AttackDamage ?? 0);
    }

    public override void Init(int meleeLevel)
    {
        meleeStats = MeleeStats.GetMelee(meleeLevel);
        ParentInit();
    }
    private void FixedUpdate()
    {
        AttackableUpdate();
    }

}
