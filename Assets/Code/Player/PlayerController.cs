using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera camera;

    [SerializeField]
    public int baseSpeed;
    [SerializeField]
    public float drag;

    public Transform orientation;
    private Rigidbody2D rb;

    private Vector2 moveDirection;
    private Vector2 mouseDirection;

    private int moveSpeed;

    float horizontalInput;
    float verticalInput;

    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        rb = GetComponent<Rigidbody2D>();
        rb.drag = drag;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        moveSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameHandler.instance.GetIsPaused() == false)
        {
            if (horizontalInput == 0f && verticalInput == 0f)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            Inputs();
        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacter();
    }

    private void RotateCharacter()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        orientation.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }
    private void MoveCharacter()
    {
        moveDirection = (orientation.up * verticalInput) + (orientation.right * horizontalInput);

        rb.AddForce((moveDirection.normalized * moveSpeed), ForceMode2D.Force);

        SpeedCap();
    }

    private void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        mouseDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if(Input.GetKey(KeyCode.Mouse0))
        {
            GetComponent<PlayerBase>().Fire(); 
        }
    }

    private void SpeedCap()
    {
        Vector2 flatVelocity = new Vector3(rb.velocity.x, rb.velocity.y);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector2 cappedVelocity = flatVelocity.normalized * moveSpeed;

            rb.velocity = new Vector2(cappedVelocity.x, rb.velocity.y);
        }
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void AdjustMoveSpeed(int speed)
    {
        moveSpeed += speed;  
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
