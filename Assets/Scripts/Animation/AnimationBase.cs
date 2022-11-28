using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ������� ����� ���������
    /// </summary>
    public abstract class AnimationBase : MonoBehaviour
    {
        /// <summary>
        /// ������ ����� ��������.
        /// </summary>
        [SerializeField] protected float m_AnimationTime;

        /// <summary>
        /// ������ ����� �������� � ������ ���������.
        /// </summary>
        public float AnimationTime => m_AnimationTime / m_AnimationScale;

        /// <summary>
        /// �������� ������� ��������.
        /// </summary>
        [SerializeField] protected float m_AnimationScale;

        /// <summary>
        /// ���� ������������� ��������.
        /// </summary>
        [SerializeField] private bool m_Looping;


        public void SetAnimationScale(float scale)
        {
            m_AnimationScale = scale;
        }


        /// <summary>
        /// ����������������� ����� ��������
        /// </summary>
        public float NormolizedAnimationTime
        {
            get
            {
                return Mathf.Clamp01(m_Timer / AnimationTime);
            }
        }

        [SerializeField] private UnityEvent m_EventStart;
        [SerializeField] private UnityEvent m_EventEnd;
        public UnityEvent OnEventEnd => m_EventEnd;

        private float m_Timer;
        private bool m_InAnimationPlaying;

        #region Unity events
        private void Update()
        {
            if (m_InAnimationPlaying)
            {
                m_Timer += Time.deltaTime;

                AnimateFrame();

                if (m_Timer > AnimationTime)
                {
                    if (m_Looping)
                    {
                        m_Timer = 0;
                    }
                    else
                    {
                        StopAnimation();
                    }
                }
            }
        }
        #endregion

        #region Public API
        /// <summary>
        /// ����� ������� ��������.
        /// </summary>
        public void StarAnimation(bool prepare = true)
        {
            if (m_InAnimationPlaying)
                return;
            if (prepare)
                PrepareAnimation();
            m_InAnimationPlaying = true;

            OnAnimationStart();

            m_EventStart?.Invoke();

        }
        /// <summary>
        /// ����� ����������� ��������.
        /// </summary>
        public void StopAnimation()
        {
            if (!m_InAnimationPlaying)
                return;

            m_InAnimationPlaying = false;

            OnAnimationEnd();

            m_EventEnd?.Invoke();

        }
        #endregion

        /// <summary>
        /// ��������� ������� ����� ��������.
        /// </summary>
        protected abstract void AnimateFrame();

        protected abstract void OnAnimationStart();

        protected abstract void OnAnimationEnd();

        /// <summary>
        /// ���������� ���������� ��������� ���������.
        /// </summary>
        public abstract void PrepareAnimation();
    }
}
