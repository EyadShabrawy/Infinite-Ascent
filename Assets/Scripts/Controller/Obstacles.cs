using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject obstacle;
    public Transform spawnPoint;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;

    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        Vector3 spawnPosition = spawnPoint.position + new Vector3(x, y, 0);

        Instantiate(obstacle, spawnPosition, spawnPoint.rotation);
    }
}