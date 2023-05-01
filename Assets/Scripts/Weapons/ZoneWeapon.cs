using System;
using DG.Tweening;
using UnityEngine;

namespace Weapons
{
    public class ZoneWeapon : Weapon
    {
        [SerializeField] private float _radius;

        [SerializeField] private SpriteRenderer _effect;
        private Sequence _effectSequence;
        
        public override void Attack()
        {
            ShowEffect();
            foreach (var entity in GetEntitiesInCircle(Player.Player.Instance.transform.position, _radius))
            {
                entity.TakeDamage(Damage);
            }
            
            StartCoroutine(UpdateCooldown());
        }

        public override void LvlUp()
        {
            Lvl++;
            if (Lvl < 3)
            {
                _radius *= 1.2f;
                Damage++;

            }

            if (Lvl >= 3)
            {
                _radius *= 1.1f;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void ShowEffect()
        {
            _effect.transform.localScale = Vector3.one * (_radius * 2);
            _effectSequence?.Kill();
            
            _effectSequence = DOTween.Sequence();
            _effectSequence.Append(_effect.DOFade(0.7f, 0.2f))
                .Append(_effect.DOFade(0, 0.2f));
        }
    }
}