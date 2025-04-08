using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlauerSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private FollowCamera m_FollowCameraPrefab;
        [SerializeField] private Player m_PlayerPrtefab;
        [SerializeField] private ShipInputController m_ShipInputControllerPrefab;
        [SerializeField] private VirtualGamepad m_VirtualGamepadPrefab;


        [SerializeField] private Transform m_SpawnPoint;

        public Player Spawn()
        {
            FollowCamera followCamera = Instantiate(m_FollowCameraPrefab);
            VirtualGamepad virtualGamepad = Instantiate(m_VirtualGamepadPrefab);

            ShipInputController shipInputController = Instantiate(m_ShipInputControllerPrefab);
            shipInputController.Construct(virtualGamepad);

            Player player = Instantiate(m_PlayerPrtefab);
            player.Construct(followCamera, shipInputController, m_SpawnPoint);

            return player;

        }

    }
}

