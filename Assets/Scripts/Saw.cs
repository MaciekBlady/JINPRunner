using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField]
    private float m_ForceModifier = 5000f;

    [SerializeField]
    private Vector3 m_Speed;

    private void Update()
    {
        transform.position += m_Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Kill();
            player.Rigidbody.AddForce(Vector3.up * m_ForceModifier);
        }
    }
}
