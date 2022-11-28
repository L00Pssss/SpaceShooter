using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    // интерфейсы. 
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler,IPointerDownHandler
    {
        // фон
        [SerializeField] private Image m_JoyBack;
        // сам стик
        [SerializeField] private Image m_Joystick;

        public Vector3 Value { get; private set; }

        // eventData - экранная кордината. (ScreenPointToLocalPointInRectangle)
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 positon = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_JoyBack.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out positon);

            positon.x = (positon.x / m_JoyBack.rectTransform.sizeDelta.x);
            positon.y = (positon.y / m_JoyBack.rectTransform.sizeDelta.y);

            positon.x = positon.x * 2 - 1;
            positon.y = positon.y * 2 - 1;

            Value = new Vector3(positon.x, positon.y, 0);

            if (Value.magnitude > 1)
                Value = Value.normalized;

            float offsetX = m_JoyBack.rectTransform.sizeDelta.x /
                2 - m_Joystick.rectTransform.sizeDelta.x / 2;
            float offsetY = m_JoyBack.rectTransform.sizeDelta.y /
                2 - m_Joystick.rectTransform.sizeDelta.y / 2;

            m_Joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}