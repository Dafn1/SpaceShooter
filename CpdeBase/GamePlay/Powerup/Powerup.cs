using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(CircleCollider2D))] //компонент с колайдкром для 2д материалов
    public abstract class Powerup : MonoBehaviour //подбираемые бонусы
    {
        private void OnTriggerEnter2D(Collider2D collision) //триггериться при прикосновении корабля с ним
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>(); //вызов компонента корабль

            if (ship != null && Player.Instance.ActiveShip) //если с объектом столкнулся сам корабль
            {
                OnPickedUp(ship); //вызов метода подбора предмета

                Destroy(gameObject); //то мы уничтожаем бонус
            }
        }

        protected abstract void OnPickedUp(SpaceShip ship); //предмет подобран и каким кораблем он подобран
       

      

    }

}

