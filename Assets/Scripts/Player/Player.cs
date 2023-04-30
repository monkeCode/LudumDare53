using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using JetBrains.Annotations;
using TMPro;
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

        [SerializeField] private TextMeshProUGUI moneyValue;

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
        public event Action<int, int> ExpChanged;

        public event Action<int> MoneyChanged;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            MoneyChanged = i => moneyValue.text = i.ToString();
            _deliveries = new Dictionary<Delivery, ArrowToDestination>();
        }
        
        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health < 0)
            {
                health = 0;
                Kill();
            }
            HpChanged?.Invoke(health, maxHealth);
        }

        public void Kill()
        {
            onDeath.Invoke();
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
    }
}
