using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knight
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject monsterRoot;
        
        [SerializeField]
        private int monsterLimit = 10;
        
        private BaseMonster[] _monsters;
        
        private int _monsterCount;
        
        private static SpawnManager _instance;
        
        public static SpawnManager GetInstance() => _instance;

        public void DeadMonster()
        {
            _monsterCount--;
        }
        
        #region 이벤트 함수
        private void Awake()
        {
            _instance = this;
            
            var monstersRoot = GameObject.Find("Monsters");

            _monsters = monstersRoot
                .GetComponentsInChildren<BaseMonster>(true);
            
            Debug.Log($"{_monsters.Length}");
        }

        private IEnumerator Start()
        {
            // 몬스터 스폰
            while (true)
            {
                // TODO 이거 스폰 시간도 랜덤하게 하기
                yield return new WaitForSeconds(3f);
                
                if (_monsterCount++ < monsterLimit)
                {
                    while (true)
                    {
                        var monsterIdx = Random.Range(0, _monsters.Length);
                        var monster = _monsters[monsterIdx];

                        if (monster.gameObject.activeSelf)
                            continue;
                        
                        StartCoroutine(monster.SpawnMonster());
                        break;
                    }
                }
            }
        }
        #endregion
    }
}