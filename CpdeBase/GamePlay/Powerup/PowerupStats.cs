using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup //перечисление эффектов
    {
        public enum EffectType //перечисление эффектов для их выбора
        {
          AddAmmo, //патроны
          AddEnergy //энергия
        }
        [SerializeField] private EffectType m_EffectType; //свойство настраиваемое через редактор 

        [SerializeField] private float m_Value;
        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
                ship.AddEnergy((int) m_Value);

            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo((int) m_Value);
        }
    }

}

