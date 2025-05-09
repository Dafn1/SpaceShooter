using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class Player : SingletoBase<Player> //�������� ������
    {
        public static SpaceShip SelectedSpaseShip;

        [SerializeField] private int m_NumLives; //����������� ������

        [SerializeField] private SpaceShip m_PlayerShipPrefab; //������ �� ������ �������
        public SpaceShip ActiveShip => m_Ship; //������ �� ������� �������

        private FollowCamera m_FollowCamera; //������ �� ������������� ������ �� �������
        private ShipInputController m_ShipInputController; //������ �� ���������� ����� � ���������
        private Transform m_SpawnPoint;

        public FollowCamera FollowCamera => m_FollowCamera;

        public void Construct(FollowCamera followCamera, ShipInputController shipInputController, Transform spawnPoint)
        {
            m_FollowCamera = followCamera;
            m_ShipInputController = shipInputController;
            m_SpawnPoint = spawnPoint;
        }

        private SpaceShip m_Ship;

        private int m_Score;
        private int m_NumKills;

        public int Score => m_Score;
        public int NumKills => m_NumKills;
        public int NumLives => m_NumLives;

        public SpaceShip SpasePrefab
        {
            get
            {
                if (SelectedSpaseShip == null)
                {
                    return m_PlayerShipPrefab;
                }
                else
                {
                    return SelectedSpaseShip;
                }
            }
        }


        private void Start() //� ������ �����
        {
            Respawn();
        }

        private void OnShopDeath() //����������� ����� ������� ��������
        {
            m_NumLives--; //���� ������� ��������,�� ����� ����������� �� 1
            m_Ship.EventOnDeath.RemoveListener(OnShopDeath);
            if (m_NumLives > 0)// ���� ����������� ������ ������ ����,��
            {
                Respawn();//�����������
            }


        }

        private void Respawn() //����������� �������
        {
            var newPlayerShip = Instantiate(SpasePrefab, m_SpawnPoint.position, m_SpawnPoint.rotation); //�������� ������ �������
            m_Ship = newPlayerShip.GetComponent<SpaceShip>(); //����� �������

            m_FollowCamera.SetTarget(m_Ship.transform); //������ �� ��������
            m_ShipInputController.SetTargetShip(m_Ship); //���������� ��������
            m_Ship.EventOnDeath.AddListener(OnShopDeath);
        }


        public void AddKill()
        {
            m_NumKills += 1;
        }

        public void AddScore(int num)
        {
            m_Score += num;
        }
    }
}
