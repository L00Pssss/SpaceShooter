using UnityEngine;
namespace SpaceShooter
{
    /// <summary>
    /// ������� ����� ���� ������������� ������� ��������.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// �������� ������ ��� ������������.
        /// </summary>
        [SerializeField]
        private string m_NickName;
        public string NickName => m_NickName;

    }
}
