using System.Collections;
using Interfaces;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int hp;
        [SerializeField] protected float speed;
        [SerializeField] protected int damage;
        [SerializeField] protected float damageDelay;
        [SerializeField] protected float atkDistance;
        [SerializeField] private MonsterType _type;
        
        public MonsterType Type => _type;

        private bool _canDamage = true;
    
        protected virtual void Move(Vector2 direction)
        {
            transform.Translate(direction * (speed * Time.deltaTime));
        }
    
        protected virtual void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(damage);
        }

        protected virtual void Update()
        {
            LifeCycle();
        }

        protected void LifeCycle()
        {
            var toPlayerDist = Player.Player.Instance.transform.position - transform.position;
            Move(toPlayerDist.normalized);
            if (!_canDamage || !(toPlayerDist.magnitude <= atkDistance)) return;
            Attack(Player.Player.Instance);
            StartCoroutine(DamageDelay());
        }

        public void TakeDamage(int damage)
        {
            if(hp <= 0) return;
            hp -= damage;
            UiManager.Instance.ShowDamageText(damage,transform.position);
            if (hp <= 0)
            {
                Die();
            }
        }

    
    
        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        public void Kill()
        {
            Die();
        }

        private IEnumerator DamageDelay()
        {
            _canDamage = false;
            yield return new WaitForSeconds(damageDelay);
            _canDamage = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, atkDistance);
        }

        public void BoostStats(float hpCoef, float damageCoef)
        {
            hp = (int)(hp*hpCoef);
            damage = (int)(damageCoef*damage);
        }
    }
}
