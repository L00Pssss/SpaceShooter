using UnityEngine;

namespace SpaceShooter {
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_mode;
        public TurretMode Mode => m_mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_Refire_Timer;

        public bool CanFire => m_Refire_Timer <= 0;

        private SpaceShip m_ship;

        private void Start()
        {
            m_ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (m_Refire_Timer > 0)
            m_Refire_Timer -= Time.deltaTime;
        }

        // Public API

        public void Fire()
        {
            if (m_TurretProperties == null) 
                return;

            if (m_Refire_Timer > 0) 
                return;

            if (m_ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) { return; }

            if (m_ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) { return; }

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).
                                    GetComponent<Projectile>();

            projectile.transform.position = transform.position;
            //врощение, что бы с головы стрелял 
            projectile.transform.up = transform.up;

            projectile.SetParentShooter(m_ship);


            m_Refire_Timer = m_TurretProperties.RateOfFire;

            {
                // SFX
            }
        }

        public void AssignLoadount(TurretProperties props)
        {
            if (m_mode != props.Mode) return;

            m_Refire_Timer = 0;
            m_TurretProperties = props;
        }

    }
}