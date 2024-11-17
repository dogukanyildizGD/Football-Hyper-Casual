using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EskiSoccerBall : MonoBehaviour
{
    public Rigidbody2D rbBall;
    public Rigidbody2D hookBall;
    public GameObject hookBallGO;
    public GameObject rbBallGO;
    private CircleCollider2D rbBallCC2D;

    public float maxDragDistance = 2f;
    private bool isPressed = false;
    public float releaseTime = .15f;
    public float releaseDelayTime = .15f;

    private bool moveCheckBool = true;

    private SpringJoint2D springJoint2D;

    //LineRenderer
    public LineRenderer lineRenderer;

    public GameObject coinNumPrefab;

    CoinsManager coinsManager;

    void Start()
    {
        lineRenderer.enabled = false;

        coinsManager = FindObjectOfType<CoinsManager>();
        rbBallCC2D = GetComponent<CircleCollider2D>();
        springJoint2D = GetComponent<SpringJoint2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && moveCheckBool)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    isPressed = true;
                    rbBall.isKinematic = true;
                    Pointer();
                    lineRenderer.enabled = true;
                    //rbBallCC2D.enabled = false;                    
                    break;

                case TouchPhase.Ended:
                    isPressed = false;
                    rbBall.isKinematic = false;
                    StartCoroutine(Release());
                    lineRenderer.enabled = false;
                    if (rbBallGO.transform.position.y <= hookBallGO.transform.position.y)
                    {
                        StartCoroutine(ReleaseDelay());
                        Debug.Log("Gecikmeli!");
                    }
                    else
                    {
                        springJoint2D.frequency = 2;
                        //rbBallCC2D.enabled = true;
                        Debug.Log("Gecikmesiz!");
                    }
                    break;
            }

            if (isPressed)
            {
                if (Vector3.Distance(touchPosition, hookBall.position) > maxDragDistance)
                {
                    rbBall.position = hookBall.position + (touchPosition - hookBall.position).normalized * maxDragDistance;
                }
                else
                {
                    rbBall.position = touchPosition;
                }
            }
        }

        if (rbBall.IsSleeping())
        {
            moveCheckBool = true;
            hookBallGO.SetActive(true);
            GetComponent<SpringJoint2D>().enabled = true;
            hookBallGO.GetComponent<Transform>().position = transform.position;
        }
    }

    IEnumerator ReleaseDelay()
    {
        springJoint2D.frequency = 1.5f;
        yield return new WaitForSeconds(releaseDelayTime);
        //rbBallCC2D.enabled = true;
    }

    void Pointer()
    {
        lineRenderer.SetPosition(0, hookBallGO.transform.position);
        lineRenderer.SetPosition(1, rbBallGO.transform.position);
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;

        moveCheckBool = false;

        hookBallGO.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            coinsManager.AddCoins(collision.transform.position, 7);

            Destroy(collision.gameObject);

            Destroy(Instantiate(coinNumPrefab, collision.transform.position, Quaternion.identity), 1f);
            Debug.Log("Coin");
        }

        if (collision.CompareTag("Ground"))
        {
            //collision.isTrigger = true;
            Debug.Log("Ground");
        }
    }
}
