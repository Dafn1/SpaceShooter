using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundElement : MonoBehaviour //задний фон
    {
        [Range(0.0f, 4.0f)] 
        [SerializeField] private float m_ParallaxPower; //сила параллакс-эффекта

        [SerializeField] private float m_TextureScale; //“екстуры скейл

        private Material m_QuadMaterial; //ссылка на материал
        private Vector2 m_Initialoffset; //ссылка на изначальную точку ассета

        private void Start() //метод перед первым кадром или при активации объекта
        {
            m_QuadMaterial = GetComponent<MeshRenderer>().material; //получение ссылки на материал
            m_Initialoffset = UnityEngine.Random.insideUnitCircle; //генераци€ случайной точки в рамках единичной окружности

            m_QuadMaterial.mainTextureScale =  Vector2.one * m_TextureScale;
        }

        private void Update() //каждый кадр
        {
            Vector2 offset = m_Initialoffset; //вектор который равен изначальному ассету

            offset.x += transform.position.x / transform.localScale.x / m_ParallaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxPower;

            m_QuadMaterial.mainTextureOffset = offset;
        }
    }
}


