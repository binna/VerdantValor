using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class Setting : MonoBehaviour
    {
        private Slider _bgmVolume;
        private Slider _eventVolume;
        private Toggle _bgmMute;
        private Toggle _eventMute;

        #region 이벤트 함수
        private void Start()
        {
            _bgmVolume = UIManager
                .GetInstance()
                .FindUIComponentByName<Slider>(
                    $"{Define.UiName.Setting}", Define.UiObjectNames.SLIDER_BGM_VOLUME);
                
            _bgmMute = UIManager
                .GetInstance()
                .FindUIComponentByName<Toggle>(
                    $"{Define.UiName.Setting}", Define.UiObjectNames.TOGGLE_BGM_MUTE);
            
            _eventVolume = UIManager
                .GetInstance()
                .FindUIComponentByName<Slider>(
                    $"{Define.UiName.Setting}", Define.UiObjectNames.SLIDER_EVENT_VOLUME);
                
            _eventMute = UIManager
                .GetInstance()
                .FindUIComponentByName<Toggle>(
                    $"{Define.UiName.Setting}", Define.UiObjectNames.TOGGLE_EVENT_MUTE);
            
            _bgmVolume.value = SoundManager.GetInstance().GetBgmAudio().volume;
            _eventVolume.value = SoundManager.GetInstance().GetEventAudio().volume;
            
            _bgmMute.isOn = SoundManager.GetInstance().GetBgmAudio().mute; 
            _eventMute.isOn = SoundManager.GetInstance().GetEventAudio().mute;
            
            _bgmVolume.onValueChanged.AddListener(OnBgmVolumeChanged);
            _eventVolume.onValueChanged.AddListener(OnEventVolumeChanged);
            
            _bgmMute.onValueChanged.AddListener(OnBgmMute);
            _eventMute.onValueChanged.AddListener(OnEventMute);
        }
        #endregion
        
        private void OnBgmVolumeChanged(float value)
        { 
            SoundManager.GetInstance().GetBgmAudio().volume = value;
        }
        
        private void OnEventVolumeChanged(float value)
        {
            SoundManager.GetInstance().GetEventAudio().volume = value;
        }
        
        private void OnBgmMute(bool isMute)
        {
            SoundManager.GetInstance().GetBgmAudio().mute = isMute;
        }
        
        private void OnEventMute(bool isMute)
        {
            SoundManager.GetInstance().GetEventAudio().mute = isMute;
        }
    }
}