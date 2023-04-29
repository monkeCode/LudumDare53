using UnityEngine;

namespace Weapons
{
    public abstract class Weapon: MonoBehaviour
    {
        public abstract int Damage { get; }
        public abstract float Cooldown { get; }
        public abstract void Attack();
    }
}