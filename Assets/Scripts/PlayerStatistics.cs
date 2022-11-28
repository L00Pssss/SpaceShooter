using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int numKills;
        public int score;
        public int time;

        public void Reset()
        {
            numKills = 0;
            score = 0;
            time = 0;
        }
    }
}