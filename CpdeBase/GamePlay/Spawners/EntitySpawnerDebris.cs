using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour //спавнер мусора
    {
        [SerializeField] private Destructible[] m_DebrisPrefabs; //список префабов которые мы можем заспавнить

        [SerializeField] private CircleArea m_Area; //зона в которой можно заспавнить мусор

        [SerializeField] private int m_NumDebris; //колличество мусора

        [SerializeField] private float m_RandomSpeed; //рандомная скорость мусора

        private void Start() // кадр в начале
        {
            for (int i = 0; i < m_NumDebris; i++) //спавниться общее количество мусора и если он уничтожается,
            {
                SpawnDebris(); // то спавнер спавнит еще мусор
            }
        }

        private void SpawnDebris() //спавнер мусора
        {
            int index = Random.Range(0, m_DebrisPrefabs.Length); //само количество

            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject); //спавн объектов

            debris.transform.position = m_Area.GetRandomInsideZone(); //позиция спавна в зоне
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>(); //изначальная скорость

            if (rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed; //рандомное передвижение по зоне с назанченной скоростью
            }
        }

        private void OnDebrisDead() //уничтожение мусора
        {
            SpawnDebris(); //при уничтожении объекта спавним мусор
        }
    }
}


