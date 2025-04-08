using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace SpaceShooter
{
    public class ShipInputController : MonoBehaviour //���������� ���� � �������� � ������
    {
        public enum ControlMode // ����� ����������
        {
            Keyboard, //���������� � ����������
            Mobile // ���������� � ���������
        }

        
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship; //��� ��������� ���������� ������

      

        [SerializeField] private ControlMode m_ContolMode; //��������� ���������� ������� ���������� ����� ��� ����� ����������

       

        public void Construct(VirtualGamepad virtualGamepad)
        {
            m_VirtualGamepad=virtualGamepad;
        }

        private SpaceShip m_TargetShip;
        private VirtualGamepad m_VirtualGamepad;

        

        private void Start()
        {
            if (m_ContolMode == ControlMode.Keyboard)
            {
                m_VirtualGamepad.VirtualJoystik.gameObject.SetActive(false); //���� �������� �����,�� ������� ������ ��������
               m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(false); //���� �������� �����,�� ������� ������ �������� �������� ������
                m_VirtualGamepad.MobileFireSecondory.gameObject.SetActive(false); //���� �������� �����,�� ���������� ������ ��������������� ��������
            }
            else
            {
                m_VirtualGamepad.VirtualJoystik.gameObject.SetActive(true); //���� ������� ��������, �� �������� ������ ��������
                m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(true); //���� ���� �����,�� �������� ������ �������� �������� ������
                m_VirtualGamepad.MobileFireSecondory.gameObject.SetActive(true); //���� ���� �����,�� �������� ������ ��������������� ��������
            }
                
        }

        //������������� ����������
        private void Update()
        {
            if (m_TargetShip == null) return; //���� ������� �����,�� ������� �� ������

            if (m_ContolMode == ControlMode.Keyboard) //���� ������� ���������� �����,�� �������� ���������� �����
                ControlKeyboard();
            if (m_ContolMode == ControlMode.Mobile) // ���� ������� ��������,�� �������� ���������� �����
                ControlMobile();

        }

        //��������� � ������ ���������� �� ������� � ����� ���������� ��� ����� � ������ ������
        private void ControlMobile() // ����� ���������� ����������
        {
            //���������� ���������
            var dir = m_VirtualGamepad.VirtualJoystik.Value;
            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = -dir.x;

            if (m_VirtualGamepad.MobileFireSecondory.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary); //������ �������� �������� ������
            }

            if (m_VirtualGamepad.MobileFireSecondory.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary); //������ �������� ��������������� ������
            }
        }

        private void ControlKeyboard()// ����� ���������� � ����������
        {
            float thrust = 0; //����
            float torque = 0; //������� ����


            if (Input.GetKey(KeyCode.UpArrow)) //������� �����
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow)) //������� ����
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow)) //������� �����
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow)) //������� ������
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary); //������ �������� �������� ������
            }

            if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary); //������ �������� ��������������� ������
            }

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}
