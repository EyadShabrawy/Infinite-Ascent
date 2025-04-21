using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb.velocity = -transform.right * speed;
        
        Destroy(gameObject, 3f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
}
