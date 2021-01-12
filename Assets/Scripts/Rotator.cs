using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 RotationDelta;

    private Transform m_Transform;

    private void Awake()
    {
        m_Transform = GetComponent<Transform>();
    }

    void Update()
    {
        m_Transform.Rotate(RotationDelta * Time.deltaTime);
    }
}
