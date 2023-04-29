using UnityEngine;

namespace Player
{
    public class MovementsController: MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private Rigidbody2D _rigidbody2D;
        private Controls _controls;
        private Vector2 _moveInput;
        
        private void Awake()
        {
            _controls = new Controls();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if (_rigidbody2D is null)
            {
                Debug.LogError("Player Rigidbody2D is null");
            }
        }
        
        private void FixedUpdate()
        {
            UpdateSpeed();
        }
        
        private void UpdateSpeed()
        {
            _moveInput = _controls.Player.Movement.ReadValue<Vector2>();
            _rigidbody2D.velocity = _moveInput * speed;
        }
        
        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }
    }
}