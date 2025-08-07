using System.Collections;
using TMPro;
using UnityEngine;

namespace Knight.Town
{
    public class TypingText : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI textUI;

        private string _currText;

        void Awake()
        {
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
                yield return new WaitForSeconds(Define.DIALOG_TYPING_SPEED);
            }
        }
    }
}