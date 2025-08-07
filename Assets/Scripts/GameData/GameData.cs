using System.Collections.Generic;
using UnityEngine;

namespace Knight
{
    public class GameData : MonoBehaviour
    {
        public static Dictionary<int, Item> items = new();
        public static Dictionary<int, ShopItem> shopItems = new();

        private bool isOpem = false;

        public static void Init()
        {
            #region item, shopItem 데이터 세팅
            var item = new Item(1, Define.ItemType.PotionHp, "RedPotion", 10);
            var shopItem = new ShopItem(
                1, 
                "체력 10",
                500,
                item);
            
            items.Add(1, item);
            shopItems.Add(1, shopItem);
            
            item = new Item(2, Define.ItemType.PotionHp, "RedPotion", 20);
            shopItem = new ShopItem(
                2, 
                "체력 20",
                1100,
                item);
            
            items.Add(2, item);
            shopItems.Add(2, shopItem);
            
            item = new Item(3, Define.ItemType.PotionAtk, "BluePotion", 3);
            shopItem = new ShopItem(
                3, 
                "공격력 강화 3",
                700,
                item);
            
            items.Add(3, item);
            shopItems.Add(3, shopItem);
            
            item = new Item(4, Define.ItemType.PotionAtk, "BluePotion", 5);
            shopItem = new ShopItem(
                4, 
                "공격력 강화 5",
                1500,
                item);
            
            items.Add(4, item);
            shopItems.Add(4, shopItem);
            
            item = new Item(5, Define.ItemType.Gold, "GoldCoin", 100);
            items.Add(5, item);
            
            item = new Item(6, Define.ItemType.Gold, "GoldCoin", 500);
            items.Add(6, item);
            #endregion
        }
    }
}