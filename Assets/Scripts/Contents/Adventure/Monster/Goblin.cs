using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knight.Adventure
{
    public class Goblin : MonsterCore
    {
        private const float TRACE_DISTANCE = 5f;
        private const float ATTACK_DISTANCE = 1.5f;
        
        private float _timer;
        private float _idleTime; 
        private float _patrolTime;
        
        private bool _isAttack;
        
        private void Start()
        {
            Init(10f, 3f, 2f, 5f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other
                    .gameObject
                    .GetComponent<KnightController_Keyboard>()
                    .TakeDamage(atkDamage);
                
                other
                    .gameObject
                    .GetComponent<Animator>()
                    .SetTrigger("Hit");
                
                // TODO 방향 맞추고 튕기기(유저는 튕길지는 좀 더 고민해보기)
                var scaleX = transform.localScale.x * -1;
                other.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }

        protected override void Idle()
        {
            _timer += Time.deltaTime;

            // 정찰
            if (_timer >= _idleTime)
            {
                // 방향 랜덤
                var scaleX = Random.Range(0, 2) == 1? 1 : -1;
                transform.localScale = new Vector3(scaleX, 1, 1);
                
                _timer = 0f;
                _patrolTime = Random.Range(1f, 5f);
                _animator.SetBool("isRun", true);
                
                ChangeState(State.PATROL);
            }
            
            // 추격
            if (_toMonsterDistance <= TRACE_DISTANCE && _isTrace)
            {
                _timer = 0f;
                _animator.SetBool("isRun", true);
                
                ChangeState(State.TRACE);
            }
        }
        
        protected override void Patrol()
        {
            transform.position += Vector3.right * transform.localScale.x * _speed * Time.deltaTime;
            
            _timer += Time.deltaTime;
            
            // 정찰 시간 끝
            if (_timer >= _patrolTime)
            {
                _timer = 0f;
                _idleTime = Random.Range(1f, 5f);
                _animator.SetBool("isRun", false);
                
                ChangeState(State.IDLE);
                return;
            }
            
            // 추격
            if (_toMonsterDistance <= TRACE_DISTANCE && _isTrace)
            {
                _timer = 0f;
                ChangeState(State.TRACE);
            }
        }
        
        protected override void Trace()
        {
            // 몬스터 -> 플레이어 방향
            var toPlayer = (_playerTransform.position - transform.position).normalized;
            var scaleX = toPlayer.x < 0f ? -1f : 1f;
            
            transform.position += Vector3.right * scaleX * _speed * Time.deltaTime;
            transform.localScale = new Vector3(scaleX, 1f, 1f);
            
            if (_toMonsterDistance > TRACE_DISTANCE)
            {
                _animator.SetBool("isRun", false);
                ChangeState(State.IDLE);
                return;
            }
            
            if (_toMonsterDistance < ATTACK_DISTANCE)
            {
                ChangeState(State.ATTACK);
            }
        }
        
        protected override void Attack()
        {
            if (!_isAttack)
                StartCoroutine(AttackRoutine());
        }

        IEnumerator AttackRoutine()
        {
            _isAttack = true;
            _animator.SetTrigger("Attack");

            // 트리거 설정 직후에는 Animator의 상태 전환이 아직 적용되지 않았을 수 있음
            // 1프레임 대기 후 상태 정보를 가져오는 것이 안전함
            yield return null;
            
            var currentAnimationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(currentAnimationLength);
            
            _animator.SetBool("isRun", false);

            // 벡터 연산의 방향성은 끝점 - 시작점 이다
            // 종점(플레이어) - 시작점(몬스터)
            // 몬스터 -> 플레이어 방향
            var toPlayer = (_playerTransform.position - transform.position).normalized;
            var scaleX = toPlayer.x > 0 ? 1 : -1;
            transform.localScale = new Vector3(scaleX, 1, 1);

            yield return new WaitForSeconds(_attackTime - 1f);
            
            _isAttack = false;
            _animator.SetBool("isRun", true);
            ChangeState(State.IDLE);
        }
    }
}