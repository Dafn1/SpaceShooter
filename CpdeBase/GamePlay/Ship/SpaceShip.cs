using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible //Корабль и его свойства
    {

        [SerializeField] private Sprite m_PreviewImage;
        ///<sumary>
        ///Масса для автоматической установки у ригида
        ///</sumary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        

        ///<sumary>
        ///Толкающая вперел сила.
        ///</sumary>
        [SerializeField] private float m_Thrust;
        
        ///<sumary>
        ///Вращающая сила.
        ///</sumary>
        [SerializeField] private float m_Mobility;

        ///<sumary>
        ///Максимальная линейная скорость.
        ///</sumary>
        [SerializeField] private float m_MaxLinearVelocity;

        ///<sumary>
        ///Мфксимальная вращательная скорость. В градусах/сек
        ///</sumary>
        [SerializeField] private float m_MaxAngularVelocity;

        ///<sumary>
        ///Сохраненная ссылка на ригид
        ///</sumary>
        private Rigidbody2D m_Rigid;

        public float MaxLinerVelocity => m_MaxLinearVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;
        public Sprite PreviewImage => m_PreviewImage;


        #region Public API

        ///<sumary>
        ///Управление линейной тягой. -1.0 до +1.0
        ///</sumary>
        public float ThrustControl { get; set; }

        ///<sumary>
        ///Управление вращательной тягой. -1.0 до +1.0
        ///</sumary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass; //установка массы

            m_Rigid.inertia = 1; //чтобы проще было балансировать в соотношении сил

            InitOffensive();
        }

        private void FixedUpdate() //для правильного управление физикой корабля
        {
            UpdateRigidBody();

            UpdateEnergyRegen(); //регенерация энергии
        }
        #endregion

        /// <summary>
        /// Метод добавления сил корабля для движения
        /// </summary>
        private void UpdateRigidBody() 
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force); // тянуть корабль вперед по направлению

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force); // тянуть корабль назад по направлению

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force); //вращение корабля в одну сторону

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force); //вращение в противположную сторону
        }

        //свойства турели
        [SerializeField] private Turret[] m_Turrets;
        public void Fire(TurretMode mode) //стрелять из всех видов турели
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy; //максимальный показатель энергии
        [SerializeField] private int m_MaxAmmo;  //максимальный показатель патронов
        [SerializeField] private int m_EnergyRegenPerSecond; //регенерация энергии

        private float m_PrimaryEnergy; //текущий показатель энергии
        private int m_SecondaryAmmo; //текущий показатель патронов

        public void AddEnergy(int e) //добавление энергии
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy); //ограниченная энергия

            
        }

        public void AddAmmo(int ammo) //добавление патронов
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo); //ограничения патронов
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen() //автоматическая регенерация энергии
        {
            m_PrimaryEnergy += (float) m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy); //ограничения
        }

        public bool DrawEnergy(int count) //растрата энергии
        {
            if (count == 0) //если энергии равняется 0,то метод работает верно
                return true;

            if (m_PrimaryEnergy >= count) //если энергии больше или равно,то
            {
                m_PrimaryEnergy -= count; 
                return true;
            }
            return false;
        }

        public bool DrawAmmo(int count) //растрата патронов
        {
            if (count == 0) //если патронов равняется 0,то метод работает верно
                return true;

            if (m_SecondaryAmmo >= count) //если патронов больше или равно,то
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }

        public void AssignWeapon(TurretProperties props) //вооружение
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props); //подбирание бонусов
            }
        }
    }

}
