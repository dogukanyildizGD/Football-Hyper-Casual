using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    public SpriteRenderer targetSize;

    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = targetSize.bounds.size.x / targetSize.bounds.size.y;

        if (screenRatio > targetRatio)
        {
            Camera.main.orthographicSize = targetSize.bounds.size.y / 2;
        }
        else
        {
            float diffrenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = targetSize.bounds.size.z / 2 * diffrenceInSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
