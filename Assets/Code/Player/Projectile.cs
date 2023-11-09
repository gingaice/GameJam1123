using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    GameObject bullet;

    protected int damage;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void Init(int dmg)
    {
        damage = dmg;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("NotBoss");
        if (collision.gameObject != null)
        {
            if (collision.gameObject.GetComponent<EnemyBase>())
            {
                collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
            }

            DestroyBullet();
        }
    }

    protected void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
