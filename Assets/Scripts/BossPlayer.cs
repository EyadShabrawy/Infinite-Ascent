using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float runSpeed = 40f;
    public float jumpForce = 400f;
    private float horizontalMove = 0f;
    private bool jump = false;
    
    [Header("References")]
    public CharacterController2D controller;
    
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    
    [Header("Animator")]
    public Animator animator;
    
    [Header("UI References")]
    public Button shootButton;
    
    private BossFightManager bossFightManager;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        bossFightManager = FindObjectOfType<BossFightManager>();
    }
    
    void FixedUpdate()
    {
        if (currentHealth <= 0) return;
        
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, jumpForce);
        
        if (jump)
        {
            jump = false;

            if (Mathf.Abs(horizontalMove) > 0.1f)
            {
                animator.CrossFade("walk", 0f);
            }
            else
            {
                animator.CrossFade("idle", 0f);
            }
            
        }
    }
    
    public void OnLeftButtonDown()
    {
        horizontalMove = -runSpeed;
        animator.CrossFade("walk", 0f);
    }
    
    public void OnLeftButtonUp()
    {
        if (horizontalMove < 0)
        {
            horizontalMove = 0;
            animator.CrossFade("idle", 0f);
        }
    }
    
    public void OnRightButtonDown()
    {
        horizontalMove = runSpeed;
        animator.CrossFade("walk", 0f);
    }
    
    public void OnRightButtonUp()
    {
        if (horizontalMove > 0)
        {
            horizontalMove = 0;
            animator.CrossFade("idle", 0f);
        }
    }
    
    public void OnJumpButtonDown()
    {
        jump = true;
        animator.CrossFade("fly", 0f);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            bossFightManager.PlayerDied();
            horizontalMove = 0;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
        }
        else if (collision.gameObject.CompareTag("BossBullet"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }
}
