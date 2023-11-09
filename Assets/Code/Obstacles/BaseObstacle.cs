using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseObstacle : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float rotationSpeed;
    [SerializeField]
    public int damage;

    [SerializeField]
    public float minSize;
    [SerializeField]
    public float maxSize;

    private Transform player;
    private Rigidbody2D rb;

    private bool isSpawned, isActive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        player = GameObject.Find("Player").transform;
    }

    public void Init(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;

        float scale = Random.Range(minSize, maxSize);
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
        if(isSpawned == true)
        {
            if(isActive == false)
            {
                MoveObstacle();
            }

            RotateObstacle();
        }
    }

    private void MoveObstacle()
    {
        Vector3 moveDirection = player.position - transform.position;

        rb.AddForce((moveDirection.normalized * moveSpeed), ForceMode2D.Force);
        isActive = true;
    }
    private void RotateObstacle()
    {
        rb.MoveRotation(rb.rotation + rotationSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == true)
        {
            collision.gameObject.GetComponent<PlayerBase>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
