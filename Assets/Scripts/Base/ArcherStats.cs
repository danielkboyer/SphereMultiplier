using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class ArcherStats : Soldier
    {
        public ArcherStats(float speed, float attackSpeed, float health, float attackDamage, float sightRadius, float attackRadius) : base(speed, attackSpeed, health, attackDamage, sightRadius, attackRadius)
        {
        }

        public static ArcherStats GetArcher(int level)
        {
            var baseSpeed = 1.28f;
            var baseAttackSpeed = 2.5f;
            var baseHealth = 20;
            var baseAttackDamage = 5;
            var baseSightRadius = float.MaxValue;
            var baseAttackRadius = 10;


            var speed = baseSpeed + level * 0.5f;
            var attackSpeed = baseAttackSpeed + level * 0.1f;
            var health = baseHealth + level * 2;
            var attackDamage = baseAttackDamage + level * 0.5f;
            var sightRadius = baseSightRadius;
            var attackRadius = baseAttackRadius + level * 0.5f;
            return new ArcherStats(speed,attackSpeed,health,attackDamage,sightRadius,attackRadius);
        }
    }

}
