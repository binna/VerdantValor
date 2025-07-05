using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource bgmAudio;

        [SerializeField]
        private AudioSource eventAudio;
        
        [SerializeField]
        private AudioClip[] clips;

        [SerializeField] 
        private Slider bgmVolume;
        
        [SerializeField] 
        private Slider eventVolume;
         
        [SerializeField] 
        private Toggle bgmMute;
        
        [SerializeField] 
        private Toggle eventMute;

        private void Awake()
        {
            bgmVolume.value = bgmAudio.volume;
            eventVolume.value = eventAudio.volume;
            
            bgmMute.isOn = bgmAudio.mute; 
            eventMute.isOn = eventAudio.mute;
        }

        private void Start()
        {
            BGMSoundPlay("Town BGM");

            bgmVolume.onValueChanged.AddListener(OnBgmVolumeChanged);
            eventVolume.onValueChanged.AddListener(OnEventVolumeChanged);
        
            bgmMute.onValueChanged.AddListener(OnBgmMute);
            eventMute.onValueChanged.AddListener(OnEventMute);
        }

        public void EventSoundPlay(string clipName)
        {
            foreach (var clip in clips)
            {
                if (clip.name == clipName)
                {
                    eventAudio.PlayOneShot(clip);
                    return;
                }
            }
            
            Debug.Log($"{clipName}을 찾지 못했습니다.");
        }
        
        private void BGMSoundPlay(string clipName)
        {
            foreach (var clip in clips)
            {
                if (clip.name == clipName)
                {
                    bgmAudio.clip = clip;
                    bgmAudio.Play();
                    return;
                }
            }
            
            Debug.Log($"{clipName}을 찾지 못했습니다.");
        }

        private void OnBgmVolumeChanged(float value)
        {
            bgmAudio.volume = value;
        }

        private void OnEventVolumeChanged(float value)
        {
            eventAudio.volume = value;
        }

        private void OnBgmMute(bool isMute)
        {
            bgmAudio.mute = isMute;
        }

        private void OnEventMute(bool isMute)
        {
            eventAudio.mute = isMute;
        }
    }
}