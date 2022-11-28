using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";
        // 
        [SerializeField] private float m_VelocityDamageModifier;

        [SerializeField] private float m_DamageConstant = 5;
        //[SerializeField]
        //private GameObject targetTeleport;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag)
            {
                return;
            }

            // Главыный объект рут

            if (tag == "Player")
            {
                var destructable = transform.root.GetComponent<Destructible>();

                if (destructable != null)
                {

                    destructable.ApplyDamage((int)m_DamageConstant +
                        (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                }
            }
        }
    }
}