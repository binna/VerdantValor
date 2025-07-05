using Unity.VisualScripting;
using UnityEngine;

namespace Knight.Adventure
{
    public class Cave : MonoBehaviour
    {
        [SerializeField] 
        private GameObject globalLight;
        
        [SerializeField] 
        private GameObject userLight;
        
        [SerializeField]
        private Object mainCamera;

        private bool _isEnter;
        
        private Camera _camera;
        private CameraFollow _cameraFollow;
        
        private float _cameraSize;

        void Start()
        {
            _camera = mainCamera.GetComponent<Camera>();
            _cameraSize = _camera.orthographicSize;
            _cameraFollow = mainCamera.GetComponent<CameraFollow>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isEnter = !_isEnter;
                globalLight.SetActive(_isEnter);
                userLight.SetActive(_isEnter);

                _camera.orthographicSize = _isEnter ? 6 : _cameraSize;
                _cameraFollow.SetMaxXBound(_isEnter ? 94 : 19);
            }
        }
    }
}