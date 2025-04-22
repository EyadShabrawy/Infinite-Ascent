using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

public void Move(float move, bool jump, float jumpForce)
{
    Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
    m_Rigidbody2D.velocity = targetVelocity;

        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
        
        if (jump)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
