using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTurnMove : MonoBehaviour
{
    float timeCounter = 0;

    public float speed;
    public float width;
    public float height;
    public float zVector;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = 4.44f + Mathf.Cos(timeCounter) * width;
        float y = 1.05f + Mathf.Sin(timeCounter) * height;
        float z = zVector;

        transform.position = new Vector3(x, y, z);
        //transform.rotation = new Vector3(x,y,?)
    }
}
