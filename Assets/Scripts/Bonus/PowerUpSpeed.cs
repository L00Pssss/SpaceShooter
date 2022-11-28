using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpSpeed : PowerUp
    {
        [SerializeField] private int thrust;
        [SerializeField] private int liner;
        protected override void OnPickedUp(SpaceShip ship)
        {

            ship.PowerupSpeed(thrust, liner);
        }
    }
}