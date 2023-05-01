using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class ParticleWeaponManager:Weapon
    {
        [SerializeField] private Weapon _prefab;
        private List<Weapon> _activeWeapons = new();

        public  override void Attack()
        {
            
        }

        protected override void Update()
        {
            if (_activeWeapons.Count < Player.Player.Instance.AtkCount)
            {
                _activeWeapons.Add(Instantiate(_prefab, Player.Player.Instance.transform.position, Quaternion.identity));
                for(int i = 1; i < _activeWeapons[0].Lvl; i++)
                    _activeWeapons[^1].LvlUp();
                return;
            }
            if (_activeWeapons.Count <= Player.Player.Instance.AtkCount) return;
            var weapon = _activeWeapons[0];
            _activeWeapons.Remove(weapon);
            Destroy(weapon.gameObject);
        }

        public override void LvlUp()
        {
            foreach (var weapon in _activeWeapons)
            {
                weapon.LvlUp();
            }
        }
    }
}