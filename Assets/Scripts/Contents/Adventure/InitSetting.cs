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
                    Resources.Load<AudioClip>(Define.INTRO_BGM_PATH));
            
            // TODO 노래 골라서 설정
            
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