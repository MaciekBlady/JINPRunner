using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private Rigidbody2D m_Rigidbody2D;

    [SerializeField]
    private float m_MovementSpeed = 3f;

    [SerializeField]
    private float m_JumpForce = 500f;

    [SerializeField]
    private LayerMask m_GroundCheckLayer;

    [SerializeField]
    private float m_GroundCheckDistance;

    private bool m_Jump = false;

    private float m_Movement = 0f;

    private bool m_IsGrounded = false;

    private RaycastHit2D[] m_RaycastResults = new RaycastHit2D[1];

    private int m_SpeedAnimatorId = Animator.StringToHash("Speed");

    private int m_GroundAnimatorId = Animator.StringToHash("Ground");

    void Update()
    {
        GetInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        m_IsGrounded = false;

        int hitsCount = Physics2D.RaycastNonAlloc(transform.position, Vector2.down, m_RaycastResults, m_GroundCheckDistance, m_GroundCheckLayer);

        for (int i = 0; i < hitsCount; i++)
        {
            RaycastHit2D raycastHit = m_RaycastResults[i];
            if (raycastHit.collider != null)
            {
                m_IsGrounded = true;
            }
        }

        UpdateMovement();
    }

    private void GetInput()
    {
        m_Movement = Input.GetAxis("Horizontal") * m_MovementSpeed;

        if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }
    }

    private void UpdateMovement()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Movement, m_Rigidbody2D.velocity.y);

        if (m_Jump && m_IsGrounded)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

            m_IsGrounded = false;
            m_Jump = false;
        }
    }

    private void UpdateAnimator()
    {
        float absMovement = Mathf.Abs(m_Movement);

        m_Animator.SetFloat(m_SpeedAnimatorId, absMovement);
        m_Animator.SetBool(m_GroundAnimatorId, m_IsGrounded);
    }
}
