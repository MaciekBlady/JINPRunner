using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private Rigidbody2D m_Rigidbody2D;

    public Rigidbody2D Rigidbody => m_Rigidbody2D;

    [SerializeField]
    private Transform m_Feet;

    [SerializeField]
    private float m_MovementSpeed = 3f;

    [SerializeField]
    private float m_JumpForce = 500f;

    [SerializeField]
    private LayerMask m_GroundCheckLayer;

    [SerializeField]
    private float m_GroundCheckDistance;

    private bool m_Jump = false;

    private bool m_Crouch = false;

    private float m_Movement = 0f;

    private bool m_IsGrounded = false;

    private RaycastHit2D[] m_RaycastResults = new RaycastHit2D[1];

    private int m_SpeedAnimatorId = Animator.StringToHash("Speed");

    private int m_GroundAnimatorId = Animator.StringToHash("Ground");

    private int m_VerticalSpeedAnimatorId = Animator.StringToHash("vSpeed");

    private int m_CrouchAnimatorId = Animator.StringToHash("Crouch");

    private bool m_FacingRight = true;

    private bool m_IsDead = false;

    public event Action<PlayerController> OnPlayerKilled;

    void Update()
    {
        GetInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        m_IsGrounded = false;

        int hitsCount = Physics2D.RaycastNonAlloc(m_Feet.position, Vector2.down, m_RaycastResults, m_GroundCheckDistance, m_GroundCheckLayer);

        Debug.DrawLine(m_Feet.position, m_Feet.position + (Vector3.down * m_GroundCheckDistance), Color.red);

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

        float vertical = Input.GetAxis("Vertical");
        m_Crouch = vertical < 0f;
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

        if (m_Movement > 0f && !m_FacingRight)
        {
            FlipSprite();
        }
        else if (m_Movement < 0f && m_FacingRight)
        {
            FlipSprite();
        }
    }

    private void UpdateAnimator()
    {
        float absMovement = Mathf.Abs(m_Movement);

        m_Animator.SetFloat(m_SpeedAnimatorId, absMovement);
        m_Animator.SetBool(m_GroundAnimatorId, m_IsGrounded);
        m_Animator.SetFloat(m_VerticalSpeedAnimatorId, m_Rigidbody2D.velocity.y);
        m_Animator.SetBool(m_CrouchAnimatorId, m_Crouch);
    }

    private void FlipSprite()
    {
        m_FacingRight = !m_FacingRight;
        m_SpriteRenderer.flipX = !m_FacingRight;
    }

    public void Kill()
    {
        if (m_IsDead)
        {
            return;
        }

        m_IsDead = true;

        m_Movement = 0f;
        m_Jump = false;

        OnPlayerKilled?.Invoke(this);
    }
}
