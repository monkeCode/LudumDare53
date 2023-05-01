using System;
using System.Collections;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class StalaktitWeapon : Weapon
    {
        [SerializeField] private float _searchRadius;
        [SerializeField] private float speed;
        [SerializeField] private Transform _prefab;
        public override void Attack()
        {
            var entis = GetEntitiesInCircle(Player.Player.Instance.transform.position, _searchRadius);
            if (entis.Count == 0)
            {
                StartCoroutine(UpdateCooldown());
                return;
            }
            for (int i = 0; i < Player.Player.Instance.AtkCount * 3; i++)
            {
                var en = entis[Random.Range(0, entis.Count)];
                var position = new Vector2(en.transform.position.x, en.transform.position.y+15);
                StartCoroutine(AtkSingleTarget(Instantiate(_prefab, position, Quaternion.identity),en));
            }
            StartCoroutine(UpdateCooldown());
        }

        IEnumerator AtkSingleTarget(Transform stalaktit,Entity en)
        {
            while (Math.Abs(stalaktit.position.y - en.transform.position.y) > 0.2f)
            {
                stalaktit.position = new Vector2(en.transform.position.x,
                    Mathf.Lerp(stalaktit.position.y, en.transform.position.y, speed*Time.deltaTime));
                yield return null;
            }
            Destroy(stalaktit.gameObject);
            en.TakeDamage(Damage);
        }
        
        public override void LvlUp()
        {
            Lvl++;
            Damage = Damage * Lvl / (Lvl - 1);
        }
    }
}
