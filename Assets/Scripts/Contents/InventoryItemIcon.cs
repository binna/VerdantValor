using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class InventoryItemIcon : MonoBehaviour
    {
        private Image _icon;
        private Button _button;
        
        private Item _item;
        
        private bool _isEmpty = true;
        private bool _isFirst = true;

        public void AddItem(Item newItem)
        {
            Init();

            _item = newItem;
            _isEmpty = false;
            
            if (_icon == null || _button == null || _item == null)
                return;
                
            _icon.sprite = Resources
                .Load<Sprite>($"{Define.IMAGES_PATH}{_item.GetItemName()}");
            _icon.color = new Color(1f, 1f, 1f, 1f);
            _button.interactable = !_isEmpty;
            _icon.gameObject.SetActive(!_isEmpty);
        }
        
        #region 이벤트 함수
        private void Awake() => Init();

        private void OnEnable()
        {
            _button.interactable = !_isEmpty;
            _icon.gameObject.SetActive(!_isEmpty);
        }
        #endregion
        
        private void Init()
        {
            if (!_isFirst)
            {
                _isFirst = false;
                return;
            } 
            
            var children = GetComponentsInChildren<Image>(true);

            foreach (var child in children)
            {
                if (!child.name.Equals(Define.UiObjectNames.IMG_ICON))
                    continue;
                    
                _icon = child;
                break;
            }

            _button = GetComponent<Button>();
            
            if (_button == null)
                _button = gameObject.AddComponent<Button>();
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(UseItem);
        }
        
        private void UseItem()
        {
            _isEmpty = true;
            
            _button.interactable = !_isEmpty;

            _icon.sprite = null;
            _icon.color = new Color(1f, 1f, 1f, 0f);
            _icon.gameObject.SetActive(!_isEmpty);
            
            _item.Use();
            
            _item = null;
        }
    }
}