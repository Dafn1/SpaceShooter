using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_MainPanel;
        [SerializeField] private GameObject m_LvelSelectionPanel;
        [SerializeField] private GameObject m_ShipSelectionPanel;


        private void Start()
        {
            ShowMainPanel();
        }
        public void ShowMainPanel()
        {
            m_MainPanel.SetActive(true);
            m_ShipSelectionPanel.SetActive(false);
            m_LvelSelectionPanel.SetActive(false);
        }


        public void EX_ShowShipSelectionPanel()
        {
            m_ShipSelectionPanel.SetActive(true);
            m_MainPanel.SetActive(false);
        }

        public void EX_ShowLevelSilectionPanel()
        {
            m_LvelSelectionPanel.SetActive(true);
            m_MainPanel.SetActive(false);

        }

        public void EX_Quit()
        {
            Application.Quit();
        }
    }

}
