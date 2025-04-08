using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour //������
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties; //������ �� ��������� ������

        private float m_RefireTimer; //������ ���������� ��������

        public bool CanFire => m_RefireTimer <= 0; //

        private SpaceShip m_Ship;

        //���������� ������

        private void Start() //����� ������ ������
        {
            m_Ship = transform.root.GetComponent<SpaceShip>(); //������ �� ��������� �������
        }

        private void Update() //�������� ������� �����
        {
            if(m_RefireTimer > 0) //�������� ������ � ��� ������,���� ������ ������ 0
            m_RefireTimer -= Time.deltaTime; //������
        }

        //����� ��������
        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return; //� ������� ���������� ������� ������� � ���������� ������� ������� ������� �����

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return; //� ������� ���������� ������� ������ � �������� �������� ������� �����

            Projectile proje�tile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>(); //����� ������ ��������� �������
            proje�tile.transform.position = transform.position;
            proje�tile.transform.up = transform.up;

            proje�tile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            {
                //�������� ����� ������
            }
        }
        
        public void AssignLoadout(TurretProperties props) //���������� ������� ������� ��� �������
        {
            if (m_Mode != props.Mode) return; //���� �� ����� �������� �������� ��� �������� ������
            m_RefireTimer = 0; //����� ������� ��� ������� ������
            m_TurretProperties = props;
        }
    }

}

