using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPreFab;
    public float spawnRatePerMinute = 10f;
    public float spawnRateIncrement = 1f;
    public float maxLife = 4f;

    private float xLimit = 8f;

    private float spawnNext = 0;

    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;//establecemos siguiente punto de spawn

            spawnRatePerMinute *= spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);

            Vector2 spawnPosition = new Vector2 (rand, 8f);

            GameObject meteor = Instantiate(asteroidPreFab, spawnPosition, Quaternion.identity);//giro ninguno

            Destroy(meteor, maxLife);
        }
    }
}
