using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class HitPoitBar : MonoBehaviour
    {
        [SerializeField] private Image m_Image;

        private float lastHitPoint;
        private void Update()
        {
            float hitPoints=((float)Player.Instance.ActiveShip.HitPoints/ (float) Player.Instance.ActiveShip.MaxHitPoints);

            if (hitPoints != lastHitPoint)
            {
                m_Image.fillAmount = hitPoints;
                lastHitPoint=hitPoints;
            }
        }
    }

}
