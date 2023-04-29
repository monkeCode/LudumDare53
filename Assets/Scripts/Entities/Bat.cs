using System;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bat : Entity
    {
        private Rigidbody2D _rb;
        [SerializeField] private float _maxVelocity;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        protected override void Move(Vector2 direction)
        {
            _rb.velocity += direction * (speed * Time.deltaTime);
            if (_rb.velocity.magnitude > _maxVelocity)
                _rb.velocity = _rb.velocity.normalized * _maxVelocity;
        }
    }
}
