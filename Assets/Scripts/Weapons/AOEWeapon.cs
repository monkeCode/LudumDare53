using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class AOEWeapon: Weapon
    {
        public override int Damage => 5;
        public override float Cooldown => 2;
        public int mobsInAreaCount;

        public readonly List<IDamageable> MobsInArea = new List<IDamageable>(100);
        private float time;
        [SerializeField] private AOEWeaponHit leftHit;
        [SerializeField] private AOEWeaponHit rightHit;

        private void Update()
        {
            mobsInAreaCount = MobsInArea.Count;
            UpdateCooldown();   
        }

        private void UpdateCooldown()
        {
            time += Time.deltaTime;
            if (time >= Cooldown)
            {
                Attack();
                time = 0;
            }
        }

        public override void Attack()
        {
            foreach (var mob in MobsInArea.ToList())
            {
                mob?.TakeDamage(Damage);
            }

            MobsInArea.Clear();
            if (leftHit.gameObject.activeSelf)
            {
                leftHit.ShowHit();
            }

            if (rightHit.gameObject.activeSelf)
            {
                rightHit.ShowHit();
            }
        }

        public void ChangeSide()
        {
            
            if (leftHit.gameObject.activeSelf)
            {
                leftHit.gameObject.SetActive(false);
                rightHit.gameObject.SetActive(true);
            }
            else
            {
                leftHit.gameObject.SetActive(true);
                rightHit.gameObject.SetActive(false);}
        }

        

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("OnCollisionEnter2D");
            var damageable = col.gameObject.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                MobsInArea.Add(damageable);
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            Debug.Log("OnCollisionExit2D");
            var damageable = col.gameObject.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                MobsInArea.Remove(damageable);
            }
        }
    }
}