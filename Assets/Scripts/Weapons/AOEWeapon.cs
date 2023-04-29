using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace Weapons
{
    public class AOEWeapon: Weapon
    {
        public override int Damage => 5;
        public override float Cooldown => 2;
        
        private float _time;
        [SerializeField] private AOEWeaponHitZone leftHitZone;
        [SerializeField] private AOEWeaponHitZone rightHitZone;
        private AOEWeaponHitZone _activeZone;
        private IReadOnlyList<Entity> _allMobs;
        [SerializeField]private int allMobsCount;

        private void Start()
        {
            _activeZone = rightHitZone;
            _allMobs = HeinzDoofenshmirtzInstantinator.Instance.GetAllEntities();
        }

        private void Update()
        {
            allMobsCount = _allMobs.Count;
            UpdateCooldown();   
        }

        private void UpdateCooldown()
        {
            _time += Time.deltaTime;
            if (_time >= Cooldown)
            {
                Attack();
                _time = 0;
            }
        }

        public override void Attack()
        {
            foreach (var mob in _allMobs.Where(mob => IsInsideZone(_activeZone, mob)).ToList())
            {
                mob.TakeDamage(Damage);
            }
            StartCoroutine(ShowHitEffect(_activeZone.hitEffect));
            ChangeSide();
        }

        private static bool IsInsideZone(AOEWeaponHitZone zone, Entity mob)
        {
            var position = mob.transform.position;
            var mobX = position.x;
            var mobY = position.y;
            return mobX >= zone.topLeftX &&
                   mobX <= zone.bottomRightX &&
                   mobY >= zone.bottomRightY &&
                   mobY <= zone.topLeftY;
        }

        private void ChangeSide()
        {
            _activeZone = _activeZone == leftHitZone ? rightHitZone : leftHitZone;
        }

        private IEnumerator ShowHitEffect(GameObject effect)
        {
            effect.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            effect.SetActive(false);
        }
    }
}