using UnityEngine;

namespace SpaceShooter
{
    [DisallowMultipleComponent]
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [Header("Singleton")]
        [SerializeField] private bool m_DoNotDestroyOnLoad;
        /// <summary>
        /// Singleton instance. May be null if DoNotDestroyOnLoad flag was not set.
        /// </summary>
        public static T Instance { get; private set; }


        /// <summary>
        /// Singleton instance. May be null if DoNotDestroyOnLoad flag was not set.
        /// </summary>
        /// 
        #region Unity events
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed = "
                    + typeof(T).Name);
                Destroy(this);
                return;
            }

            Instance = this as T;

            if (m_DoNotDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        #endregion
    }
}