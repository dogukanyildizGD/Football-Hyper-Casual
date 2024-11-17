using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoccerBall : MonoBehaviour
{
    // Önceki değişkenler
    public Rigidbody2D rbBall;
    public Rigidbody2D hookBall;
    public GameObject hookBallGO;
    public GameObject rbBallGO;
    private CircleCollider2D rbBallCC2D;
    public CameraPositioner cameraPositioner;
    public float maxDragDistance = 2f;
    private bool isPressed = false;
    public float releaseTime = .15f;
    public float releaseDelayTime = .15f;
    private bool moveCheckBool = true;
    private bool isOnSurface = false;  // Yeni boolean değişkeni
    private SpringJoint2D springJoint2D;
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
                case TouchPhase.Began:
                    isPressed = true;
                    rbBall.isKinematic = true;
                    cameraPositioner.StopFollowing();
                    Pointer();
                    lineRenderer.enabled = true;
                    break;

                case TouchPhase.Moved:
                    Pointer();
                    lineRenderer.enabled = true;
                    break;

                case TouchPhase.Ended:
                    isPressed = false;
                    rbBall.isKinematic = false;
                    StartCoroutine(Release());
                    lineRenderer.enabled = false;
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

        if (Input.GetMouseButtonDown(0) && moveCheckBool)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mousePosition, rbBall.position) <= maxDragDistance)
            {
                isPressed = true;
                rbBall.isKinematic = true;
                cameraPositioner.StopFollowing();
                Pointer();
                lineRenderer.enabled = true;
            }
        }

        if (Input.GetMouseButton(0) && isPressed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePosition, hookBall.position) > maxDragDistance)
            {
                rbBall.position = hookBall.position + (mousePosition - hookBall.position).normalized * maxDragDistance;
            }
            else
            {
                rbBall.position = mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0) && isPressed)
        {
            isPressed = false;
            rbBall.isKinematic = false;
            StartCoroutine(Release());
            lineRenderer.enabled = false;
        }

        if (rbBall.velocity.magnitude < 0.05f)
        {
            rbBall.velocity = Vector2.zero;
            rbBall.angularVelocity = 0f;

            if (!moveCheckBool)
            {
                moveCheckBool = true;
                hookBallGO.SetActive(true);
                GetComponent<SpringJoint2D>().enabled = true;
                hookBallGO.GetComponent<Transform>().position = transform.position;
            }
        }

        if (rbBall.IsSleeping())
        {
            moveCheckBool = true;
            hookBallGO.SetActive(true);
            GetComponent<SpringJoint2D>().enabled = true;
            hookBallGO.GetComponent<Transform>().position = transform.position;
        }

        // Yüzeye temas ediliyorsa ve topa dokunulduysa fırlatma mekanizmasını etkinleştir
        if (isOnSurface && Input.GetMouseButtonDown(0))
        {
            moveCheckBool = true;
            hookBallGO.SetActive(true);
            GetComponent<SpringJoint2D>().enabled = true;
            hookBallGO.GetComponent<Transform>().position = transform.position;
        }
    }

    // Top bir yüzeye temas ettiği sürece çağrılır
    void OnCollisionStay2D(Collision2D collision)
    {
        isOnSurface = true;
    }

    // Top yüzeyden ayrıldığında çağrılır
    void OnCollisionExit2D(Collision2D collision)
    {
        isOnSurface = false;
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
        cameraPositioner.StartFollowing();
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
            Debug.Log("Ground");
        }
    }
}

