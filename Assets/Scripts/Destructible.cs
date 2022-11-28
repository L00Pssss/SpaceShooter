using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То что может иметь хит поинты.  
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Indestrictible;
        public bool IsIndestructible => m_Indestrictible;

        /// <summary>
        /// Стартовое кол-во хитпоинтов. 
        /// </summary>
        [SerializeField] private int m_HitPoints;




        /// <summary>
        /// текущие хит поинты.
        /// </summary>
        [SerializeField] private int m_currentHitPoints;
        public int HitPoints => m_currentHitPoints;
        [SerializeField] private bool m_delayDestroyAnimation;
        [SerializeField] private bool m_playParticle;
        [SerializeField] private AnimationBase m_destroyAnimation;
        [SerializeField] private GameObject Partice;

        public int HitPoint
        {
            get { return m_currentHitPoints; }
            set { m_currentHitPoints = value; }

        }

        #endregion

        #region Unity Events
        //protected - для переопределения в дочерних классах. Виртуал что бы можнно было переопределить. 
        protected virtual void Start()
        {
            m_currentHitPoints = m_HitPoints;
        }


        #endregion

        #region Publick API
        /// <summary>
        /// Применение 
        /// </summary>
        /// <param name="damage">Урон наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestrictible) return;

            m_currentHitPoints -= damage;

            if (m_currentHitPoints <= 0)
                OnDeath();
        }



        #endregion
        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда хит поинты ниже нуля.
        /// </summary>
        protected virtual void OnDeath()
        {
            if (m_delayDestroyAnimation == true && m_playParticle == false)
            {
                Destroy(gameObject);
                m_EventOnDeath?.Invoke();
                return;
            }
            if (m_playParticle && m_delayDestroyAnimation)
            {
                Destroy(gameObject);
                m_EventOnDeath?.Invoke();
                Instantiate(Partice, transform.position, Quaternion.identity);
                return;

            }
            if (m_delayDestroyAnimation == false)
            {
                StartCoroutine(OnDeathAnimation());
                m_destroyAnimation?.StarAnimation();
                return;
            }

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
        IEnumerator OnDeathAnimation()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        [Header("0 = Neutaral")]
        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;

        public int TeamID
            {
                get { return m_TeamId; }
                set { m_TeamId = value; }
            }
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;


        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion
    }
}
