using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Common
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler //��������� ��� ���������
    {
        [SerializeField] private Image m_JoyBack;  // �������� ����������� �� ��� ���������
        [SerializeField] private Image m_Joystick;  //�������� ����������� �� ��� ����

        public Vector3 Value { get; private set; } //�������� ����������� ��������� � ������������ ��������

        public void OnDrag(PointerEventData eventData) //����� ������� ������������� 3 ����������
        {
            Vector2 position = Vector2.zero; //������ ������, ������� ���������� �������

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

        public void OnPointerDown(PointerEventData eventData) //����� ������� ������������� 3 ����������
        {
            OnDrag(eventData); // ��� ��������� ��������� ���� ������ �� ���� �������
        }

        public void OnPointerUp(PointerEventData eventData) //����� ������� ������������� 3 ����������
        {
            Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}


