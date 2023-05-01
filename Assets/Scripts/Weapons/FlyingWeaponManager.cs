using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class FlyingWeaponManager:MonoBehaviour
    {
        [SerializeField] private FlyingWeapon _prefab;
        private List<FlyingWeapon> _activeWeapons = new();

        private void Update()
        {
            if (_activeWeapons.Count < Player.Player.Instance.AtkCount)
            {
                _activeWeapons.Add(Instantiate(_prefab, Player.Player.Instance.transform.position, Quaternion.identity));
                return;
            }
            if (_activeWeapons.Count <= Player.Player.Instance.AtkCount) return;
            var weapon = _activeWeapons[0];
            _activeWeapons.Remove(weapon);
            Destroy(weapon);
        }
    }
}