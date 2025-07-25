using UnityEngine;
using UnityEngine.UI;

namespace Knight.Adventure
{
    public class KnightController_Keyboard : BasePlayer
    {
        [SerializeField] 
        private Image hpBar;
        
        private readonly Vector2 _crouchOffset = new(0.03912544f, 0.5389824f); 
        private readonly Vector2 _crouchSize = new(0.6767006f, 0.9599333f);
        
        private readonly Vector2 _standOffset = new(0.03912544f, 0.7860222f);
        private readonly Vector2 _standSize = new(0.6767006f, 1.400105f);
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider;

        private Vector3 _inputDir;
        
        private bool _isGround;
        private bool _isAttack;
        private bool _isCombo;
        private bool _isLadder;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
        }

        private void Update()       // 일반적인 작업
        {
            InputKeyboard();
            Jump();
            Attack();
        }

        private void FixedUpdate()  // 물리적인 작업
        {
            Move();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _animator.SetBool("isGround", true);
                _isGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _animator.SetBool("isGround", false);
                _isGround = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Monster"))
            {
                // TODO 몬스터 데미지
                other.gameObject
                    .GetComponent<MonsterCore>()
                    .TakeDamage(Player.GetInstance().GetDamage());
                other.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                
                // TODO 방향 맞추고 튕기기(몬스터는 무조건 튕기기)
                var scaleX = transform.localScale.x * -1;
                other.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
                return;
            }

            if (other.CompareTag("Ladder"))
            {
                _isLadder = true;
                _rigidbody.gravityScale = 0f;
                _rigidbody.linearVelocity = Vector2.zero;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Ladder"))
            {
                _isLadder = false;
                _rigidbody.gravityScale = 2f;
                _rigidbody.linearVelocity = Vector2.zero;
            }
        }

        void InputKeyboard()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");     // 좌, 우
            var vertical = Input.GetAxisRaw("Vertical");         // 상, 하

            _inputDir = new Vector3(horizontal, vertical, 0);
            
            _animator.SetFloat("PositionX", _inputDir.x);
            _animator.SetFloat("PositionY", _inputDir.y);

            if (vertical < 0)
            {
                _collider.size = _crouchSize;
                _collider.offset = _crouchOffset;
            }
            else
            {
                _collider.size = _standSize;
                _collider.offset = _standOffset;
            }
        }

        void Move()
        {
            if (_isLadder)
            {
                _rigidbody.position = new Vector2(13.5f, _rigidbody.position.y);
                _rigidbody.linearVelocityY = _inputDir.y * Player.GetInstance().GetSpeed();
            }
            
            if (_inputDir.x != 0)
            {
                _isLadder = false;
                var scaleX = _inputDir.x > 0 ? 1f : -1f;
                transform.localScale = new Vector3(scaleX, 1, 1);
                
                _rigidbody.linearVelocityX = _inputDir.x * Player.GetInstance().GetSpeed();
            }
        }

        void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            {
                _animator.SetTrigger("Jump");
                _rigidbody.AddForceY(Player.GetInstance().GetJumpPower(), ForceMode2D.Impulse);
            }
        }

        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!_isAttack)
                {
                    _isAttack = true;
                    Player.GetInstance().SetDamage(3f);
                    _animator.SetTrigger("Attack");
                    return;
                }
                
                _isCombo = true;
            }
        }

        // 공격 애니메이션에 이벤트로 연결
        public void WaitCombo()
        {
            if (_isCombo)
            {
                Player.GetInstance().SetDamage(5f);
                _animator.SetBool("isCombo", true);
                return;
            }
        
            _isAttack = false;
            _animator.SetBool("isCombo", false);
        }
        
        // 콤보 애니메이션에 이벤트로 연결
        public void EndCombo()
        {
            _isAttack = false;
            _isCombo = false;
            _animator.SetBool("isCombo", false);
        }

        public void TakeDamage(float damage)
        {
            Player.GetInstance().TakeDamage(damage);
            hpBar.fillAmount = Player.GetInstance().GetHpRatio();
            
            if (Player.GetInstance().GetCurrentHp() <= 0f)
                Death();
        }

        void Death()
        {
            _animator.SetTrigger("Death");
            
            // TODO 로비로 가는 로직을 만들어야 함
        }
    }
}