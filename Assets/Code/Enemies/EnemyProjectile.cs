using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.GetComponent<PlayerBase>())
            {
                collision.gameObject.GetComponent<PlayerBase>().TakeDamage(damage);
            }

            DestroyBullet();
        }
    }
}
