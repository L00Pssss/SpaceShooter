using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        public SpaceShip ActiveShop => m_Ship;

        [SerializeField] private Teleport m_teleport;

        public SpaceShip ActiveShip => m_Ship;

        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
                Destroy(m_Ship.gameObject);
        }
        private void Start()
        {
            Respawn();
        }

        private void OnShopDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                m_teleport?.SetShip(m_Ship);

                m_Ship.EventOnDeath.AddListener(OnShopDeath);

                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);
            }
        }

        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int Number)
        {
            Score += Number;
        }

        #endregion
    }
}