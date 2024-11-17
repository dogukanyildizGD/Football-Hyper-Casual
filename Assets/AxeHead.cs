using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHead : MonoBehaviour
{
    private EdgeCollider2D edgeCollider2;

    // Start is called before the first frame update
    void Start()
    {
        edgeCollider2 = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            edgeCollider2.isTrigger = false;
            Destroy(collision.gameObject);
        }
    }
}
