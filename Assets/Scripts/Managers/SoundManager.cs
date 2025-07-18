using Knight.Town;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] 
        private Slider bgmVolume;
        
        [SerializeField] 
        private Slider eventVolume;
         
        [SerializeField] 
        private Toggle bgmMute;
        
        [SerializeField] 
        private Toggle eventMute;
        
        private AudioSource _bgmAudio;
        private AudioSource _eventAudio;

        private AudioClip _introBGM;
        private AudioClip _townBGM;
        
        private AudioClip _portalEvent;
        
        private static SoundManager _instance;

        public static SoundManager GetInstance()
        {
            if (_instance == null)
            {
                GameObject soundManager = GameObject.Find(Define.GameObjectNames.SOUND_MANAGER);
                
                if (soundManager == null)
                {
                    soundManager = new GameObject(Define.GameObjectNames.SOUND_MANAGER);
                    soundManager.AddComponent<SoundManager>();
                }
                
                DontDestroyOnLoad(soundManager);
                    
                _instance = soundManager.GetComponent<SoundManager>();
            }

            return _instance;
        }

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                switch (child.gameObject.name)
                {
                    case Define.GameObjectNames.BGM_AUDIO:
                        _bgmAudio = child.GetComponent<AudioSource>();
                        break;
                    case Define.GameObjectNames.EVENT_AUDIO:
                        _eventAudio = child.GetComponent<AudioSource>();
                        break;
                }
            }
            
            if (_bgmAudio == null)
            {
                GameObject bgmAudio = new GameObject(Define.GameObjectNames.BGM_AUDIO);
                bgmAudio.transform.SetParent(transform);
                
                _bgmAudio = bgmAudio.AddComponent<AudioSource>();
                _bgmAudio.playOnAwake = false;
                
                bgmVolume = UIManager
                    .GetInstance()
                    .FindUIComponentByName<Slider>(
                        $"{Define.UiName.Setting}", Define.UiObjectNames.SLIDER_BGM_VOLUME);
                
                bgmMute = UIManager
                    .GetInstance()
                    .FindUIComponentByName<Toggle>(
                        $"{Define.UiName.Setting}", Define.UiObjectNames.TOGGLE_BGM_MUTE);
            }

            if (_eventAudio == null)
            {
                GameObject eventAudio = new GameObject(Define.GameObjectNames.EVENT_AUDIO);
                eventAudio.transform.SetParent(transform);
                
                _eventAudio = eventAudio.AddComponent<AudioSource>();
                _eventAudio.playOnAwake = false;
                
                eventVolume = UIManager
                    .GetInstance()
                    .FindUIComponentByName<Slider>(
                        $"{Define.UiName.Setting}", Define.UiObjectNames.SLIDER_EVENT_VOLUME);
                
                eventMute = UIManager
                    .GetInstance()
                    .FindUIComponentByName<Toggle>(
                        $"{Define.UiName.Setting}", Define.UiObjectNames.TOGGLE_EVENT_MUTE);
            }

            _introBGM = Resources.Load<AudioClip>(Define.INTRO_BGM_PATH);
            _townBGM = Resources.Load<AudioClip>(Define.TOWN_BGM_PATH);
            
            _portalEvent = Resources.Load<AudioClip>(Define.PORTAL_PATH);
            
            bgmVolume.value = _bgmAudio.volume;
            eventVolume.value = _eventAudio.volume;
            
            bgmMute.isOn = _bgmAudio.mute; 
            eventMute.isOn = _eventAudio.mute;

            
        }

        private void Start()
        {
            _bgmAudio.clip = _introBGM;
            _bgmAudio.Play();

            bgmVolume.onValueChanged.AddListener(OnBgmVolumeChanged);
            eventVolume.onValueChanged.AddListener(OnEventVolumeChanged);
        
            bgmMute.onValueChanged.AddListener(OnBgmMute);
            eventMute.onValueChanged.AddListener(OnEventMute);
        }

        public void PlaySound(Define.SoundType soundType)
        {
            switch (soundType)
            {
                case Define.SoundType.IntroBgm:
                    _bgmAudio.clip = _introBGM;
                    _bgmAudio.loop = true;
                    _bgmAudio.Play();
                    break;
                case Define.SoundType.TownBgm:
                    _bgmAudio.clip = _townBGM;
                    _bgmAudio.loop = true;
                    _bgmAudio.Play();
                    break;
                case Define.SoundType.PortalEvent:
                    _bgmAudio.loop = false;
                    _eventAudio.PlayOneShot(_portalEvent);
                    break;
            }
        }
        
        public void StopBGMSound()
        {
            _bgmAudio.Stop();
        }

        private void OnBgmVolumeChanged(float value)
        {
            _bgmAudio.volume = value;
        }

        private void OnEventVolumeChanged(float value)
        {
            _eventAudio.volume = value;
        }

        private void OnBgmMute(bool isMute)
        {
            _bgmAudio.mute = isMute;
        }

        private void OnEventMute(bool isMute)
        {
            _eventAudio.mute = isMute;
        }
    }
}