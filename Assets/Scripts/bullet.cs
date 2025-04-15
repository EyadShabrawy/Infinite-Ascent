using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class bullet : MonoBehaviour
{
    public Rigidbody2D RB;
    public float speed;
    public GameObject bulletEffect;
 
    void Start()
    {
        RB.velocity = transform.right * speed;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }
}