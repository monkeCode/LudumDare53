using System.Collections;
using Interfaces;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int maxHp;
        [SerializeField] protected float speed;
        [SerializeField] protected int damage;
        [SerializeField] protected float damageDelay;
        [SerializeField] protected float atkDistance;
        [SerializeField] private MonsterType _type;
        [SerializeField] private xpShard XPShard;
        [SerializeField] private float _hpCoef;
        [SerializeField] private float _dmageCoef;
        private const float ToPlayerDistance = 100;
        public MonsterType Type => _type;

        private bool _canDamage = true;
        private int hp;
        private int dmg;
    
        protected virtual void Move(Vector2 direction)
        {
            transform.Translate(direction * (speed * Time.deltaTime));
        }
    
        protected virtual void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(dmg);
        }

        protected virtual void Update()
        {
            LifeCycle();
            if(Vector2.Distance(Player.Player.Instance.transform.position, transform.position) > ToPlayerDistance)
                HeinzDoofenshmirtzInstantinator.Destroy(this);
        }

        protected void LifeCycle()
        {
            var toPlayerDist = Player.Player.Instance.transform.position - transform.position;
            if(toPlayerDist.magnitude > atkDistance)
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
            // Player.Player.Instance.AddExperience(10);
            Instantiate(XPShard, transform.position, Quaternion.identity).InitRandomShard();
            HeinzDoofenshmirtzInstantinator.Destroy(this);
        }

        public void ResetHp()
        {
            hp = maxHp;
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

        public void BoostStats(float time)
        {
            hp = (int)(maxHp*_hpCoef * time);
            dmg = (int)(_dmageCoef*damage * time);
        }

        public virtual void SpawnStart()
        {
            BoostStats(WaveGenerator.Timer/60+1);
        }
    }
}
