using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 RotationDelta;

    private Transform m_Transform;

    private void Awake()
    {
        m_Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Transform.Rotate(RotationDelta);
    }
}
