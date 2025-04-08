using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour //турель
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties; //ссылка на экземпляр класса

        private float m_RefireTimer; //таймер повторного выстрела

        public bool CanFire => m_RefireTimer <= 0; //

        private SpaceShip m_Ship;

        //функционал турели

        private void Start() //перед первым кадром
        {
            m_Ship = transform.root.GetComponent<SpaceShip>(); //ссылка на компонент корабль
        }

        private void Update() //проверка каждого кадра
        {
            if(m_RefireTimer > 0) //отнимать таймер в том случае,если таймер больше 0
            m_RefireTimer -= Time.deltaTime; //таймер
        }

        //метод стрельбы
        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return; //у коробля вызывается отнятие энергии и отнимается энергии столько сколько нужно

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return; //у коробля вызывается отнятие патрон и отнимает патронов скольок нужно

            Projectile projeсtile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>(); //будет создан компонент префаба
            projeсtile.transform.position = transform.position;
            projeсtile.transform.up = transform.up;

            projeсtile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            {
                //добавить аудио ресурс
            }
        }
        
        public void AssignLoadout(TurretProperties props) //назначение какихто свойств или бонусов
        {
            if (m_Mode != props.Mode) return; //если мы хотим положить свойства для главного оружия
            m_RefireTimer = 0; //сброс таймера при подборе орудия
            m_TurretProperties = props;
        }
    }

}

