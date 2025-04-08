using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class Player : SingletoBase<Player> //свойства игрока
    {
        public static SpaceShip SelectedSpaseShip;

        [SerializeField] private int m_NumLives; //колличетсво жизней

        [SerializeField] private SpaceShip m_PlayerShipPrefab; //ссылка на префаб коробля
        public SpaceShip ActiveShip => m_Ship; //ссылка на текущий корабль

        private FollowCamera m_FollowCamera; //ссылка на фиксированную камеру на объекте
        private ShipInputController m_ShipInputController; //ссылка на управление компа и джойстика
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


        private void Start() //в начале кадра
        {
            Respawn();
        }

        private void OnShopDeath() //ыфполняется когда корабль погибает
        {
            m_NumLives--; //если корабль погибает,то жизни уменьшаются на 1
            m_Ship.EventOnDeath.RemoveListener(OnShopDeath);
            if (m_NumLives > 0)// если колличество жмзней больше нуля,то
            {
                Respawn();//возрождение
            }


        }

        private void Respawn() //возрождение корабля
        {
            var newPlayerShip = Instantiate(SpasePrefab, m_SpawnPoint.position, m_SpawnPoint.rotation); //создание нового корабля
            m_Ship = newPlayerShip.GetComponent<SpaceShip>(); //Вызов корабля

            m_FollowCamera.SetTarget(m_Ship.transform); //слежка за кораблем
            m_ShipInputController.SetTargetShip(m_Ship); //управление кораблем
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
