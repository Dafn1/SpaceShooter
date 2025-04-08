using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBilder : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject m_PalyerHUDPrefab;
        [SerializeField] private GameObject m_levelGUIPrefab;
        [SerializeField] private GameObject m_BeckgroundPrefab;

        [Header("Dependencies")]
        [SerializeField] private PlauerSpawner m_PlayerSpawner;
        [SerializeField] private LevelBoundary m_levelBoundary;
        [SerializeField] private LevelController m_levelController;

        private void Awake()
        {
            m_levelBoundary.Init();
            m_levelController.Init();


            Player player = m_PlayerSpawner.Spawn();
            player.Init();

            Instantiate(m_PalyerHUDPrefab);
            Instantiate(m_levelGUIPrefab);

            GameObject background = Instantiate(m_BeckgroundPrefab);
            background.AddComponent<SyncTransform>().SetTarget(player.FollowCamera.transform);

            SetSyncTarget(background);
        }

        private static void SetSyncTarget(GameObject background)
        {
            SyncTransform[] bg=background.GetComponentsInChildren<SyncTransform>();
            Transform targetSync=Camera.main.transform;

            for(int i=0; i<bg.Length; i++)
            {
                bg[i].SetTarget(targetSync);
            }
        }

    }
}

