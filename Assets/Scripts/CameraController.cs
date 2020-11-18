using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float m_SmoothTime = 1.0f;

    [SerializeField]
    private Vector3 m_Offset;

    private Vector3 m_Velocity;

    void Update()
    {
        if (m_Target == null)
        {
            return;
        }

        Vector3 targetPosition = m_Target.position + m_Offset;

        if (MathUtils.AreApproximatelyEqual(transform.position, targetPosition))
        {
            return;
        }

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref m_Velocity, m_SmoothTime);
        transform.position = smoothPosition;
    }
}