using System.Collections;
using UnityEngine;

namespace Knight.Town
{
    public class InteractionEvent : MonoBehaviour
    {
        public enum Type
        {
            SIGN,
            DOOR,
            NPC
        }
        
        [SerializeField]
        private SoundManager _soundManager;

        [SerializeField] 
        private GameObject popUp;

        [SerializeField] 
        private FadeRoutine fade;

        [SerializeField] 
        private GameObject map;

        [SerializeField] 
        private GameObject house;

        [SerializeField] 
        private Vector3 inDoorPosition;
        
        [SerializeField] 
        private Vector3 outDoorPosition;

        [SerializeField] 
        private bool isHouse;
        
        [SerializeField]
        private Type type;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interaction(other.transform);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            
            if (type != Type.DOOR && other.CompareTag("Player"))
            {
                popUp.SetActive(false);
            }
        }

        void Interaction(Transform player)
        {
            switch (type)
            {
                case Type.SIGN:
                    popUp.SetActive(true);
                    break;
                case Type.DOOR:
                    StartCoroutine(DoorRoutine(player));
                    break;
                case Type.NPC:
                    popUp.SetActive(true);
                    break;
            }
        }

        IEnumerator DoorRoutine(Transform player)
        {
            _soundManager.EventSoundPlay("Door Open");
            
            yield return StartCoroutine(fade.Fade(3f, Color.black, true));
            
            map.SetActive(isHouse);
            house.SetActive(!isHouse);
            
            var position = isHouse ? outDoorPosition : inDoorPosition;
            player.transform.position = position;

            isHouse = !isHouse;
            
            _soundManager.EventSoundPlay("Door Close");

            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(fade.Fade(3f, Color.black, false));
        }
    }
}