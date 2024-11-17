using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private ObstacleSpawner spawner; // Spawner referansı
    private bool hasSpawned = false; // Yeni engel spawn edilip edilmediğini kontrol eden flag

    void Start()
    {
        // Spawner referansını bul ve ata
        spawner = FindObjectOfType<ObstacleSpawner>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !hasSpawned)
        {
            Debug.Log("Ball triggered obstacle");
            spawner.SpawnSingleObstacle(); // Yeni bir engel spawn et
            hasSpawned = true; // Yeni engel spawn edildiğini işaretle
        }
    }
}
