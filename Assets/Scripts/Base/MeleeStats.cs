using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class MeleeStats : Soldier
    {
        public MeleeStats(float speed, float attackSpeed, float health, float attackDamage, float sightRadius, float attackRadius) : base(speed, attackSpeed, health, attackDamage, sightRadius, attackRadius)
        {
        }

        public static MeleeStats GetMelee(int level)
        {
            var baseSpeed = 1.45f;
            var baseAttackSpeed = 2f;
            var baseHealth = 50;
            var baseAttackDamage = 8;
            var baseSightRadius = float.MaxValue;
            var baseAttackRadius = 2;


            var speed = baseSpeed + level * 0.5f;
            var attackSpeed = baseAttackSpeed + level * 0.1f;
            var health = baseHealth + level * 2;
            var attackDamage = baseAttackDamage + level * 0.5f;
            var sightRadius = baseSightRadius;
            var attackRadius = baseAttackRadius;
            return new MeleeStats(speed, attackSpeed, health, attackDamage, sightRadius, attackRadius);
        }
    }

}
