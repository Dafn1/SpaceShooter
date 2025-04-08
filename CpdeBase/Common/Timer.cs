using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Timer : MonoBehaviour
    {
        private float m_CurrentTime;

        public bool IsFinished => m_CurrentTime <= 0;

        public Timer(float startTime)
        {
            StartTimer(startTime);
        }

        public void StartTimer(float startTime)
        {
            m_CurrentTime = startTime;


        }

        public void RemoveTime(float deltaTime)
        {
            if (m_CurrentTime <= 0) return;

            m_CurrentTime -= deltaTime;
        }
    }

}
