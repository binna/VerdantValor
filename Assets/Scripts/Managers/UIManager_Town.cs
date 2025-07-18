using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Knight.Town
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Button startBtn;

        [SerializeField]
        private GameObject playGame;

        [SerializeField] 
        private Transform uiRoot;
        
        private Dictionary<string, GameObject> _uiMap = new();
        
        private static UIManager _instance;

        public static UIManager GetInstance()
        {
            if (_instance == null)
            {
                GameObject uiManager = GameObject.Find("@UIManager");

                if (uiManager == null)
                {
                    uiManager = new GameObject("@UIManager");
                    uiManager.AddComponent<UIManager>();

                    // TODO ==================================
                    // GameObject d = GameObject.Find("UI");
                    // if (d != null)
                    // {
                    //     foreach (Transform t in d.transform)
                    //     {
                    //         if 
                    //     }
                    // }
                    ///////////////////////////////////////////
                }
                
                _instance = uiManager.GetComponent<UIManager>();
            }

            return _instance;
        }
        
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
            List<Button> enterButtons = new();
            
            foreach (Transform child in uiRoot)
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

        private void Start()
        {
            startBtn
                .onClick
                .AddListener(OnClickStartButton);
        }
        #endregion
        
        private void OnClickStartButton()
        {
            _uiMap.TryGetValue($"{Define.UiName.Intro}", out var intro);
            _uiMap.TryGetValue($"{Define.UiName.HUD}", out var hud);
            
            if (intro == null || hud == null)
                return;

            var idInputField = intro
                .transform
                .GetComponentInChildren<TMP_InputField>(true);
            
            if (string.IsNullOrEmpty(idInputField.text))
            {
                Show($"{Define.UiName.Alarm}");
                return;
            }
            
            #region 데이터 세팅
            GameData.Init();
            
            // 유저
            var inventoryItemIcons = 
                FindUIComponentsByName<InventoryItemIcon>($"{Define.UiName.Inventory}");
            
            var myItems = new Item[Define.INVNETORY_COUNT];
            myItems[0] = GameData.items[1];
            myItems[1] = GameData.items[1];
            myItems[2] = GameData.items[2];
            myItems[3] = GameData.items[2];
            myItems[4] = GameData.items[2];
            
            Player.GetInstance().Init(
                inventoryItemIcons, 
                myItems,
                "shine94", 1000, 150, 100, 
               3f, 4f, 12f);

            // 상점
            var shopItemIcons = 
                FindUIComponentsByName<ShopItemIcon>($"{Define.UiName.Shop}");
            
            for (int i = 0; i < shopItemIcons.Length; i++)
            {
                if (GameData.shopItems.TryGetValue(i + 1, out var shopItem))
                {
                    shopItemIcons[i].Init(shopItem);
                }
            }
            #endregion
            
            var images = hud
                .GetComponentsInChildren<Image>(true);
                
            foreach (var image in images)
            {
                if (!image.gameObject.name.Equals("Img_Bar"))
                    continue;

                Player.GetInstance().SetHpBar(image);
                break;
            }
            
            var texts = hud
                .GetComponentsInChildren<TextMeshProUGUI>(true);
            
            foreach (var text in texts)
            {
                switch (text.gameObject.name)
                {
                    case Define.UiObjectNames.TXT_ID:
                        text.text = idInputField.text;
                        continue;
                    case Define.UiObjectNames.TXT_LEVEL:
                        text.text = $"{Player.GetInstance().GetLevel()}";
                        continue;
                }
            }
            
            SoundManager.GetInstance().PlaySound(Define.SoundType.TownBgm);
            
            playGame.SetActive(true);
            Hide($"{Define.UiName.Intro}");
        }
    }
}