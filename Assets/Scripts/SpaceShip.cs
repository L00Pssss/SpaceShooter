using System.Collections;
using UnityEngine;


namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Масса для автоматической установки ригида.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Максимальная линейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;
        /// <summary>
        /// Максимальная вращательная скорость. В градусах/сек
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;



        /// <summary>
        /// Сохраненная ссылка на ригид.
        /// </summary>
        private Rigidbody2D m_Rigid;

        #region
        /// <summary>
        /// Управление линейно тягой. -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// Управление линейно тягой. -1.0 до +1.0
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
        /// Метод добавления сил кораблю для движения
        /// </summary>
        private void UpdateRigidBody()
        {
            // AddForce - Сила прикладывается непрерывно вдоль направления(Локально).
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up *
                Time.fixedDeltaTime, ForceMode2D.Force);

            // m_Rigid.velocity - вектор скорости. 
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) *
                Time.fixedDeltaTime, ForceMode2D.Force);

            //AddTorque - врaщение. 
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
            // Clamp - ограничить. 
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
