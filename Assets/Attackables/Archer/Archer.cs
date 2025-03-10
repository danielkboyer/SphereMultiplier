using Assets.Scripts;
using UnityEngine;
#nullable enable
public class Archer : Attackable
{
    public GameObject ArrowPrefab;
    private ArcherStats? archerStats;

    public override bool CanAttack => true;
    public override bool IsEnemy => false;

    public override bool IsBuilding => false;

    protected override float Health
    {
        get => archerStats?.Health ?? 0;
        set
        {
            if (archerStats != null)
            {
                archerStats.Health = value;
            }
        }
    }

    public override float Speed
    {
        get => archerStats?.Speed ?? 0;

    }

    public override float AttackSpeed
    {
        get => archerStats?.AttackSpeed ?? 0;

    }
    public override float AttackDamage
    {
        get => archerStats?.AttackDamage ?? 0;

    }

    public override float AttackRadius
    {
        get => archerStats?.AttackRadius ?? 0;

    }
    public override float SightRadius
    {
        get => archerStats?.SightRadius ?? 0;

    }

    //TODO: Spawn an arrow
    public override void Attack(Attackable enemy)
    {
        if (ArrowPrefab == null)
        {
            throw new System.Exception("ArrowPrefab is null");
        }
        var arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<Arrow>();
        arrow.Init(this.archerStats?.AttackDamage ?? 0, enemy);
    }

    public override void Init(int archerLevel)
    {
        archerStats = ArcherStats.GetArcher(archerLevel);
        ParentInit();
    }
    private void FixedUpdate()
    {
        AttackableUpdate();
    }

}
