using UnityEngine;

namespace Knight.Town
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField]
        private Define.UiName popupName;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag($"{Define.Tag.Player}"))
            {
                UIManager.GetInstance().Show(popupName.ToString());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag($"{Define.Tag.Player}"))
            {
                UIManager.GetInstance().Hide(popupName.ToString());
            }
        }
    }
}