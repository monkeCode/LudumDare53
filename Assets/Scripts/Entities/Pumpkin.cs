using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class Pumpkin : Entity
    {
        [SerializeField] private float _lifeTime;
        public override void SpawnStart()
        {
            base.SpawnStart();
            StartCoroutine(Alive());
        }

        private IEnumerator Alive()
        {
            yield return new WaitForSeconds(_lifeTime);
            HeinzDoofenshmirtzInstantinator.Destroy(this);
        }
    }
}
