using System;
using System.Linq;
using Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class FlyingWeapon : Weapon
    {
        
        [SerializeField] private float _speedCoef;
        [SerializeField] private float _searchRadius;
        [SerializeField] private float _atkRadius;
        
        
        private Entity _target;
        
        


        public override void Attack()
        {
            foreach (var en in GetEntitiesInCircle(transform.position, _atkRadius))
            {
                en.TakeDamage(Damage);
            }

            StartCoroutine(UpdateCooldown());
        }

        public override void LvlUp()
        {
            Lvl++;
            Damage = Damage * Lvl / (Lvl - 1);
            _speedCoef *= 1.1f;
        }

        protected override void Update()
        {
            base.Update();
            
            if (_target is null || !_target.enabled)
            {
                _target = FindingTarget();
                return;
            }
            var distance =Vector2.Distance(Player.Player.Instance.transform.position, _target.transform.position);
            var distance2 = Vector2.Distance(transform.position, _target.transform.position);
            if (distance > _searchRadius || distance2 < _atkRadius)
            {
                _target = null;
                return;
            }
            
            var dir = (_target.transform.position - transform.position).normalized;
            transform.transform.Translate(dir * (_speedCoef * Time.deltaTime));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(Player.Player.Instance.transform.position, _searchRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _atkRadius);
        }

        private Entity FindingTarget()
        {
            var entities =  GetEntitiesInCircle(Player.Player.Instance.transform.position, _searchRadius)
                .OrderBy(it => Vector2.Distance(it.transform.position, transform.position)).ToList();
            if (entities.Count == 0) return null;
            return entities[Random.Range(0, (int) (entities.Count * 0.4f))];
        }
    }
}
