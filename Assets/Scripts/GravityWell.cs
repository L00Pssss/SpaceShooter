using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SapceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [Header("Сила притягивания")]
        [SerializeField] private float m_Force; 
        [SerializeField] private float m_Radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            // attachedRigidbody = присоедененный риджедбади
            if (collision.attachedRigidbody == null) return;

            Vector2 direction = transform.position - collision.transform.position;

            float distance = direction.magnitude;

            if (distance < m_Radius)
            {
                Vector2 force = (distance / m_Radius) * m_Force * direction.normalized;
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }
        #if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
        #endif
    }
}