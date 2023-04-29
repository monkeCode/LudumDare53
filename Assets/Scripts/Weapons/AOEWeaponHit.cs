using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class AOEWeaponHit: MonoBehaviour
    {
        [SerializeField] private AOEWeapon _weapon;
        [SerializeField] private GameObject weaponImage;

        public void ShowHit()
        {
            StartCoroutine(ShowHitCoroutine());
        }
        
        private IEnumerator ShowHitCoroutine()
        { 
            weaponImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            weaponImage.SetActive(false);
            _weapon.ChangeSide();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("OnCollisionEnter2D");
            var damageable = col.gameObject.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                _weapon.MobsInArea.Add(damageable);
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            Debug.Log("OnCollisionExit2D");
            var damageable = col.gameObject.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                _weapon.MobsInArea.Remove(damageable);
            }
        }
    }
}