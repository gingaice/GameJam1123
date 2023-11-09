using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public int baseSpeed;
    [SerializeField]
    public float drag;

    public Transform orientation;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    private int moveSpeed;
    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = drag;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        moveSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        MoveCharacter();
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
}
