using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Transform m_targetTeleport;
        [SerializeField] private SpaceShip m_ship;

        [HideInInspector] public bool IsReceive = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.root.tag == "Player")
            {
                m_ship.GetComponentInChildren<TrailRenderer>().enabled = false;  
                other.transform.root.position = m_targetTeleport.transform.position;
                StartCoroutine(OnTrail());
            }
        }

        public void SetShip(SpaceShip ship)
        {
            this.m_ship = ship;
        }

        IEnumerator OnTrail()
        {
            yield return new WaitForSeconds(0.3f);
            m_ship.GetComponentInChildren<TrailRenderer>().enabled = true;
        }
    }
}
