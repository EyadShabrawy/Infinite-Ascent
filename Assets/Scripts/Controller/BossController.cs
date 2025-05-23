using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Boss Settings")]
    public Slider healthSlider;
    
    [Header("Boss Attack")]
    public GameObject bossBullet;
    public Transform firePoint;
    public float shootRate;
    private float nextShootTime = 0f;
    
    private BossFightManager bossFightManager;
    private int currentHealth;
    private int maxHealth = 100;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        bossFightManager = FindObjectOfType<BossFightManager>();
    }
    
    void Update()
    {
        if (currentHealth <= 0) return;
        
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + 1f / shootRate;
        }
    }
    
    void Shoot()
    {
        Instantiate(bossBullet, firePoint.position, firePoint.rotation);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            bossFightManager.BossDefeated();
            gameObject.SetActive(false);
        }
    }
}
