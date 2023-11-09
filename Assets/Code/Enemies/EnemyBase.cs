using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public int baseAttack;
    [SerializeField]
    public int baseSpeed;
    [SerializeField]
    public int drag;

    private int health;
    protected int attack;
    private int moveSpeed;

    private GameObject player;
    private Rigidbody2D rb;

    private EnemySpawner enemySpawner;

    private bool isSpawned;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.freezeRotation = true;
        rb.drag = drag;

        health = maxHealth;
        attack = baseAttack;
        moveSpeed = baseSpeed;
    }
    public void Init(Vector2 spawnPosition, SpawnerBase spawner)
    {
        transform.position = spawnPosition;
        enemySpawner = (EnemySpawner)spawner; 

        isSpawned = true;
    }
    // Update is called once per frame
    void Update()
    {
       if(health <= 0)
       {
            Destroy(gameObject);
       }
    }

    private void FixedUpdate()
    {
        if(isSpawned == true)
        {
            Seek(player);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    private void Seek(GameObject target)
    {
        Vector2 destination = target.transform.position;
        Vector2 moveDirection = destination - new Vector2(transform.position.x, transform.position.y);

        float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        rb.AddForce((moveDirection.normalized *  moveSpeed), ForceMode2D.Force);
        rb.rotation = angle;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() == true)
        {
            collision.gameObject.GetComponent<PlayerBase>().TakeDamage(attack);
            Destroy(gameObject);
        }
    }
}
