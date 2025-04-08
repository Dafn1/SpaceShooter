using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SyncTransform : MonoBehaviour //фон,который будет двигаться вместе с объектом
    {
        [SerializeField] private Transform m_Target; //подключение к камере, для закрепления к ней

        void Update() //каждый кадр
        {
            if (!m_Target) return;
           transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z); //премещение
        }

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }
    }
}


