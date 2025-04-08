using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace SpaceShooter
{
    public class ShipInputController : MonoBehaviour //управление комп и джойстик и кнопки
    {
        public enum ControlMode // выбор управлени€
        {
            Keyboard, //управление с клавиатуры
            Mobile // управление с джойстика
        }

        
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship; //дл€ упрощени€ напрвлени€ ссылки

      

        [SerializeField] private ControlMode m_ContolMode; //приватна€ переменаа€ котора€ спрашивает какое нам нужно управление

       

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
                m_VirtualGamepad.VirtualJoystik.gameObject.SetActive(false); //если включена клава,то убираем объект джойстик
               m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(false); //если включена клава,то убираем кнопку выстрела главного оружи€
                m_VirtualGamepad.MobileFireSecondory.gameObject.SetActive(false); //если включена клава,то выуключаем кнопку дополнительного выстрела
            }
            else
            {
                m_VirtualGamepad.VirtualJoystik.gameObject.SetActive(true); //если включен джойстик, то включаем объект джойстик
                m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(true); //если выкл клава,то включаем кнопку выстрела главного оружи€
                m_VirtualGamepad.MobileFireSecondory.gameObject.SetActive(true); //если выкл клава,то включаем кнопку дополнительного выстрела
            }
                
        }

        //ќсуществление управлени€
        private void Update()
        {
            if (m_TargetShip == null) return; //если корабль погиб,то выходим из метода

            if (m_ContolMode == ControlMode.Keyboard) //если выбрали управление клавы,то вызываем управление клавы
                ControlKeyboard();
            if (m_ContolMode == ControlMode.Mobile) // если выбрали джойстик,то вызываем управление клавы
                ControlMobile();

        }

        //обработка с какого устройства мы заходим и какое управление нам нужно в данный момент
        private void ControlMobile() // вызов управление джойстиком
        {
            //управление джойстика
            var dir = m_VirtualGamepad.VirtualJoystik.Value;
            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = -dir.x;

            if (m_VirtualGamepad.MobileFireSecondory.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary); //кнопка выстрела главного оружи€
            }

            if (m_VirtualGamepad.MobileFireSecondory.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary); //кнопка выстрела дополнительного оружи€
            }
        }

        private void ControlKeyboard()// вызов управление с клавиатуры
        {
            float thrust = 0; //т€га
            float torque = 0; //углова€ т€га


            if (Input.GetKey(KeyCode.UpArrow)) //стрелка вверх
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow)) //стрелка вниз
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow)) //стрелка влево
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow)) //стрелка вправо
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary); //кнопка выстрела главного оружи€
            }

            if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary); //кнопка выстрела дополнительного оружи€
            }

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}
