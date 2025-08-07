using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knight.Town
{
    public class ShopItemIcon : MonoBehaviour
    {
        private Image _icon;
        private Button _button;
        private TextMeshProUGUI _description;
        
        private ShopItem _item;
        
        public void Init(ShopItem newItem)
        {
            _item = newItem;
            
            SetShopItemData();
        }

        #region 이벤트 함수
        private void Awake()
        {
            
            var children = transform.GetComponentsInChildren<Image>(true);

            foreach (var child in children)
            {
                if (!child.name.Equals(Define.UiObjectNames.IMG_ICON))
                    continue;
                
                _icon = child;
                break;
            }
            
            _description = GetComponentInChildren<TextMeshProUGUI>();
            _button = transform.GetComponentInChildren<Button>(true);

            SetShopItemData();
        }
        #endregion

        private void SetShopItemData()
        {
            if (_icon == null 
                || _description == null 
                || _button == null
                || _item == null)
                return;
            
            _icon.sprite = Resources
                .Load<Sprite>($"{Define.IMAGES_PATH}{_item.GetItemName()}");
            
            _description.text = _item.GetItemDescription();
            _button.onClick.AddListener(_item.BuyItem);
        }
    }
}