using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Knight.Intro
{
    public class StartButton : MonoBehaviour
    {
        private TMP_InputField _idInputField;
        private bool _isFirst;

        #region 이벤트 함수
        private void Start()
        {
            GetComponent<Button>()
                .onClick
                .AddListener(OnClickStartButton);
           
            _idInputField = gameObject.transform.parent
                .Find("Img_Background/Input_Id")
                .GetComponentInChildren<TMP_InputField>(true);
        }
        #endregion

        private void OnClickStartButton()
        {
            if (string.IsNullOrEmpty(_idInputField.text))
            {
                UIManager
                    .GetInstance()
                    .ShowAlarm("아이디가 입력되지 않았습니다.<br>아이디를 입력해주세요.");
                return;
            }
            
            GetComponent<Button>().interactable = false;

            var id = _idInputField.text;
            
            // TODO 유저 정보 검색하고 세팅하는 로직 하는 부분 필요
            #region 데이터 세팅
            GameData.Init();

            var myItems = new Item[Define.INVNETORY_COUNT];
            myItems[0] = GameData.items[1];
            myItems[1] = GameData.items[1];
            myItems[2] = GameData.items[2];
            myItems[3] = GameData.items[2];
            myItems[4] = GameData.items[2];

            Player.GetInstance().Init(
                myItems,
                id, 1000, 150, 100,
                50.5f, 3f, 4f, 12f);
            #endregion
            
            SceneManager.LoadScene((int)Define.SceneType.Town);
        }
    }
}