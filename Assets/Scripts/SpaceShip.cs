using System.Collections;
using UnityEngine;


namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// ����� ��� �������������� ��������� ������.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// ��������� ������ ����.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;
        /// <summary>
        /// ������������ ������������ ��������. � ��������/���
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;



        /// <summary>
        /// ����������� ������ �� �����.
        /// </summary>
        private Rigidbody2D m_Rigid;

        #region
        /// <summary>
        /// ���������� ������� �����. -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// ���������� ������� �����. -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }




        [SerializeField] private TrailRenderer Trail;

        public TrailRenderer trail => Trail;

        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            IntitOffensive();
        }
        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }
        #endregion

        /// <summary>
        /// ����� ���������� ��� ������� ��� ��������
        /// </summary>
        private void UpdateRigidBody()
        {
            // AddForce - ���� �������������� ���������� ����� �����������(��������).
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up *
                Time.fixedDeltaTime, ForceMode2D.Force);

            // m_Rigid.velocity - ������ ��������. 
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) *
                Time.fixedDeltaTime, ForceMode2D.Force);

            //AddTorque - ��a�����. 
            m_Rigid.AddTorque(TorqueControl * m_Mobility *
                Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity *
                (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        public void AddEnergy(int energy)
        {
            // Clamp - ����������. 
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        private void IntitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }

        public void AssignWepon(TurretProperties properties)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadount(properties);
            }
        }

        IEnumerator RevertHp()
        {
            yield return new WaitForSeconds(5f);
            HitPoint = 100;
        }

        IEnumerator RevertSpeed()
        {
            yield return new WaitForSeconds(3f);
            m_Thrust = 1000;
            m_MaxLinearVelocity = 15;
        }


        public void HealthDown()
        {
            HitPoint = 1;
            StartCoroutine(RevertHp());
        }
        public void PowerupSpeed(int thrust, int liner)
        {

            m_Thrust = thrust;
            m_MaxLinearVelocity = liner;
            StartCoroutine(RevertSpeed());
        }


    }

}
