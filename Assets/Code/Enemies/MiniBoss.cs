using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBoss : EnemyBase
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    public float shotCooldown;
    [SerializeField]
    public float bulletForce;
    [SerializeField]
    public float fireRange;

    [SerializeField]
    public float meleeCooldown;
    private float meleeTimer;

    bool canFire;

    private void Start()
    {
        base.Start();

        canFire = true;
        meleeTimer = 0;
    }
    private new void Update()
    {
        meleeTimer += Time.deltaTime;

        Vector2 distance = player.transform.position - transform.position;

        if(distance.magnitude <= fireRange)
        {
            Fire();
        }

        base.Update();
    }
    public void Fire()
    {
        if (canFire == true)
        {
            Vector2 dir = player.transform.position - transform.position;

            GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);

            projectile.GetComponent<Projectile>().Init(attack);
            projectile.GetComponent<Rigidbody2D>().AddForce(dir.normalized * bulletForce, ForceMode2D.Impulse);

            if(GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }

            Invoke("ResetShot", shotCooldown);
            canFire = false;
        }
    }
    private void ResetShot()
    {
        canFire = true;
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject == player)
        {
            if(meleeTimer >= meleeCooldown)
            {
                player.GetComponent<PlayerBase>().TakeDamage(attack);

                meleeTimer = 0;
            }
        }
    }
 
}
