using UnityEngine;

namespace Knight
{
    public class KnightController_Joystick : MonoBehaviour
    {
        [SerializeField] 
        private float moveSpeed = 3f;

        private Animator _animator;
        private Rigidbody2D _rigidbody;

        private Vector3 _inputDir;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void InputJoystick(float x, float y)
        {
            _inputDir = new Vector3(x, y, 0).normalized;

            _animator.SetFloat("JoystickX", _inputDir.x);
            _animator.SetFloat("JoystickY", _inputDir.y);
        }

        void Move()
        {
            if (_inputDir.x != 0)
            {
                var scaleX = _inputDir.x > 0 ? 1f : -1f;
                transform.localScale = new Vector3(scaleX, 1, 1);
                _rigidbody.linearVelocity = _inputDir * moveSpeed;
            }
        }
    }
}