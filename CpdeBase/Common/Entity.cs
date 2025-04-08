using UnityEngine;
namespace Common
{
    /// <summary>
    /// Базовый класс всех интерактивныйх игровых объектов на сцене
    /// </summary>
    public abstract class Entity : MonoBehaviour //чтобы код нельзя было добавить на объект
    {
        /// <summary>
        /// Назвние объекта для пользователя
        /// </summary>
        [SerializeField]//для отображение и регулирование в юнити
        private string m_Nickname;
        public string Nickname => m_Nickname;
    }

}
