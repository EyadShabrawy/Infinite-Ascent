using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float jetpackForce = 10f;
    private Rigidbody2D rb;
    private bool isTouching = false;
    private bool isGameOver = false;
   
    [Header("Animator")]
    public Animator animator; 
    [Header("Gravity Settings")]
    public float normalGravity = 2f;
    public float maxFallSpeed = 10f;
    
    [Header("UI References")]
    public Button shootButton;
    
    private GameManager gameManager;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = normalGravity;
        
        gameManager = GameManager.Instance;
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (shootButton != null && RectTransformUtility.RectangleContainsScreenPoint(
                shootButton.GetComponent<RectTransform>(), touch.position, null))
            {
                isTouching = false;
            }
            else
            {
                isTouching = true;
            }
        }
        else
        {
            isTouching = false;
        }
    }
    
    void FixedUpdate()
    {
        // Don't apply physics if game is over
        if (isGameOver) return;
        
        Vector2 velocity = rb.velocity;
        
        if (isTouching)
        {
            velocity.y = jetpackForce;
            animator.SetBool("isJumping", true);
        }
        else
        {
            velocity.y = Mathf.Max(velocity.y, -maxFallSpeed);
            animator.SetBool("isJumping", false);
        }
        
        rb.velocity = velocity;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
        isGameOver = true;
        gameManager.ShowGameOver();
        }
    }
}
