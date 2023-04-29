using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        public static Player Instance { get; private set; }
        public UnityEvent onDeath = new();
        
        [SerializeField] private float health = 100;
        public float money;
        [SerializeField] private ArrowToDestination pointer;
        [SerializeField] private Dictionary<Delivery, ArrowToDestination> deliveries;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            deliveries = new Dictionary<Delivery, ArrowToDestination>();
        }
        
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            onDeath.Invoke();
            Destroy(this);
        }

        public void StartDelivery(Delivery delivery)
        {
            var newPointer = Instantiate(pointer);
            newPointer.target = delivery.Destination.transform;
            deliveries.Add(delivery, newPointer);
        }

        public Delivery[] CompleteDelivery(DeliveryPoint destination)
        {
            var deliveriesToComplete = deliveries
                .Where(x => x.Key.Destination == destination)
                .Select(x =>
                {
                    Destroy(x.Value.gameObject);
                    return x.Key;
                })
                .ToArray();
            deliveries = deliveries
                .Where(x => x.Key.Destination != destination)
                .ToDictionary(x => x.Key, x => x.Value);
            return deliveriesToComplete;
        }

    }
}
