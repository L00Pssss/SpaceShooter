using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics {get;set; }

        public static SpaceShip PlayerShip {get; set; }

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            // сбрасываем статы перед началом эпизода
            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();
            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        public void FinishCurrentLevel(bool success)
        {
           LastLevelResult = success;
           CalculateLevelStatistic();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);

        }

        private void CalculateLevelStatistic()
        {
            LevelStatistics.score = Player.Instance.Score;
            LevelStatistics.numKills = Player.Instance.NumKills;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
        }
    }

}