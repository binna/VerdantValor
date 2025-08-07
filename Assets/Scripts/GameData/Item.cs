namespace Knight
{
    public class Item
    {
        private readonly int _itemId;
        private readonly Define.ItemType _itemType;
        private readonly string _itemName;
        private readonly int _value;

        public Item(int itemId, Define.ItemType itemType, string itemName, int value)
        {
            _itemId = itemId;
            _itemType = itemType;
            _itemName = itemName;
            _value = value;
        }
        
        public string GetItemName() => _itemName;
        public string GetDescription => $"{GetTypeDescription()} {_value}";

        public void Use()
        {
            Player.GetInstance().UseItem(_itemType, _value);
        }
        
        private string GetTypeDescription()
        {
            return _itemType switch
            {
                Define.ItemType.PotionHp => "HP",
                Define.ItemType.PotionAtk => "공격력",
                _ => ""
            };
        }
    }
}