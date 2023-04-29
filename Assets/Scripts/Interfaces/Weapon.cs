using UnityEngine;

namespace Interfaces
{
    public abstract class Weapon: MonoBehaviour
    {
        public abstract float Damage { get; }
        public abstract float Cooldown { get; }
        public abstract void Attack();
    }
}