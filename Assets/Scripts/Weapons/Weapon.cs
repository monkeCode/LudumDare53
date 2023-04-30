using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon: MonoBehaviour
    {
        [SerializeField] private float _coolDown;
        [SerializeField] private int _damage;
        public virtual int Damage
        {
            get => _damage;
            protected set => _damage = value;
        }
        public virtual float Cooldown => _coolDown * Player.Player.Instance.AtkCooldownModifier;
        public abstract void Attack();

        private bool _canAtk = true;
        public bool CanAtk => _canAtk;

        public int Lvl { get; protected set; } = 1;
        
        
        private void MakeAttack()
        {
            if (CanAtk)
            {
                _canAtk = false;
                Attack();
            }
        }

        protected virtual void Update()
        {
            MakeAttack();
        }

        protected IEnumerator UpdateCooldown()
        {
            yield return new WaitForSeconds(Cooldown * Player.Player.Instance.AtkCooldownModifier);
            _canAtk = true;
        }
        
        protected List<Entity> GetEntitiesInCircle(Vector2 center, float radius)
        {
            var list = (
                from en in HeinzDoofenshmirtzInstantinator.Instance.GetAllEntities()
                let pos = (Vector2) en.transform.position
                where (pos - center).magnitude <= radius
                select en).ToList();
            return list;
        }

        public abstract void LvlUp();
    }
}