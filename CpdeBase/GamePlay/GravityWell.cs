using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour //притягивание объекта
    {
        [SerializeField] private float m_Force; //сила притяжения
        [SerializeField] private float m_Radius; //радиус

        private void OnTriggerStay2D(Collider2D collision) // При попадании на территорию реагирования идет активация триггера
        {
            if (collision.attachedRigidbody == null) return; //у колизии есть ли риджетбоди

            Vector2 dir = transform.position - collision.transform.position; // притяжение к себе

            float dist = dir.magnitude; // дистанция равна расстоянию нашего напрвления

            if (dist < m_Radius) //если дистанция меньше,чем радиус, то
            {
                Vector2 force = dir.normalized * m_Force * (dist / m_Radius); //то мы притягиваем к себе предмет,чем ближе,тем сильнее притяжение
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force); //
            } 
        }

#if UNITY_EDITOR
        private void OnValidate() //оно происходит когда меняется значение
        {
            GetComponent<CircleCollider2D>().radius = m_Radius; //вызов радиуса соллайдера 2д
        }
#endif
    }

}
