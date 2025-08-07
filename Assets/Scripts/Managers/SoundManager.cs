using UnityEngine;

namespace Knight
{
    public class SoundManager : MonoBehaviour
    {
        private AudioSource _bgmAudio;
        private AudioSource _eventAudio;
        
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

        public AudioSource GetBgmAudio()
        {
            return _bgmAudio;
        }
        
        public AudioSource GetEventAudio()
        {
            return _eventAudio;
        }

        #region 이벤트 함수
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
                var audioSource = new GameObject(Define.GameObjectNames.BGM_AUDIO);
                audioSource.transform.SetParent(transform);
                
                _bgmAudio = audioSource.AddComponent<AudioSource>();
                _bgmAudio.playOnAwake = false;
            }

            if (_eventAudio == null)
            {
                var audioSource = new GameObject(Define.GameObjectNames.EVENT_AUDIO);
                audioSource.transform.SetParent(transform);
                
                _eventAudio = audioSource.AddComponent<AudioSource>();
                _eventAudio.playOnAwake = false;
            }
        }
        #endregion

        public void PlaySound(Define.SoundType soundType, AudioClip audioClip)
        {
            switch (soundType)
            {
                case Define.SoundType.Bgm:
                    _bgmAudio.clip = audioClip;
                    _bgmAudio.loop = true;
                    _bgmAudio.Play();
                    break;
                case Define.SoundType.Event:
                    _bgmAudio.loop = false;
                    _eventAudio.PlayOneShot(audioClip);
                    break;
            }
        }
        
        public void StopBGMSound()
        {
            _bgmAudio.Stop();
        }
    }
}