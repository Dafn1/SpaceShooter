using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Ограничитель позиции.Работает в связке со скриптом LevelBoundary если таковой имеется на сцене
    /// Кидается на объекте который надо ограничить
    /// </summary>

    public class LevelBoundaryLimiter : MonoBehaviour //ограничивающая стена
    {
        private void Update() //каждый кадр
        {
            if (LevelBoundary.Instance == null) return;
            var lb = LevelBoundary.Instance; //ссылка на левел
            var r = lb.Radius; //ссылка на радиус

            if (transform.position.magnitude > r) //если позиция больше радиуса,то  какой вид стоит
            {
                if (lb.LimitMode == LevelBoundary.Mode.Limit)   //вид ограничения в виде ограничения
                {
                    transform.position = transform.position.normalized * r;
                }

                if (lb.LimitMode == LevelBoundary.Mode.Teleport) // вид ограничения в виде телепора
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
    }
}

