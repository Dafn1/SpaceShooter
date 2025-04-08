using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Common
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler //интерфнйс дл€ джойстика
    {
        [SerializeField] private Image m_JoyBack;  // картинка указывающа€ на фон джойстика
        [SerializeField] private Image m_Joystick;  //картинка указывающа€ на сам стик

        public Vector3 Value { get; private set; } //значение конкретного джойстика с ограничением свойства

        public void OnDrag(PointerEventData eventData) //метод которые предоставл€ют 3 интерфейса
        {
            Vector2 position = Vector2.zero; //пустой вектор, который обозначает позицию

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_JoyBack.rectTransform, //
                eventData.position,
                eventData.pressEventCamera,
                out position);

            position.x = (position.x / m_JoyBack.rectTransform.sizeDelta.x);
            position.y = (position.y / m_JoyBack.rectTransform.sizeDelta.y);

            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            Value = new Vector3(position.x, position.y, 0);

            if (Value.magnitude > 1)
                Value = Value.normalized;

            float offsetX = m_JoyBack.rectTransform.sizeDelta.x / 2 - m_Joystick.rectTransform.sizeDelta.x / 2;
            float offsetY = m_JoyBack.rectTransform.sizeDelta.y / 2 - m_Joystick.rectTransform.sizeDelta.y / 2;

            m_Joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
        }

        public void OnPointerDown(PointerEventData eventData) //метод которые предоставл€ют 3 интерфейса
        {
            OnDrag(eventData); // при отпускаии джойстика стик уходит на свою позицию
        }

        public void OnPointerUp(PointerEventData eventData) //метод которые предоставл€ют 3 интерфейса
        {
            Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}


