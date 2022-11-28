using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class AIPointRay : MonoBehaviour
    {
        [SerializeField] private float m_EvadeRayLength;
        public float EvadeRayLength => m_EvadeRayLength;

        private static readonly Color GizmoColor = new Color(1, 0, 0, 1F);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            //вычисляю до куда должен быть вектор
            Gizmos.DrawLine(transform.position, transform.position + transform.up * EvadeRayLength);
        }






        //private void OnDrawGizmos()
        //{

        //}
    }
} 
