using System.Collections;
using TMPro;
using UnityEngine;


namespace Knight.Town
{
    public class TypingText : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI textUI;

        [SerializeField] 
        private float typingSpeed = 0.1f;

        private string _currText;

        void Awake()
        {
            // 유니티 상에 적힌 글씨 저장
            _currText = textUI.text;
        }

        void OnEnable()
        {
            textUI.text = string.Empty;

            StartCoroutine(TypingRoutine());
        }

        IEnumerator TypingRoutine()
        {
            int textLength = _currText.Length;

            for (int i = 0; i < textLength; i++)
            {
                textUI.text += _currText[i];
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}