using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField]
    public AudioClip deathNoise;

    [SerializeField]
    public int Value;

    protected int health;
    protected int attack;
    protected int moveSpeed;

    protected GameObject player;
    protected Rigidbody2D rb;

    private EnemySpawner enemySpawner;

    private bool isSpawned;

    private GameHandler gameHandler;

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

        gameHandler = GameHandler.instance;
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
            OnDeath(true);
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

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rb.AddForce((moveDirection.normalized *  moveSpeed), ForceMode2D.Force);
    }

    protected void OnDeath(bool killed)
    {
        if(killed == true)
        {
            gameHandler.score = gameHandler.score + Value;
            gameHandler.AddKill();
        }

        GetComponent<AudioSource>().PlayOneShot(deathNoise);
        Destroy(gameObject);
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() == true)
        {
            Vector2 point = collision.GetContact(0).point;

            if(point.x < collision.transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerController>().AddDamagedArea(damageDirections.LEFT);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().AddDamagedArea(damageDirections.RIGHT);
            }

            collision.gameObject.GetComponent<PlayerBase>().TakeDamage(attack);
            OnDeath(false);
        }
    }
}
