using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Knight
{
    public class PortalController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject portalEffect;

        [SerializeField] 
        private Define.SceneType scene;
        
        [SerializeField]
        private BasePlayer player;
        
        private FadeRoutine _fade;
        
        private GameObject _loadingImage;
        private Image _progressBar;
        
        private AudioClip _audioClip;
        
        private void Start()
        {
            _fade = UIManager
                .GetInstance()
                .FindUIByName($"{Define.UiName.Fade}")
                .GetComponent<FadeRoutine>();

            _loadingImage = UIManager
                .GetInstance()
                .FindUIComponentByName<Image>(
                    $"{Define.UiName.Loading}", Define.UiObjectNames.IMG_BACKGROUND).gameObject;
            
            _progressBar = UIManager
                .GetInstance()
                .FindUIComponentByName<Image>(
                    $"{Define.UiName.Loading}", Define.UiObjectNames.IMG_PROGRESS_BAR);

            _audioClip = Resources.Load<AudioClip>(Define.PORTAL_PATH);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            SoundManager.GetInstance().PlaySound(Define.SoundType.Event, _audioClip);

            player.BlockInput();
            
            if (other.CompareTag($"{Define.Tag.Player}"))
            {
                StartCoroutine(PortalRoutine());
            }

        }

        private IEnumerator PortalRoutine()
        {
            SoundManager.GetInstance().StopBGMSound();
            
            portalEffect.SetActive(true);
            yield return StartCoroutine(_fade.Fade(3f, Color.white, true));

            _loadingImage.SetActive(true);
            yield return StartCoroutine(_fade.Fade(3f, Color.white, false));

            while (_progressBar.fillAmount < 1f)
            {
                _progressBar.fillAmount += Time.deltaTime * 0.3f;
            
                yield return null;
            }

            SceneManager.LoadScene((int)scene);
        }
    }
}