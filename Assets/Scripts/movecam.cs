using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecam : MonoBehaviour
{
    public float camSpeed;
    void Update()
    {
        transform.position+= new Vector3(camSpeed*Time.deltaTime,0,0);
    }
}
