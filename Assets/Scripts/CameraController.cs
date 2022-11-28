using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

    [SerializeField] private Transform m_Target;

    [SerializeField] private float m_InterpolationLiner;

    [SerializeField] private float m_InterpolationAngular;

    [SerializeField] private float m_CameraZ0ffset;

    [SerializeField] private float m_Forward0ffset;


    private void FixedUpdate()
    {
        if (m_Target == null || m_camera == null) return;

        Vector2 camPosition = m_camera.transform.position;
        Vector2 targetPostion = m_Target.position + m_Target.transform.up * m_Forward0ffset;
        Vector2 newCamPositon = Vector2.Lerp(camPosition, targetPostion, m_InterpolationLiner * Time.deltaTime);

        m_camera.transform.position = new Vector3(newCamPositon.x, newCamPositon.y, m_CameraZ0ffset);


        if (m_InterpolationAngular > 0)
        {
            m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, m_Target.rotation,
                                          m_InterpolationAngular * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        m_Target = newTarget;
    }
}
