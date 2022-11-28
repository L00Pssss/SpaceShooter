using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpWeapon : PowerUp
    {
        [SerializeField] private TurretProperties m_Properties;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWepon(m_Properties);
        }
    }
}