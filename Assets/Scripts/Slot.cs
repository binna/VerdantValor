using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] 
        private Image itemImage;

        [SerializeField] 
        private Button slotButton;
        
        private BaseItem item;
        
        public bool isEmpty = true;
        
        void Awake()
        {
            slotButton.onClick.AddListener(UseItem);
        }

        void OnEnable()
        {
            slotButton.interactable = !isEmpty;
            itemImage.gameObject.SetActive(!isEmpty);
        }

        public void AddItem(BaseItem newItem)
        {
            item = newItem;
            isEmpty = false;
            itemImage.sprite = item.icon;
            
            // SetNativeSize: sprite의 원본 크기로 UI 이미지 크기를 조정
            //  원본 비율을 유지하고 싶을 때
            //  이미지 리소스를 동적으로 교체할 때
            itemImage.SetNativeSize();

            slotButton.interactable = !isEmpty;
            itemImage.gameObject.SetActive(!isEmpty);
        }

        public void UseItem()
        {
            item = null;
            isEmpty = true;
            slotButton.interactable = !isEmpty;
            itemImage.gameObject.SetActive(!isEmpty);
        }
    }
}