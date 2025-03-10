using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class EnemyCampStats : Soldier
    {
        public EnemyCampStats(float speed, float attackSpeed, float health, float attackDamage, float sightRadius, float attackRadius) : base(speed, attackSpeed, health, attackDamage, sightRadius, attackRadius)
        {
        }

        public static EnemyCampStats GetEnemyCamp(int level)
        {
            var baseSpeed = 0f;
            var baseAttackSpeed = 0f;
            var baseHealth = 100;
            var baseAttackDamage = 0;
            var baseSightRadius = float.MaxValue;
            var baseAttackRadius = 0;


            var speed = baseSpeed;
            var attackSpeed = baseAttackSpeed;
            var health = baseHealth;
            var attackDamage = baseAttackDamage;
            var sightRadius = baseSightRadius;
            var attackRadius = baseAttackRadius;
            return new EnemyCampStats(speed,attackSpeed,health,attackDamage,sightRadius,attackRadius);
        }
    }

}
