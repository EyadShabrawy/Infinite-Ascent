using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    private float runSpeed;
    private float jumpForce;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool canJump = true;
    
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
    
    private UserManager userManager;
    
    void Start()
    {
        userManager = UserManager.Instance;
        SetStatsBasedOnLevel();
        
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        bossFightManager = FindObjectOfType<BossFightManager>();
    }
    
    void SetStatsBasedOnLevel()
    {
        switch (userManager.SpeedLevel)
        {
            case 1:
                runSpeed = 15f;
                jumpForce = 400f;
                break;
            case 2:
                runSpeed = 20f;
                jumpForce = 450f;
                break;
            case 3:
                runSpeed = 25f;
                jumpForce = 500f;
                break;
            case 4:
                runSpeed = 30f;
                jumpForce = 550f;
                break;
            case 5:
                runSpeed = 35f;
                jumpForce = 600f;
                break;
            default:
                runSpeed = 15f;
                jumpForce = 400f;
                break;
        }
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
        if (canJump)
        {
            jump = true;
            canJump = false;
            animator.CrossFade("fly", 0f);
        }
    }
    
    public void TakeDamage()
    {
        int adjustedDamage;
        switch (userManager.ArmorLevel)
        {
            case 1:
                adjustedDamage = 50;
                break;
            case 2:
                adjustedDamage = 40;
                break;
            case 3:
                adjustedDamage = 30;
                break;
            case 4:
                adjustedDamage = 20;
                break;
            case 5:
                adjustedDamage = 10;
                break;
            default:
                adjustedDamage = 50;
                break;
        }
        
        currentHealth -= adjustedDamage;
        
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
            TakeDamage();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
