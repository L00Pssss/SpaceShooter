using SpaceShooter;
using UnityEngine;

public class AnimationPlayerKeyboard : MonoBehaviour
{
    [SerializeField] private KeyCode m_Key;

    [SerializeField] private AnimationBase m_Target;

    private void Update()
    {
        if (Input.GetKeyDown(m_Key))
        {
            m_Target.StarAnimation(true);
        }
    }

    public void GoAnimation()
    {
        m_Target.StarAnimation(true);
    }
}

