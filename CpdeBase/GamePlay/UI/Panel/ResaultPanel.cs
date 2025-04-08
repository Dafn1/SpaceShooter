using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResaultPanel : MonoBehaviour
    {
        private const string PassedText = "Passed";
        private const string NextLevel = "Next";
        private const string MainMenu = "Main menu";
        private const string Losse = "Lose";
        private const string Restart = "Restart";
        private const string Kill = "Kill: ";
        private const string Score = "Score: ";
        private const string Time = "Time: ";


        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Scpre;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;

        private bool m_LevelPassed=false;


        private void Start()
        {
            gameObject.SetActive(false);

            LevelController.Instance.LevelLost += OnLevelLost;
            LevelController.Instance.LevelPassed += OnLevelPassed;
        }

        private void OnDestroy()
        {
            LevelController.Instance.LevelLost -= OnLevelLost;
            LevelController.Instance.LevelPassed -= OnLevelPassed;
        }
        private void FillLevelsStatistics()
        {
            m_Kills.text= Kill + Player.Instance.NumKills.ToString();
            m_Scpre.text= Score + Player.Instance.Score.ToString();
            m_Time.text = Time + LevelController.Instance.Leveltime.ToString("F0") ;
        }

        private void OnLevelPassed()
        {
            gameObject.SetActive(true);

            m_LevelPassed=true;

            FillLevelsStatistics();

            m_Result.text = PassedText;

            if (LevelController.Instance.HesNextLevel == true)
            {
                m_ButtonNextText.text = NextLevel; 
            }
            else
            {
                m_ButtonNextText.text = MainMenu;
            }
        }

       

        private void OnLevelLost()
        {
            gameObject.SetActive(true);
            FillLevelsStatistics();

            m_Result.text = Losse;

            m_ButtonNextText.text = Restart;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive (false);

            if(m_LevelPassed == true)
            {
                LevelController.Instance.LoadNextLevel();
            
            }
            else
            {
                LevelController.Instance.RestartLevel();
            }
        }
    }
}

