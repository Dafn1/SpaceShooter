using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ImpactEffect : MonoBehaviour //исчезновение снарядов
    {
        [SerializeField] private float m_Lifetime; //сколко он живет

        private float m_Timer; //скольок он прожил

        private void Update() //каждый кадр
        {
            if (m_Timer < m_Lifetime) //если он больше прожил меньше чем сами жизни
                m_Timer += Time.deltaTime;
            else
                Destroy(gameObject); //если он прожил больше,чем нужно,то он уничтожается
        }
    }

}

