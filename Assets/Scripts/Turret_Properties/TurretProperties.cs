using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary,
        Secondary
    }
    [CreateAssetMenu]
    /// sealed - нельзя наследовать класс.
    public sealed class TurretProperties : ScriptableObject
    {
        /// <summary>
        /// какая турель 
        /// </summary>
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;
        /// <summary>
        /// Снаряд, ссылка на снаряд префаб
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;
        /// <summary>
        /// скорострельность.
        /// <summary>
        /// скорострельность.
        /// </summary>
        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;
        /// <summary>
        /// энергия затраты. 
        /// </summary>
        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;
        /// <summary>
        /// Потроны сколько тратиться и т.д.
        /// </summary>
        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;
        /// <summary>
        /// Ссылка на звук, звук снаряда к пирмеру. 
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;

    }
}
