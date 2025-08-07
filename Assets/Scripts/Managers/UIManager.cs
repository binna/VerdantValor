using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class UIManager : MonoBehaviour
    {
        private Transform _uiRoot;
        
        private readonly Dictionary<string, GameObject> _uiMap = new();
        
        private static UIManager _instance;

        public static UIManager GetInstance() => _instance;

        public GameObject FindUIByName(string uiName)
        {
            _uiMap.TryGetValue(uiName, out var popup);
            return popup;
        }
        
        public T FindUIComponentByName<T>(string uiName, string objectName) where T : MonoBehaviour
        {
            if (_uiMap.TryGetValue(uiName, out var ui))
            {
                var components = ui.GetComponentsInChildren<T>(true);
                
                foreach (var component in components)
                {
                    if (component.gameObject.name.Equals(objectName))
                        return component;
                }
            }

            return null;
        }
        
        public T[] FindUIComponentsByName<T>(string uiName) where T : MonoBehaviour
        {
            return _uiMap.TryGetValue(uiName, out var ui) ? 
                ui.GetComponentsInChildren<T>(true) : null;
        }

        public void Hide(string uiName)
        {
            _uiMap.TryGetValue(uiName, out var popup);

            if (popup != null)
                popup.SetActive(false);
        }

        public void Show(string uiName)
        {
            _uiMap.TryGetValue(uiName, out var popup);

            if (popup != null)
                popup.SetActive(true);
        }
        
        public void ShowAlarm(string message)
        {
            _uiMap.TryGetValue($"{Define.UiName.Alarm}", out var popup);

            if (popup == null)
                return;

            var texts = popup
                .GetComponentsInChildren<TextMeshProUGUI>(true);
            
            popup.SetActive(true);
            foreach (var text in texts)
            {
                if (!text.gameObject.name.Equals("Txt_Message"))
                    continue;
                
                text.text = message;
            }
        }

        #region 이벤트 함수
        private void Awake()
        {
            _instance = this;

            var ui = GameObject.Find("UI");
            if (ui == null)
                return;
            
            _uiRoot = ui.transform;

            List<Button> enterButtons = new();

            foreach (Transform child in _uiRoot)
            {
                _uiMap.Add(child.gameObject.name, child.gameObject);

                var buttons = child
                    .GetComponentsInChildren<Button>(true);

                foreach (var button in buttons)
                {
                    if (button.gameObject.name.Equals(Define.UiObjectNames.EXIT_BUTTON))
                    {
                        button
                            .onClick
                            .AddListener(() => Hide(child.gameObject.name));
                        continue;
                    }

                    if (button.gameObject.name.Contains(Define.UiObjectNames.ENTER_BUTTON))
                    {
                        enterButtons.Add(button);
                    }
                }
            }

            foreach (var enterButton in enterButtons)
            {
                enterButton
                    .onClick
                    .AddListener(() =>
                        Show(
                            enterButton.gameObject.name.Replace(
                                Define.UiObjectNames.ENTER_BUTTON, "")));
            }
        }
        #endregion
    }
}