using UnityEngine;

namespace SpaceShooter
{
    public class Finish_TR : MonoBehaviour
    {

        [SerializeField]
        private GameObject Panel;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Panel.SetActive(true);
        }
    }
}
