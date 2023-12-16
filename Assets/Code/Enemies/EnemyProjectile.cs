using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.GetComponent<PlayerController>() == true)
            {
                Vector2 point = collision.ClosestPoint(transform.position);

                if (point.x < collision.transform.position.x)
                {
                    collision.gameObject.GetComponent<PlayerController>().AddDamagedArea(damageDirections.LEFT);
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerController>().AddDamagedArea(damageDirections.RIGHT);
                }

                collision.gameObject.GetComponent<PlayerBase>().TakeDamage(damage);
            }

            DestroyBullet();
        }
    }
}
