using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public enum SoldierType
    {
        Melee,
        Archer,
        Goblin,
    }

    
    public class Soldier
    {

      
        public float Speed { get; set; }
        public float AttackSpeed { get; set; }
        public float Health { get; set; }
        public float AttackDamage { get; set; }

        public float SightRadius { get; set; }
        public float AttackRadius { get; set; }

        public Soldier(float speed, float attackSpeed, float health, float attackDamage, float sightRadius, float attackRadius)
        {
            Speed = speed;
            AttackSpeed = attackSpeed;
            Health = health;
            AttackDamage = attackDamage;
            SightRadius = sightRadius;
            AttackRadius = attackRadius;
        }

       

    }

  
}
