using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            
        }
    }
}
