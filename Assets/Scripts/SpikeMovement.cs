using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    public float hareketHizi = 1.0f; // Hareket Hýzý
    public float hareketMesafesi = 1.0f; // Hareket mesafesi
    private Rigidbody2D rb; // Rigidbody2D Bileþeni
    private Vector2 baslangicPozisyonu; // Baþlangýç pozisyonundaki X koordinatý

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true; //Engeli fizik motorundan baðýmsýz yap
        }

        baslangicPozisyonu = transform.position; // Objeyi oluþturduðumuz pozisyonu kaydet
    }

    // Update is called once per frame
    void Update()
    {
        // Pingpong hareketini Rigidbody2D ile saðla
        float yeniX = baslangicPozisyonu.x + Mathf.PingPong(Time.time * hareketHizi, hareketMesafesi * 2) - hareketMesafesi;
        rb.MovePosition(new Vector2(yeniX, transform.position.y));
    }
}
