using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform throwpoint;
    public void Shoot()
    {
        Instantiate(bullet, throwpoint.position, throwpoint.rotation);
    }
    
}
