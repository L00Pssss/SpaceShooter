using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Ограничитель позиции. Работает в связке со скриптом LevelBoundary если таковой имеется на сцене.
    /// Кидается на объект который надо ограничить.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var levelBoundary = LevelBoundary.Instance;
            var radius = levelBoundary.Radius;

            if (transform.position.magnitude > radius)
            {
                if (levelBoundary.LimitMode == LevelBoundary.Mode.Limit)
                {
                    if (tag == "Player")
                    {
                        transform.position = transform.position.normalized * radius;
                    }
                    if (tag == "Asteroid")
                    {
                        transform.position = transform.position.normalized * radius;
                    }
                }

                if (levelBoundary.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * radius;
                }
            }


        }
    }
}
