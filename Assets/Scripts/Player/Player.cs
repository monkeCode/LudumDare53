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
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
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

        
    }
}
