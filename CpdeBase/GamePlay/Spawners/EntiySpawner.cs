using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class EntiySpawner : MonoBehaviour //спавн сущностей
    {
        public enum SpawnMode //перечисление
        {
            Start, //при старте
            Loop //переодический
        }
         
        [SerializeField] private Entity[] m_EntityPrefabs; //префаб который может заспавнить спавн

        [SerializeField] private CircleArea m_Area; //зона в которй можно заспавнить


        [SerializeField] private SpawnMode m_SpawnMode; //ссылка на спавн мод(наше перечисление)

        [SerializeField] private int m_NumSpawns; //колличетсво предметов которое может заспавниться

        [SerializeField] private float m_RespawnTime; //как часто надо сбрасывать таймер спавна

        private float m_Timer;

        private void Start() //при начальном кадре
        {
           if (m_SpawnMode == SpawnMode.Start) //если спавн мода равен старту ,то нужно заспавнить предметы и сбросить таймер
           {
                SpawnEntities(); //спанв предметов
           }
            m_Timer = m_RespawnTime;
        }

        private void Update() //каждый кадр
        {
           if (m_Timer > 0) // если у нас таймер больше нуля, то мы отнмаем таймер
                m_Timer -= Time.deltaTime;

           if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0) //если у нас мод равен зацикливанию и мы можем спавнить какойто объект,то мы спавним объект
           {
                SpawnEntities(); //спавн предметов

                m_Timer = m_RespawnTime; //обновляем тайер
           }
        }

        private void SpawnEntities() //позволяет заспавнить предметы
        {
           for (int i = 0; i < m_NumSpawns; i++) //цикл который будет спавнить предметы от нуля и до какогото количества предметов
           {
                int index = Random.Range(0, m_EntityPrefabs.Length); //само количество

                GameObject e = Instantiate(m_EntityPrefabs[index].gameObject); //спавн объектов

                e.transform.position = m_Area.GetRandomInsideZone(); //позиция в рамках зоны
           }
        }
    }

}

