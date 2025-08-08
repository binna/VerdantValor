using TMPro;
using UnityEngine;

namespace Knight.Adventure
{
    public class InitSetting : MonoBehaviour
    {
        private void Start()
        {
            SoundManager
                .GetInstance()
                .PlaySound(
                    Define.SoundType.Bgm, 
                    Resources.Load<AudioClip>(Define.ADVENTURE_BGM_PATH));

            // #region 삭제할 것
            // GameData.Init();
            //
            // var myItems = new Item[Define.INVNETORY_COUNT];
            // myItems[0] = GameData.items[1];
            // myItems[1] = GameData.items[1];
            // myItems[2] = GameData.items[2];
            // myItems[3] = GameData.items[2];
            // myItems[4] = GameData.items[2];
            //
            // Player.GetInstance().Init(
            //     myItems,
            //     "shine94", 1000, 150, 100,
            //     50.5f, 3f, 4f, 12f);
            //
            // #endregion
            
            Player.GetInstance().SetInventoryInit();
            Player.GetInstance().SetHUD(
                UIManager
                    .GetInstance()
                    .FindUIComponentByName<TMP_Text>(
                        $"{Define.UiName.HUD}", Define.UiObjectNames.TXT_ID),
                UIManager
                    .GetInstance()
                    .FindUIComponentByName<TMP_Text>(
                        $"{Define.UiName.HUD}", Define.UiObjectNames.TXT_LEVEL),
                UIManager
                    .GetInstance()
                    .FindUIComponentByName<TMP_Text>(
                        $"{Define.UiName.HUD}", Define.UiObjectNames.TXT_GOLD)
            );
            Player.GetInstance().SetHUDData();
        }
    }
}