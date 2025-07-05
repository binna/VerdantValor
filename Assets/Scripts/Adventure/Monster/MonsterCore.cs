using UnityEngine;
using UnityEngine.UI;

namespace Knight.Adventure
{
    public abstract class MonsterCore : MonoBehaviour
    {
        public enum State
        {
            IDLE,
            PATROL,
            TRACE,
            ATTACK
        }
        
        [SerializeField] 
        protected Image hpBar;
        
        [SerializeField]
        private State state = State.IDLE;

        [SerializeField] 
        protected float _hp = 10f;
        
        [SerializeField] 
        protected float _speed = 3f;
        
        [SerializeField]
        public float _attackTime = 2f;

        [SerializeField] 
        protected float atkDamage = 5f;
        
        private const float HP = 10f;

        protected Animator _animator;
        protected Rigidbody2D _rigidbody;
        protected Transform _playerTransform;
        protected ItemManager _itemManager;
        
        protected float _toMonsterDistance;
        protected bool _isTrace;
        protected bool _isDead;
        
        protected abstract void Idle();
        protected abstract void Patrol();
        protected abstract void Trace();
        protected abstract void Attack();

        public void TakeDamage(float damage)
        {
            _hp -= damage;
            hpBar.fillAmount = _hp / HP;

            if (_hp <= 0)
                Death();
        }

        void Death()
        {
            _isDead = true;
            _animator.SetTrigger("Death");
            gameObject.SetActive(false);
            
            // TODO 죽었을 때, 보여질 화면 작업 필요

            int itemCount = Random.Range(0, 3);
            for(int i = 0; i < itemCount; i++)
                _itemManager.DripItem(transform.position);
        }

        protected void ChangeState(State newState)
        {
            state = newState;
        }
        
        protected void Init(float hp, float speed, float attackTime, float damage)
        {
            _hp = hp;
            _speed = speed;
            _attackTime = attackTime;
            _attackTime = damage;

            _itemManager = FindFirstObjectByType<ItemManager>();
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();

            // TODO ================================
            _hp = HP;
            hpBar.fillAmount = _hp / HP;

            _isTrace = false;
            _isDead = false;
            /////////////////////////////////////////
        }
       
        private void Update()
        {
            // 두 위치(플레이어 -> 몬스터) 간의 벡터(방향 + 거리 포함)
            var toMonster = transform.position - _playerTransform.position;
            
            // 거리 추출
            // 항상 양수
            _toMonsterDistance = toMonster.magnitude;

            // 정규화된 방향 벡터(길이는 1, 방향 정보만 유지)
            var toMonsterDirection = toMonster.normalized;
            
            // 몬스터가 바라보는 방향 계산
            // localScale.x가 양수면 오른쪽, 음수면 왼쪽을 바라보는 것으로 판단
            // Vector3.right는 +X축 방향
            var moveDirection = Vector3.right * transform.localScale.x;
            
            float dotValue = Vector3.Dot(moveDirection, toMonsterDirection);
            
            // 몬스터 기준, 플레이어가 시야각 안에 있는지 확인
            _isTrace = dotValue < -0.5f;
            
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.PATROL:
                    // 정찰 기능
                    Patrol();
                    break;
                case State.TRACE:
                    // 추적 기능
                    Trace();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground") 
                || other.gameObject.CompareTag("Player"))
                return;
            
            var scaleX = transform.localScale.x * -1;
            transform.localScale = new Vector3(scaleX, 1f, 1f);
        }
    }
}