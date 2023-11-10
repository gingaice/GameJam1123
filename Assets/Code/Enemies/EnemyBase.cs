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

    protected int health;
    protected int attack;
    protected int moveSpeed;

    protected GameObject player;
    protected Rigidbody2D rb;

    private EnemySpawner enemySpawner;

    private bool isSpawned;

    private GameHandler gameHandler;
    private int Value = 100;
    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.freezeRotation = true;
        rb.drag = drag;

        health = maxHealth;
        attack = baseAttack;
        moveSpeed = baseSpeed;

        gameHandler = GameObject.Find("UIManager").GetComponent<GameHandler>();
    }
    public void Init(Vector2 spawnPosition, SpawnerBase spawner)
    {
        transform.position = spawnPosition;
        enemySpawner = (EnemySpawner)spawner; 

        isSpawned = true;
    }
    // Update is called once per frame
    protected void Update()
    {
       if(health <= 0)
       {
            //GetComponent<UIManager>().SetScore(value);

            gameHandler.score = gameHandler.score + Value;
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
