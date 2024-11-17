using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    public float resoX;
    public float resoY;

    private CanvasScaler can;

    // Start is called before the first frame update
    void Start()
    {
        can = GetComponent<CanvasScaler>();
        setInfo();
    }

    void setInfo()
    {
        resoX = (float)Screen.currentResolution.width;
        resoY = (float)Screen.currentResolution.height;

        can.referenceResolution = new Vector2(resoX, resoY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
