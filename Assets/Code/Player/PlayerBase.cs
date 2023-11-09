using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField]
    public int ammo;
    [SerializeField]
    public float pressureIncrement;
    [SerializeField]
    public float pressureDecrement;

    [SerializeField]
    public Sprite stationarySprite;
    [SerializeField]
    public Sprite movingSprite;

    PlayerController controller;
    SpriteRenderer spriteRenderer;

    private int durability;
    private float pressure;

    float pressureChangeTimer;
    float pressureChangeCooldown;

    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform attackPos;
    AudioSource gunShot;
    [SerializeField]
    public float bulletForce;

    bool allowInvoke=true;
    // Start is called before the first frame update
    void Start()
    {
        durability = 100;
        pressure = 0;

        pressureChangeTimer = 0;
        pressureChangeCooldown = 1;

        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
        gunShot = GetComponent<AudioSource>();
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
            IncreasePressure();
        }
        else
        {
            if (pressure != 0)
            {
                DecreasePressure();
            }
        }
    }

    private void IncreasePressure()
    {
        if(pressureChangeTimer < pressureChangeCooldown)
        {
            return;
        }

        float damage = 100 - durability;

        float pressureIncrease = damage * pressureIncrement;

        float endPressure = pressure + pressureIncrease;

        pressure = Mathf.Lerp(pressure, endPressure, 1);

        pressureChangeTimer = 0;

    }
    private void DecreasePressure()
    {
        if (pressureChangeTimer < pressureChangeCooldown)
        {
            return;
        }

        float endPressure = pressure - pressureDecrement;

        pressure = Mathf.Lerp(pressure, endPressure, 1);

        if (pressure < 0)
        {
            pressure = 0;
        }

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
        Vector2 dir = transform.up;

        GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce(dir.normalized * bulletForce, ForceMode2D.Impulse);
        if(gunShot!=null)
            gunShot.Play();
        if(allowInvoke)
        {
            Invoke("ResetShot", 2.0f);
            allowInvoke = false;
        }
    }
    private void ResetShot()
    {
        allowInvoke = true;
    }    
}
