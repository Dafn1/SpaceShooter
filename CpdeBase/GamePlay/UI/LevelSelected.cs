using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LevelSelected : MonoBehaviour
    {

        [SerializeField] private LevelProperties m_LevelProperties;
        [SerializeField] private Text m_LevelTitle;
        [SerializeField] private Image m_PreviewImage;

        private void Start()
        {
            m_PreviewImage.sprite=m_LevelProperties.PreviewImage;
            m_LevelTitle.text = m_LevelProperties.Title;

        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(m_LevelProperties.ScenName);
        }

    }

}
