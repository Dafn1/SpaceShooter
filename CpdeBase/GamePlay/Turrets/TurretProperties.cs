using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public enum TurretMode //перечисления режимов,которые можно переключить
    {
        Primary, //главная 
        Secondary //вторичная
    }

    [CreateAssetMenu]

    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private Projectile m_ProjectilePrefab; //ссылка на префаб снаряда
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        [SerializeField] private float m_RateOfFire; //чем меньше скорость,тем сильнее стреляет
        public float RateOfFire => m_RateOfFire;

        [SerializeField] private int m_EnergyUsage; //скольок употребяет энергии
        public int EnergyUsage => m_EnergyUsage;

        [SerializeField] private int m_AmmoUsage; //скольок употребляет патронов
        public int AmmoUsage => m_AmmoUsage;

        [SerializeField] private AudioClip m_LaunchSFX; //ссылк ана аудио клип
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}

