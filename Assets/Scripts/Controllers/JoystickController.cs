using UnityEngine;
using UnityEngine.EventSystems;

namespace Knight
{
    public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField]
        private KnightController_Joystick knightController;

        [SerializeField] 
        private GameObject backgroundUI;

        [SerializeField] 
        private GameObject handlerUI;

        private Vector2 _startPosition;
        private Vector2 _currPosition;

        void Start()
        {
            backgroundUI.SetActive(false);
        }

        // 마우스 버튼 또는 터치가 눌렸을 때 호출
        public void OnPointerDown(PointerEventData eventData)
        {
            backgroundUI.SetActive(true);
            backgroundUI.transform.position = eventData.position;
            _startPosition = eventData.position;
        }

        // 마우스 또는 터치를 누르고 움직일때(= 드래그) 호출
        public void OnDrag(PointerEventData eventData)
        {
            _currPosition = eventData.position;
            var dragDirection = _currPosition - _startPosition;

            // distance : 거리
            // +75f, -75f : X/Y축 조이스틱 입력의 최대값(조이스틱 범위 제한)
            var maxDistance = Mathf.Min(dragDirection.magnitude, 75f);

            // _startPosition : 조이스틱 원래 위치
            // dragDirection.normalized * maxDistance : 드래그 방향값 * 최대 거리 75f
            handlerUI.transform.position = _startPosition + dragDirection.normalized * maxDistance;

            // 캐릭터 이동
            knightController.InputJoystick(dragDirection.x, dragDirection.y);
        }

        // 마우스 버튼 또는 터치가 떼어졌을 때 호출
        public void OnPointerUp(PointerEventData eventData)
        {
            // 캐릭터 이동 중지
            knightController.InputJoystick(0, 0);

            // 드래그 종료 시 조이스틱 핸들 위치 초기화
            handlerUI.transform.position = Vector2.zero;
            backgroundUI.SetActive(false);
        }
    }
}