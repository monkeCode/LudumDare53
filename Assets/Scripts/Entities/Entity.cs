using System;
using System.Collections;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected int hp;
    [SerializeField] protected int speed;
    [SerializeField] protected int damage;
    [SerializeField] protected float damageDelay;
    [SerializeField] protected float atkDistance;

    private bool _canDamage = true;
    
    protected virtual void Move(Vector2 direction)
    {
        transform.Translate(direction.normalized);
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
        var toPlayerDist = Player.Instance.transform.position - transform.position;
        Move(toPlayerDist);
        if (!_canDamage || !(toPlayerDist.magnitude <= atkDistance)) return;
        Attack(Player.Instance);
        StartCoroutine(DamageDelay());
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
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
}
