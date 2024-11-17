using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject normalObstaclePrefab; // Normal engel prefab'ı
    public GameObject spikeObstaclePrefab; // Dikenli engel prefab'ı
    public GameObject background; // Arka plan objesi
    public int initialObstacleCount = 5; // Başlangıçta oluşturulacak engellerin sayısı
    public float obstacleSpacing = 2.0f; // Engeller arasındaki boşluk
    public float spikeObstacleXPosition = 1.0f; // Dikenli engel için X konumu (Inspector'dan ayarlanabilir)
    private List<GameObject> obstacles = new List<GameObject>(); // Mevcut engellerin listesi

    void Start()
    {
        float initialHeight = -obstacleSpacing; // İlk engel yükseklik olarak obstacleSpacing değerinde başlar
        for (int i = 0; i < initialObstacleCount; i++)
        {
            SpawnObstacle(initialHeight + i * obstacleSpacing);
        }
    }

    public void SpawnSingleObstacle()
    {
        Debug.Log("Spawning new obstacle");
        // En son engelin pozisyonunu al
        float lastObstacleY = obstacles.Count > 0 ? obstacles[obstacles.Count - 1].transform.position.y : 0f;

        // Yeni engelin pozisyonunu belirle
        float newObstacleY = lastObstacleY + obstacleSpacing;
        SpawnObstacle(newObstacleY);
    }

    private void SpawnObstacle(float yPosition)
    {
        float backgroundWidth = background.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        GameObject obstaclePrefab;
        Vector3 position;

        // Sağ/sol veya dikenli obje seçimi
        float randomValue = Random.value;
        if (randomValue < 0.66f)
        {
            // Sağda veya solda spawn olacak normal engel
            bool placeOnRight = Random.value > 0.5f;
            obstaclePrefab = normalObstaclePrefab;

            if (placeOnRight)
            {
                position = new Vector3(background.transform.position.x + backgroundWidth, yPosition, 0);
            }
            else
            {
                position = new Vector3(background.transform.position.x - backgroundWidth, yPosition, 0);
            }

            GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, transform);

            // Eğer sol tarafta ise, sadece instance'in scale değerini değiştir
            if (!placeOnRight)
            {
                obstacle.transform.localScale = new Vector3(-obstacle.transform.localScale.x, obstacle.transform.localScale.y, obstacle.transform.localScale.z);
            }

            // ObstacleTrigger scriptini engellere ekleyelim
            if (!obstacle.GetComponent<ObstacleTrigger>())
            {
                obstacle.AddComponent<ObstacleTrigger>();
            }

            // Engeli listeye ekle
            obstacles.Add(obstacle);
        }
        else
        {
            // Dikenli engel
            obstaclePrefab = spikeObstaclePrefab;
            position = new Vector3(spikeObstacleXPosition, yPosition, 0); // X konumu Inspector'dan ayarlanabilir

            GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, transform);

            // ObstacleTrigger scriptini engellere ekleyelim
            if (!obstacle.GetComponent<ObstacleTrigger>())
            {
                obstacle.AddComponent<ObstacleTrigger>();
            }

            // Dikenli engelin x ekseni üzerine hareket etmesi için SpiveMovement scriptini ekleyelim
            if (!obstacle.GetComponent<SpikeMovement>())
            {
                obstacle.AddComponent<SpikeMovement>();
            }

            // Engeli listeye ekle
            obstacles.Add(obstacle);
        }
    }
}
