using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common
{
    public class CircleArea : MonoBehaviour //область спавна
    {
        [SerializeField] private float m_Radius; //свойства радиуса
        public float Radius => m_Radius;

        public Vector2 GetRandomInsideZone() //радномная точка внутри спавна
        {
            return (Vector2) transform.position + (Vector2) UnityEngine.Random.insideUnitSphere * m_Radius; //из трехмерного пространства становится двухмерным для точки рандома
        }
#if UNITY_EDITOR
        private static Color GizmoColor = new Color(0, 1, 0, 0.3f); //цвет зоны

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }

}


