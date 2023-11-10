using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float rotationSpeed;

    [SerializeField]
    public float minSize;
    [SerializeField]
    public float maxSize;

    protected float scale;

    private Transform player;
    private Rigidbody2D rb;

    protected bool isSpawned, isActive;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        player = GameObject.Find("Player").transform;
    }
    public void Init(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;

        scale = UnityEngine.Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(scale, scale, scale);

        isActive = false;
        isSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isSpawned == true)
        {
            if (isActive == false)
            {
                MoveObstacle();
            }

            RotateObstacle();
        }
    }

    public void MoveObstacle()
    {
        Vector3 moveDirection = player.position - transform.position;

        rb.AddForce((moveDirection.normalized * moveSpeed), ForceMode2D.Force);
        isActive = true;
    }
    public void RotateObstacle()
    {
        rb.MoveRotation(rb.rotation + rotationSpeed * Time.fixedDeltaTime);
    }
}
