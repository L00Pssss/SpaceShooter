using System.Security.Cryptography;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;

        [SerializeField] private float m_Lifetime;

        [SerializeField] private int m_Damage;

        [SerializeField] private ImpactEffect m_impactEffectPrefab;

        [SerializeField] private bool m_AOE;

        [SerializeField] private bool m_isHoming;

        [SerializeField] private float m_RadiusAOE;

        [SerializeField] private LayerMask whatAreDestructible;

        [SerializeField] private GameObject m_targetDestructible;

        private float m_Timer;

        private void Start()
        {
            m_targetDestructible = GameObject.FindGameObjectWithTag("Asteroid");
        }

        private void Update()
        {

            if (m_isHoming && m_targetDestructible != null)
            {
                transform.up = (m_targetDestructible.transform.position - transform.position).normalized;
            }

            float stelLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stelLength;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stelLength);

            if (hit && m_AOE == false)
            {
                Destructible destructible = hit.collider.transform.root.GetComponent<Destructible>();

                if (destructible != null && destructible != m_Parent)
                {
                    destructible.ApplyDamage(m_Damage);

                    if (m_Parent == Player.Instance.ActiveShop)
                    {
                        Player.Instance.AddScore(destructible.ScoreValue);
                    }
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            if (hit && m_AOE)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_RadiusAOE, whatAreDestructible);
                
                foreach (Collider2D list_Coliders in colliders)
                {
                    Destructible destructible = list_Coliders.transform.root.GetComponent<Destructible>();
                    if (destructible != null && destructible != m_Parent)
                    {
                        destructible.ApplyDamage(m_Damage);
                    }
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }


        private void OnProjectileLifeEnd(Collider2D collider, Vector2 position)
        {
            Destroy(gameObject);
        }

        private void AOE()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider c in colliders)
            {

                if (c.GetComponent<Asteroid>())
                {
                    Debug.Log("Rabotaet");
                    c.GetComponent<Destructible>().ApplyDamage(5);
                }

            }
        }


        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;  
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(this.transform.position, m_RadiusAOE);
        }

    }
}