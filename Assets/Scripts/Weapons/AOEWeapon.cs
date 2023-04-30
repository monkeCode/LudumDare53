using System;
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
        public override float Cooldown => 2 * Player.Player.Instance.AtkCooldownModifier;
        
        private float _time;
        [SerializeField] private AOEWeaponHitZone leftHitZone;
        [SerializeField] private AOEWeaponHitZone rightHitZone;
        private AOEWeaponHitZone _activeZone;
        private static IReadOnlyList<Entity> AllMobs => HeinzDoofenshmirtzInstantinator.Instance.GetAllEntities();
        private bool _canAtk = true;

        private void Start()
        {
            _activeZone = rightHitZone;
        }

        private IEnumerator UpdateCooldown()
        {
            yield return new WaitForSeconds(Cooldown);
            _canAtk = true;
        }

        private void Update()
        {
            if(_canAtk)
                Attack();
        }

        public override void Attack()
        {
            _canAtk = false;
            StartCoroutine(Atk());
        }

        private IEnumerator Atk()
        {
            _activeZone = Player.MovementsController.Instance.Velocity.x > 0 ? rightHitZone : leftHitZone;
            for (int i = 0; i < Player.Player.Instance.AtkCount; i++)
            {
                foreach (var mob in AllMobs.Where(mob => IsInsideZone(_activeZone, mob)).ToList())
                {
                    mob.TakeDamage(Damage);
                }
                yield return StartCoroutine(ShowHitEffect(_activeZone.hitEffect));
                ChangeSide();
            }
            StartCoroutine(UpdateCooldown());
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
            yield return new WaitForSeconds(0.2f);
            effect.SetActive(false);
        }
    }
}