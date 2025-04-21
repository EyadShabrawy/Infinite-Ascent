using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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
    
    [Header("Fuel System")]
    public FuelManager fuelManager;
    private bool isFuelEmpty = false;
    
    private GameManager gameManager;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = normalGravity;
        
        gameManager = GameManager.Instance;
        
        fuelManager.onFuelEmpty.AddListener(OnFuelEmpty);
    }
    
    void Update()
    {
        if (fuelManager != null && fuelManager.IsFuelEmpty())
        {
            isFuelEmpty = true;
        }
        
        if (Input.touchCount > 0)
        {
            isTouching = !isFuelEmpty;
        }
        else
        {
            isTouching = false;
        }
    }
    
    void FixedUpdate()
    {
        if (isGameOver) return;
        
        Vector2 velocity = rb.velocity;
        
        if (isTouching)
        {
            velocity.y = jetpackForce;
            animator.SetBool("isJumping", true);
            
            fuelManager.ConsumeFuel(fuelManager.fuelConsumptionRate * Time.fixedDeltaTime);
        }
        else
        {
            velocity.y = Mathf.Max(velocity.y, -maxFallSpeed);
            animator.SetBool("isJumping", false);
        }
        
        rb.velocity = velocity;
    }
    
    [Header("Coin System")]
    public TextMeshProUGUI coinText;
    private int coinsCollected = 0;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            gameManager.ShowGameOver();
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            coinsCollected++;
            UpdateCoinText();
            Destroy(collision.gameObject);
        }
    }
    
    private void UpdateCoinText()
    {
        coinText.text = coinsCollected.ToString();
    }
    
    public int GetCoinsCollected()
    {
        return coinsCollected;
    }
    
    private void OnFuelEmpty()
    {
        isFuelEmpty = true;
    }
    
    private void OnDestroy()
    {
        fuelManager.onFuelEmpty.RemoveListener(OnFuelEmpty);
    }
}
