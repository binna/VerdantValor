using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject settingPopup;

        [SerializeField]
        private Button settingButton;
        
        [SerializeField]
        private Button exitTopButton;
        
        [SerializeField]
        private Button exitBottomButton;

        private void Start()
        {
            settingButton.onClick.AddListener(OnSettingPopup);
            exitTopButton.onClick.AddListener(OnCloseSettingPopup);
            exitBottomButton.onClick.AddListener(OnCloseSettingPopup);
        }

        public void OnSettingPopup()
        {
            settingPopup.SetActive(true);
            Time.timeScale = 0f;
        }
        
        public void OnCloseSettingPopup()
        {
            settingPopup.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}