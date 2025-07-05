using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Knight
{
    public class PortalController : MonoBehaviour
    {
        [SerializeField] 
        private FadeRoutine fade;

        [SerializeField] 
        private GameObject portalEffect;

        [SerializeField]
        private GameObject loadingImage;

        [SerializeField]
        private Image progressBar;

        [SerializeField] 
        private int sceneIdx;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(PortalRoutine());
            }

        }

        IEnumerator PortalRoutine()
        {
            portalEffect.SetActive(true);
            yield return StartCoroutine(fade.Fade(3f, Color.white, true)); // 페이드 온

            loadingImage.SetActive(true);
            yield return StartCoroutine(fade.Fade(3f, Color.white, false)); // 페이드 오프

            while (progressBar.fillAmount < 1f)
            {
                progressBar.fillAmount += Time.deltaTime * 0.3f;
            
                yield return null;
            }

            SceneManager.LoadScene(sceneIdx);

        }
    }
}
