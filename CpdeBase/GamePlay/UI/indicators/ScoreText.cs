using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private int lastScoteText;

        private void Update()
        {
            int score = Player.Instance.Score;

            if (lastScoteText != score)
            {
                m_Text.text ="Score : "+score.ToString();
                lastScoteText = score;
            }
        }
    }

}
