using UnityEngine.UI;
using UnityEngine;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_bonus;

        [SerializeField] private Text m_Result;

        [SerializeField] private Text m_ButtontNextText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            if (levelResults.time > 1)
            {
                levelResults.bonus = 200;
            }

            m_Success = success;
            m_Kills.text = "Kills : " + levelResults.numKills.ToString();
            m_Score.text = "Score : " + levelResults.score.ToString();
            m_Time.text =  "Time : " + levelResults.time.ToString();
            m_bonus.text = "Bonus : " + levelResults.bonus.ToString();
            m_Result.text = success ? "WIin" : "Lose";  // если положительное, если отрецальное то 
            m_ButtontNextText.text = success ? "Next" : "Restart";

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}