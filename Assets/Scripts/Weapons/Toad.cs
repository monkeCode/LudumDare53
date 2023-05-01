using System;
using System.Collections;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Toad : Weapon
    {
        [SerializeField] private Sprite _idleSprite;
        [SerializeField] private Sprite _jumpSprite;
        [SerializeField] private float _speed;
        [SerializeField] private float _atkRadius;
        [SerializeField] private float _searchRadius;
        [SerializeField] private float _height = 1;

        private Vector2 _target;
        private SpriteRenderer _renderer;
        private float _distance;
        private float Alpha => Mathf.PI/_distance;
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public override void Attack()
        {
            GetTarget();
            StartCoroutine(Atk());
        }

        private void GetTarget()
        {
            _target = SearchTarget();
            _distance = Mathf.Abs(_target.x - transform.position.x);
            
        }

        private IEnumerator Atk()
        {
            float totalX = 0;
            float totalY = 0;
            float startedY = transform.position.y;
            while (Vector2.Distance(_target, transform.position) > 0.1)
            {
                Debug.Log(_target.x);
                _renderer.sprite = _jumpSprite;
                _renderer.flipX = _target.x - transform.position.x > 0;
                var offset = _distance / _speed * Time.deltaTime * (_target.x - transform.position.x > 0 ? 1 : -1);
                var offsetY = (_target.y - startedY) / _speed * Time.deltaTime;
                totalX += offset;
                var x = transform.position.x + offset ;
                var y =  _height * Mathf.Sin(Alpha* MathF.Abs(totalX));
                totalY += offsetY;
                transform.position = new Vector2(x, y + startedY + totalY);
                yield return null;
            }

            foreach (var en in GetEntitiesInCircle(transform.position, _atkRadius))
            {
                en.TakeDamage(Damage);
                en.transform.Translate((en.transform.position - transform.position).normalized);
            }

            _renderer.sprite = _idleSprite;
            StartCoroutine(UpdateCooldown());
        }

        public override void LvlUp()
        {
            Lvl++;
            _atkRadius *= 1.1f;
            transform.localScale = new Vector3(_atkRadius * 1.7f, _atkRadius * 1.7f, 1);
            Damage = Damage * Lvl / (Lvl - 1);
            _speed *= 0.8f;
        }

        private Vector2 SearchTarget()
        {
            var entities = GetEntitiesInCircle(Player.Player.Instance.transform.position, _searchRadius);
            if (entities.Count == 0)
                return Player.Player.Instance.transform.position;
            return entities[Random.Range(0, entities.Count)].transform.position;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
            Gizmos.color =Color.red;
            Gizmos.DrawWireSphere(transform.position, _atkRadius);
        }
    }
}
