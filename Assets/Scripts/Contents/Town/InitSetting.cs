using TMPro;
using UnityEngine;

namespace Knight.Town
{
    public class InitSetting : MonoBehaviour
    {
        private void Start()
        {
            SoundManager
                .GetInstance()
                .PlaySound(
                    Define.SoundType.Bgm, 
                    Resources.Load<AudioClip>(Define.TOWN_BGM_PATH));
            
            var shopItemIcons = UIManager
                .GetInstance()
                .FindUIComponentsByName<ShopItemIcon>($"{Define.UiName.Shop}");
            
            for (int i = 0; i < shopItemIcons.Length; i++)
            {
                if (GameData.shopItems.TryGetValue(i + 1, out var shopItem))
                {
                    shopItemIcons[i].Init(shopItem);
                }
            }
            
            Player.GetInstance().SetInventoryInit();
            Player.GetInstance().SetHUD(
                    UIManager
                        .GetInstance()
                        .FindUIComponentByName<TMP_Text>($"{Define.UiName.HUD}", "Txt_Id"),
                    UIManager
                        .GetInstance()
                        .FindUIComponentByName<TMP_Text>($"{Define.UiName.HUD}", "Txt_Level"),
                    UIManager
                        .GetInstance()
                        .FindUIComponentByName<TMP_Text>($"{Define.UiName.HUD}", "Txt_Gold")
                );
            Player.GetInstance().SetHUDData();
        }
    }
}