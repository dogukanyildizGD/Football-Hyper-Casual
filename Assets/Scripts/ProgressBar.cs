using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public float maximum;
    public float current;
    public Image mask;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();               
    }

    private void FixedUpdate()
    {
        if (current <= maximum)
        {
            current += Time.deltaTime;
            timerText.text = Mathf.Floor(current).ToString();
        }
        else
        {

        }
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;

        mask.fillAmount = fillAmount;
    }
}
