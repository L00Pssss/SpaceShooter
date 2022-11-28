using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class EpisodeSelectionController : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;
        [SerializeField] private Text m_EpisodeNickName;
        [SerializeField] private Image m_PerviewImage;

        private void Start()
        {
            if (m_EpisodeNickName != null)
                m_EpisodeNickName.text = m_Episode.EpisodeName;
            if (m_PerviewImage != null)
                m_PerviewImage.sprite = m_Episode.PreviewImage;
        }

        public void OnStartEpisodeButtonClicked()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }
    }
}