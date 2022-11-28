using UnityEngine;
namespace SpaceShooter
{
    /// <summary>
    /// базовый класс всех интерактивних игровых объектов.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Название обїекта для пользователя.
        /// </summary>
        [SerializeField]
        private string m_NickName;
        public string NickName => m_NickName;

    }
}
