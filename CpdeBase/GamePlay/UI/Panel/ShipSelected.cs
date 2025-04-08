using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ShipSelected : MonoBehaviour
    {
        private const string HPBar = "HP: ";
        private const string Speed = "Speed: ";
        private const string Agility = "Agility: ";

        [SerializeField] private MainMenu m_MainMenu;
        [SerializeField] private SpaceShip m_Prefab;
        [SerializeField] private Text m_ShipName;
        [SerializeField] private Text m_HitPoints;
        [SerializeField] private Text m_Speed;
        [SerializeField] private Text m_Agility;
        [SerializeField] private Image m_Preview;

        private void Start()
        {
            if(m_Prefab == null)
            {
                return;
            }
            m_ShipName.text = m_Prefab.Nickname;
            m_HitPoints.text= HPBar+m_Prefab.MaxHitPoints.ToString();
            m_Speed.text= Speed+m_Prefab.MaxLinerVelocity.ToString();
            m_Agility.text= Agility+m_Prefab.MaxAngularVelocity.ToString();
            m_Preview.sprite = m_Prefab.PreviewImage;
            

        }

        public void SelectShip()
        {
            Player.SelectedSpaseShip = m_Prefab;
            m_MainMenu.ShowMainPanel();

        }


    }

}
