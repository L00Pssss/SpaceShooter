using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    // интерфейс 
    public interface ILevelCondition
    {
        bool IsCopleted { get; }
    }
    public class LevelController : SingletonBase<LevelController>
    {


        // ¬рем€ прохождени€ за которое будет начисл€тьс€ очки
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;
        // уровень выполнен. 
        [SerializeField] private UnityEvent m_EventLevelCompleted;

        // масив с интерфейсами. 
        private ILevelCondition[] m_Conditions;
        // флаг прохождени€ 
        private bool m_IsLevelCompleted;

        // сколько прошло времени с начала уровн€. 
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;


        // Start is called before the first frame update
        void Start()
        {
            // ѕолучение интерфейсов. 
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;

                CheckLevevlConditions();
            }
        }

        // проверка сработали ли дочерние услови€. 
        private void CheckLevevlConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsCopleted)
                    numCompleted++;
            }

            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                m_EventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }
    }
}