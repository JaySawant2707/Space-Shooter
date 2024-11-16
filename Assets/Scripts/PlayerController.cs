using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Vector2 rawInput;
    Vector2 minBound;
    Vector2 maxBound;
    //Vector3 touchPos;

    Rigidbody2D rb;
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        InitBounds();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom, maxBound.y - paddingTop);

        transform.position = newPos;

        // if(Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        //     touchPos.z = 0;
        //     Vector3 dir = touchPos - transform.position;
        //     rb.linearVelocity = new Vector2(dir.x, dir.y) * moveSpeed * Time.deltaTime;

        //     if (touch.phase == UnityEngine.TouchPhase.Ended)
        //     {
        //         rb.linearVelocity = Vector2.zero;
        //     }
        // }
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
