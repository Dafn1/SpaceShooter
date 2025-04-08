using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour //урон при прикосновениик предмету
    {
        public static string IgnoreTag = "WorldBoundary"; //чтобы не было урона от невидимых стен

        [SerializeField] private float m_VelocityDamageModifier; //скорость

        [SerializeField] private float m_DamageConstant; //урон

        private void OnCollisionEnter2D(Collision2D collision) //если мы столкнулись с объектом,есть ли компонент уничтожения
        {
            if(collision.transform.tag == IgnoreTag) return; //если мы столкнулись с объектом где есть игнор тек,то урона не будет

            var desrtructable = transform.root.GetComponent<Destructible>(); //если ли уничтожение

            if (desrtructable != null)
            {
                desrtructable.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude)); //чем юольше скорость тем сильнее урон
            }
        }
    }
}


