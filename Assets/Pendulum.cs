using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Rigidbody2D axeBody2d;
    public float leftPushRange;
    public float rightPushRange;
    public float velocityTrashold;

    // Start is called before the first frame update
    void Start()
    {
        axeBody2d = GetComponent<Rigidbody2D>();
        axeBody2d.angularVelocity = velocityTrashold;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }

    public void Push()
    {
        if (transform.rotation.z > 0 
            && transform.rotation.z < rightPushRange
            && axeBody2d.angularVelocity > 0
            && axeBody2d.angularVelocity < velocityTrashold)
        {
            axeBody2d.angularVelocity = velocityTrashold;
        }
        else if (transform.rotation.z < 0
            && transform.rotation.z > leftPushRange
            && axeBody2d.angularVelocity < 0
            && axeBody2d.angularVelocity > velocityTrashold * -1)
        {
            axeBody2d.angularVelocity = velocityTrashold * -1;
        }
    }
}
