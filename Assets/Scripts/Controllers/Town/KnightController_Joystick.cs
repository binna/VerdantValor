using UnityEngine;
using UnityEngine.UI;

namespace Knight.Town
{
    public class KnightController_Joystick : BasePlayer
    {
        [SerializeField] 
        private Image hpBar;

        private Animator _animator;
        private Rigidbody2D _rigidbody;

        private Vector3 _inputDir;

        private float _timer;
        
        public void InputJoystick(float x, float y)
        {
            _inputDir = new Vector3(x, y, 0).normalized;

            _animator.SetFloat("JoystickX", _inputDir.x);
            _animator.SetFloat("JoystickY", _inputDir.y);
        }

        #region 이벤트 함수
        private void Start()
        {
            _isBlocked = false;
            
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            
            hpBar.fillAmount = Player.GetInstance().GetHpRatio();
        }

        private void FixedUpdate()
        {
            if (!Player.GetInstance().IsFullHp())
            {
                _timer += Time.deltaTime;
                if (_timer >= 2f)
                {
                    _timer = 0;
                    Player.GetInstance().RecoveryHp();
                    hpBar.fillAmount = Player.GetInstance().GetHpRatio();
                }
            }

            Move();
        }
        #endregion

        private void Move()
        {
            if (_isBlocked)
                return;
            
            if (_inputDir.x != 0)
            {
                var scaleX = _inputDir.x > 0 ? 1f : -1f;
                transform.localScale = new Vector3(scaleX, 1, 1);
                _rigidbody.linearVelocity = _inputDir * Player.GetInstance().GetSpeed();
                
                Debug.Log($"{Player.GetInstance().GetSpeed()}");
            }
        }
    }
}