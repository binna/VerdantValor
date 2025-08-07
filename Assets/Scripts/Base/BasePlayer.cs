using UnityEngine;

namespace Knight
{
    public class BasePlayer : MonoBehaviour
    {
        protected static bool _isBlocked;
        
        public void BlockInput()
        {
            _isBlocked = true;
        }
        
        public void UnblockInput()
        {
            _isBlocked = false;
        }
    }
}