using UnityEngine;

namespace Knight
{
    public abstract class BasePlayer : MonoBehaviour
    {
        protected static bool _isBlocked;
        
        protected static Vector3 _initPosition = new(-19.05f, -1.52f, 0);
        protected static Vector3 _initScale = new(1, 1, 1);

        public abstract void UpdatePosition(Vector3 position, Vector3 scale);
        
        public void BlockInput()
        {
            _isBlocked = true;
        }
    }
}