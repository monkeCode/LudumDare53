using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementsController: MonoBehaviour
    {
        public static MovementsController Instance { get; private set; }
        [SerializeField] private float speed = 10;
        private Rigidbody2D _rigidbody2D;
        private Controls _controls;
        private Vector2 _moveInput;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            _controls = new Controls();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _controls.Player.DropDelivery.performed += context => Player.Instance.DropDelivery();
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

        public void InvertMovementDebuff(bool setActive)
        {
            speed *= -1;
        }

        public void SpeedDebuff(bool setActive)
        {
            if (setActive)
                speed /= 2;
            else
                speed *= 2;
        }
        private void OnEnable()
        {
            _controls?.Enable();
        }

        private void OnDisable()
        {
            _controls?.Disable();
        }

        public Vector2 Velocity => _rigidbody2D.velocity;
    }
}