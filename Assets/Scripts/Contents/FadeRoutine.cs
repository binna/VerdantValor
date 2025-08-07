using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class FadeRoutine : MonoBehaviour
    {
        [SerializeField]
        private Image fadePanel;

        public IEnumerator Fade(float fadeTime, Color color, bool isFadeStart)
        {
            float timer = 0f;
            float percent = 0f;

            fadePanel.gameObject.SetActive(true);   
            
            while (percent < 1f)
            {
                timer += Time.deltaTime;
                percent = timer / fadeTime;
                
                float value = isFadeStart ? percent : 1 - percent;
                
                fadePanel.color = new Color(color.r, color.g, color.b, value);
                yield return null;
            }
        }
    }
}