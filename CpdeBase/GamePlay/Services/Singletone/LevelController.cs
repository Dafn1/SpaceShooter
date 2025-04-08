using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelController : SingletoBase<LevelController>
    {
        private const string MainMenuSceneName = "Main menu";

        public event UnityAction LevelPassed;
        public event UnityAction LevelLost;

        [SerializeField] private LevelProperties m_LevelProperties;
        [SerializeField] private LevelCondition[] m_Condition;

        private bool m_IsLevelComplited;
        private float m_LevelTime;



        public bool HesNextLevel => m_LevelProperties.NextLevel != null;
        public float Leveltime => m_LevelTime;

        private void Start()
        {
            Time.timeScale = 1;
            m_LevelTime = 0;
        }

        private void Update()
        {

            if (m_IsLevelComplited == false)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }
         

            if (Player.Instance.NumLives == 0)
            {
                Lose();
            }
        }

        private void CheckLevelConditions()
        {
            int numCompleted = 0;

            for (int i = 0; i < m_Condition.Length; i++)
            {
                if (m_Condition[i].IsCompleted == true)
                {
                    numCompleted++;
                }
            }


            if (numCompleted == m_Condition.Length)
            {
                m_IsLevelComplited = true;

                Pass();

            }

        }

        private void Lose()
        {
            LevelLost?.Invoke();
            Time.timeScale = 0;
        }

        private void Pass()
        {
            LevelPassed?.Invoke();
            Time.timeScale = 0;
        }

        public void LoadNextLevel()
        {
            if (HesNextLevel == true)
            {
                SceneManager.LoadScene(m_LevelProperties.NextLevel.ScenName);
            }
            else
            {
                SceneManager.LoadScene(MainMenuSceneName);
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(m_LevelProperties.ScenName);
        }
    }

}
