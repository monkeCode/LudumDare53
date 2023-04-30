using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int health = 100;
        [SerializeField] private int lvl = 1;
        [SerializeField] private int currentExp;
        [SerializeField] private int _atkCount;
        [SerializeField] private float atkCooldownModifier = 1;
        [SerializeField] private float regenDelayInSeconds = 1;
        [SerializeField] private int regenValue = 1;
        public float AtkCooldownModifier => atkCooldownModifier;

        public int AtkCount => _atkCount;
        
        public int ExpToNextLvl => lvl * 300;
        public static Player Instance { get; private set; }
        public UnityEvent onDeath = new();

        [SerializeField]private int money;  

        public int Money
        {
            get => money;
            set
            {
                money = value;
                MoneyChanged?.Invoke(money);
            }
        }

        [SerializeField] private ArrowToDestination pointer;
        private Dictionary<Delivery, ArrowToDestination> _deliveries;

        public event Action<int, int> HpChanged;
        public event Action<int, int> HpChangedFromTo; 
        public event Action<int, int> ExpChanged;

        public event Action<int> MoneyChanged;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            _deliveries = new Dictionary<Delivery, ArrowToDestination>();
            StartCoroutine(RegenerateHp());
        }
        
        public void TakeDamage(int damage)
        {
            HpChangedFromTo?.Invoke(health, health-damage);
            health -= damage;

            if (health < 0)
            {
                health = 0;
                Kill();
            }
            HpChanged?.Invoke(health, maxHealth);
        }

        public void Heal(int healing)
        {
            health += healing;

            if (health > 100)
                health = 100;
            HpChanged?.Invoke(health, maxHealth);
        }

        public void Kill()
        {
            onDeath.Invoke();
        }

        private IEnumerator RegenerateHp()
        {
            while (true)
            {
                health = Math.Min(maxHealth, health+regenValue);
                yield return new WaitForSeconds(regenDelayInSeconds);
            }
        }

        public void StartDelivery(Delivery delivery)
        {
            var newPointer = Instantiate(pointer);
            newPointer.target = delivery.Destination.transform.position;
            _deliveries.Add(delivery, newPointer);
            UiManager.Instance.ShowDebuffIcons();
            delivery.Debuff.Action.Invoke(true);
        }

        public Delivery[] CompleteDelivery(DeliveryPoint destination)
        {
            var deliveriesToComplete = _deliveries
                .Where(x => x.Key.Destination == destination)
                .Select(x =>
                {
                    Destroy(x.Value.gameObject);
                    x.Key.Debuff.Action.Invoke(false);
                    return x.Key;
                })
                .ToArray();
            _deliveries = _deliveries
                .Where(x => x.Key.Destination != destination)
                .ToDictionary(x => x.Key, x => x.Value);
            return deliveriesToComplete;
        }

        public Debuff[] GetDebuffsFromDeliveries()
        {
            return _deliveries.Select(x => x.Key.Debuff).ToArray();
        }

        public void AddExperience(int exp)
        {
            currentExp += exp;
            if (currentExp > ExpToNextLvl)
            {
                currentExp -= ExpToNextLvl;
                AddNewLvl();
            }
            ExpChanged?.Invoke(currentExp, ExpToNextLvl);
        }

        private void AddNewLvl()
        {
            maxHealth += 20;
            //TODO: подумать че должно при уровне бафаться
        }

        public void DropDelivery()
        {
            foreach (var del in _deliveries)
            {
                      del.Key.Debuff.Action.Invoke(false);
                      Destroy(del.Value.gameObject);  
            }
            _deliveries.Clear();
            UiManager.Instance.ShowDebuffIcons();
        }

        public void HealthDebuff(bool setActive)
        {
            if (setActive)
            {
                maxHealth /= 2;
                if (health != 1)
                    health /= 2;
            }
            else
            {
                maxHealth *= 2;
                health *= 2;
            }
        }

        public void LessAttacksDebuff(bool setActive)
        {
            if (setActive)
                _atkCount -= 1;
            else
                _atkCount += 1;
        }

        public void LessAttackSpeedDebuff(bool setActive)
        {
            if (setActive)
                atkCooldownModifier *= 1.5f;
            else
                atkCooldownModifier /= 1.5f;
        }
    }
}
