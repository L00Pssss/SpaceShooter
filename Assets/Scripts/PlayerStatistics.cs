using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics 
    {
        public int numKills;
        public int score;
        public int time;
        public int bonus;

        public void Reset()
        {
            numKills = 0;
            score = 0;
            time = 0;
            bonus = 0;
        }
    }
}