namespace Knight
{
    public class ShopItem
    {
        private readonly int _shopId;
        
        private readonly string _title;
        private readonly int _price;

        private readonly Item _item;

        public ShopItem(
            int shopId,
            string title,
            int price,
            Item item)
        {
            _shopId = shopId;
            _title = title;
            _price = price;
            _item = item;
        }
        
        public string GetItemName() => _item.GetItemName();
        public string GetItemDescription() => $"{_title}<br>{_price}원";
        public int GetPrice() => _price;

        public void BuyItem()
        {
            if (Player.GetInstance().GetGold() >= _price)
            {
                Player.GetInstance().BuyItem(_price);
                        
                UIManager
                    .GetInstance()
                    .ShowAlarm("구매에 성공했습니다.");
                return;
            }

            UIManager
                .GetInstance()
                .ShowAlarm("구매에 실패했습니다.<br>돈이 부족합니다.");
        }
    }
}