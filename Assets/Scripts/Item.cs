using UnityEngine;

namespace Knight
{
    public class Item : MonoBehaviour, BaseItem
    {
        public ItemManager Inventory { get; set; }
        public GameObject obj { get; set; }
        public string itmeName { get; set; }
        public Sprite icon { get; set; }

        public void Get()
        {
            // 아이템을 먹은 것처럼 보여주기 위함
            gameObject.SetActive(false);
            
            // 인벤토리에게 아이템 획득을 알리는 기능
            Inventory.GetItem(this);
        }

        public void Use()
        {
            Debug.Log($"{itmeName} 아이템 사용");
        }

        void Start()
        {
            Inventory = FindFirstObjectByType<ItemManager>();
            icon = GetComponent<SpriteRenderer>().sprite;
            
            obj = gameObject;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                Get();
        }
    }
}