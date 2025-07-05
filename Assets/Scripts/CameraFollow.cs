using UnityEngine;

namespace Knight
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        
        [SerializeField]
        private Vector3 offset = new(0f, 3f, -10f);
        
        [SerializeField]
        private float smoothSpeed = 5f;
        
        // minBound ~ maxBound 내로 카메라 이동 가능
        [SerializeField]
        private Vector2 minBound = new(-9.5f, -11f);
        
        [SerializeField]
        private Vector2 maxBound = new(28.5f, 13f);

        private void LateUpdate()
        {
            if (target == null)
                return;
            
            Vector3 destination = target.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, destination, Time.deltaTime * smoothSpeed);
            
            smoothPos.x = Mathf.Clamp(smoothPos.x, minBound.x, maxBound.x);
            smoothPos.y = Mathf.Clamp(smoothPos.y, minBound.y, maxBound.y);
            
            transform.position = smoothPos;
        }

        public void SetMaxXBound(float x)
        {
            this.maxBound = new Vector2(x, this.maxBound.y);
        }
    }
}