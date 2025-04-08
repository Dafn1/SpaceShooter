using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LivePanel : MonoBehaviour
    {
        [SerializeField] private Text m_Text;
        [SerializeField] private Image m_Icon;

        private int lastLive;

        private void Start()
        {
            m_Icon.sprite = Player.Instance.ActiveShip.PreviewImage;
        }

        private void Update()
        {
            int live = Player.Instance.NumLives;

            if (lastLive != live)
            {
                m_Text.text = live.ToString();
                lastLive = live;
            }
        }
    }

}
