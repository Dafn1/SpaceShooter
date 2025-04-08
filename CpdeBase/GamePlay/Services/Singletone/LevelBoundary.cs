using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletoBase<LevelBoundary> //ограничение уровня
    {
        [SerializeField] private float m_Radius; //ограничение
        public float Radius => m_Radius;

        public enum Mode //если корабль каснется ограничения
        {
            Limit, //ограничение движения
            Teleport //Телепортироваться
        }
        [SerializeField] private Mode m_LimitMode; //актулаьный режим ограничения
        public Mode LimitMode => m_LimitMode;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green; //цвет территории
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius); //рисуем диск территории
        }
#endif
    }
}


