using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Common
{
    /// <summary>
    /// Уничтожение объекта на сцене. То что может иметь хит поинты
    /// </summary>
    public class Destructible : Entity //дочерен от кода с наименованием
    {
        #region Properties 
        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Tndestructible;
        public bool IsIndestructible => m_Tndestructible;

        /// <summary>
        /// Стартовое кол-во хитпоинтов.
        /// </summary>
        [SerializeField] public int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;

        /// <summary>
        /// Текущие хит поинты
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints; //текущие жизни приравнять к стартовым
            transform.SetParent(null);
        }

        #endregion

        #region Public API
        /// <summary>
        /// Применение дамага к объекту.
        /// </summary>
        /// <param name="damage"> Урон наносимый объекту</param>
        public void ApplyDamage(int damage) // метод уничтожения
        {
            if (m_Tndestructible) return; //если объект не уничтожаемый,то выходим из метода
            m_CurrentHitPoints -= damage; //к текущим хитопоинтам уменьшаем за счет домага

            if (m_CurrentHitPoints <= 0) //если жизней меньше 0, то
                OnDeath(); // уничтожение объекта
        }


        #endregion
        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда хит поинты ниже нуля
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject); //уничтожение объекта
            m_EventOnDeath?.Invoke(); //активация
        }
        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath; //кнопка
        public UnityEvent EventOnDeath => m_EventOnDeath; //свойство через которое мы сможем до него достучаться

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        public object Velocity { get; internal set; }
    }
}