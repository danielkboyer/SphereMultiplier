using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class GoblinStats : Soldier
    {
        public GoblinStats(float speed, float attackSpeed, float health, float attackDamage, float sightRadius, float attackRadius) : base(speed, attackSpeed, health, attackDamage, sightRadius, attackRadius)
        {
        }

        public static GoblinStats GetGoblin(int level)
        {
            var baseSpeed = 1.45f;
            var baseAttackSpeed = 2f;
            var baseHealth = 50;
            var baseAttackDamage = 8;
            var baseSightRadius = 15;
            var baseAttackRadius = 2;


            var speed = baseSpeed + level * 0.5f;
            var attackSpeed = baseAttackSpeed + level * 0.1f;
            var health = baseHealth + level * 2;
            var attackDamage = baseAttackDamage + level * 0.5f;
            var sightRadius = baseSightRadius;
            var attackRadius = baseAttackRadius;
            return new GoblinStats(speed, attackSpeed, health, attackDamage, sightRadius, attackRadius);
        }
    }

}
