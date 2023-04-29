using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class AOEWeapon: Weapon
    {
        public override int Damage => 5;
        public override float Cooldown => 2;

        private HashSet<IDamageable> mobsInArea;

        public override void Attack()
        {
            foreach (var mob in mobsInArea)
            {
                mob.TakeDamage(Damage);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                mobsInArea.Add(damageable);
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                mobsInArea.Remove(damageable);
            }
        }
    }
}