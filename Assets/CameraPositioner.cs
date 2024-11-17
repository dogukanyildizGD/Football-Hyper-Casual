using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    public Transform ballTransform; // Topun transform'u
    private bool shouldFollow = false; // Kameranın topu takip edip etmeyeceğini kontrol eder
    private float initialYOffset; // Başlangıçta kamera ve top arasındaki y ekseni farkı

    void Start()
    {
        // Kamera ve top arasındaki başlangıç y ekseni farkını hesapla
        initialYOffset = transform.position.y - ballTransform.position.y;
    }

    void Update()
    {
        if (shouldFollow)
        {
            FollowBall();
        }
    }

    void FollowBall()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = ballTransform.position.y + initialYOffset; // Kameranın y eksenindeki hareketini top ile senkronize et
        transform.position = newPosition;
    }

    public void StopFollowing()
    {
        shouldFollow = false;
    }

    public void StartFollowing()
    {
        shouldFollow = true;
    }
}
