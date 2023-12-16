using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseObstacle : BaseObject
{
    [SerializeField]
    public int damageMulti;

    private int damage;

    public new void Init(Vector2 spawnPosition)
    {
        base.Init(spawnPosition);

        damage = (int)MathF.Round(scale * damageMulti); 
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Int32 layer = LayerMask.NameToLayer("Border");

        if (collision.gameObject.GetComponent<PlayerController>() == true)
        {
            Vector2 point = collision.GetContact(0).point;

            if (point.x < collision.transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerController>().AddDamagedArea(damageDirections.LEFT);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().AddDamagedArea(damageDirections.RIGHT);
            }

            collision.gameObject.GetComponent<PlayerBase>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == layer)
        {
            Destroy(gameObject);
        }
    }
}
