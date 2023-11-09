using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamage
{
    [SerializeField]
    public int ammo;

    [SerializeField]
    public int baseAttack;
    private int attack;

    [SerializeField]
    public float pressureIncrement;
    [SerializeField]
    public float pressureDecrement;

    [SerializeField]
    public Sprite stationarySprite;
    [SerializeField]
    public Sprite movingSprite;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    public float shotCooldown;

    PlayerController controller;
    SpriteRenderer spriteRenderer;

    private int durability;
    private float pressure;

    float pressureChangeTimer;
    float pressureChangeCooldown;


    AudioSource gunShot;

    [SerializeField]
    public float bulletForce;

    bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        attack = baseAttack;
        durability = 100;
        pressure = 100;

        pressureChangeTimer = 0;
        pressureChangeCooldown = 1;

        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
        gunShot = GetComponent<AudioSource>();

        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        pressureChangeTimer += Time.deltaTime;

        if(controller.GetIsMoving() == true)
        {
            spriteRenderer.sprite = movingSprite;
        }
        else
        {
            spriteRenderer.sprite = stationarySprite;
        }

        if (durability < 100)
        {
            DecreasePressure();
        }
        else
        {
            if (pressure != 100)
            {
                IncreasePressure();
            }
        }
    }

    private void IncreasePressure()
    {
        if (pressureChangeTimer < pressureChangeCooldown)
        {
            return;
        }

        float endPressure = pressure + pressureIncrement;

        pressure = Mathf.Lerp(pressure, endPressure, 1);

        if (pressure > 100)
        {
            pressure = 100;
        }

        pressureChangeTimer = 0;

    }
    private void DecreasePressure()
    {
        if (pressureChangeTimer < pressureChangeCooldown)
        {
            return;
        }

        float damage = 100 - durability;

        float pressureDecrease = damage * pressureDecrement;

        float endPressure = pressure - pressureDecrease;

        pressure = Mathf.Lerp(pressure, endPressure, 1);

        pressureChangeTimer = 0;
    }

    public void TakeDamage(int damage)
    {
        durability -= damage;
    }
    public void Repair(int amount)
    {
        durability += amount;
    }
    public float GetPressure()
    {
        return pressure;
    }
    public void Fire()
    {
        if(canFire == true)
        {
            Vector2 dir = transform.up;

            GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);

            projectile.GetComponent<Projectile>().Init(attack);
            projectile.GetComponent<Rigidbody2D>().AddForce(dir.normalized * bulletForce, ForceMode2D.Impulse);

            if (gunShot != null)
            {
                gunShot.Play();
            }

            Invoke("ResetShot", shotCooldown);
            canFire = false;
        }
    }
    private void ResetShot()
    {
        canFire = true;
    }    
}
