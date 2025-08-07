using TMPro;
using UnityEngine.UI;

namespace Knight
{
    public class Player
    {
        private readonly Item[] _items = new Item[Define.INVNETORY_COUNT];
        private InventoryItemIcon[] _inventoryItemIcons;

        private TMP_Text _idText;
        private TMP_Text _levelText;
        private TMP_Text _goldText;
        
        private Image _hpBar;
        
        private string _id = "shine94";
        private int _gold = 1000;
        private int _exp = 150;
        private int _hp = 100;
        
        private float _currentHp = 50.5f;
        private float _atkDamage = 3f;
        private float _speed = 4f;
        private float _jumpPower = 12f;

        private static Player _instance;

        public static Player GetInstance()
        {
            if (_instance == null)
                _instance = new Player();

            return _instance;
        }

        public void Init(
            Item[] items,
            string id, int gold, int exp, int hp, 
            float currentHp, float atkDamage, float speed, float jumpPower)
        {
            var itemCount = items.Length;
            
            for (var i = 0; i < itemCount; i++)
            {
                _items[i] = items[i];
            }
            
            for (var i = itemCount; i < Define.INVNETORY_COUNT; i++)
            {
                _items[i] = null;
            }

            _id = id;
            _gold = gold;
            _exp = exp;
            _hp = hp;
            
            _currentHp = currentHp;
            _atkDamage = atkDamage;
            _speed = speed;
            _jumpPower = jumpPower;
        }

        public float GetDamage() => _atkDamage;
        public float GetCurrentHp() => _currentHp;
        public bool IsFullHp() => _currentHp >= _hp;
        public float GetSpeed() => _speed;
        public float GetJumpPower() => _jumpPower;
        public int GetGold() => _gold;
        public Item GetItemByIdx(int idx) => _items[idx];
        
        public float GetHpRatio()
        {
            if (_currentHp == 0)
                return 0;

            return _currentHp / _hp;
        }
        
        public int GetLevel()
        {
            return (_exp / 100) switch
            {
                0 => 1,
                1 => 2,
                2 => 3,
                3 => 4,
                _ => 5
            };
        }
        
        public void SetDamage(float damage) => _atkDamage = damage;
        public void TakeDamage(float damage) => _currentHp -= damage;
        public void RecoveryHp() => _currentHp += Define.RECOVERY_HP;
        public void SetHpBar(Image image) => _hpBar = image;
        public void BuyItem(int value) => _gold -= value;
        
        public void UseItem(Define.ItemType itemType, int value)
        {
            switch (itemType)
            {
                case Define.ItemType.PotionHp:
                    _currentHp += value;
                    _hpBar.fillAmount = GetHpRatio();
                    return;
                case Define.ItemType.PotionAtk:
                    _atkDamage += value;
                    return;
                case Define.ItemType.Gold:
                    _gold += value;
                    return;
            }
        }

        public void SetInventoryInit()
        {
            _inventoryItemIcons = UIManager
                .GetInstance()
                .FindUIComponentsByName<InventoryItemIcon>($"{Define.UiName.Inventory}");
            
            for (var i = 0; i < Define.INVNETORY_COUNT; i++)
            {
                if (_items[i] == null)
                    continue;
                
                _inventoryItemIcons[i].AddItem(_items[i]);
            }
        }

        public void SetHUD(TMP_Text idText, TMP_Text levelText, TMP_Text goldText)
        {
            _idText = idText;
            _levelText = levelText;
            _goldText = goldText;
        }
        
        public void SetHUDData()
        {
            _idText.text = _id;
            _levelText.text = $"{GetLevel()}";
            _goldText.text = $"{_gold}";
        }
    }
}