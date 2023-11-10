using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBase : MonoBehaviour, IDamage
{
    Camera camera;

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
    public GameObject Gun1;
    [SerializeField] 
    public GameObject Gun2;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    public float shotCooldown;
    private float fireRate;

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
    bool gunSwitch;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        attack = baseAttack;
        durability = 100;
        pressure = 100;
        fireRate = shotCooldown;

        pressureChangeTimer = 0;
        pressureChangeCooldown = 1;

        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
        gunShot = GetComponent<AudioSource>();

        canFire = true;
        gunSwitch = false;
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
            controller.ClearDamage();

            if (pressure != 100)
            {
                IncreasePressure();
            }
        }

        if(durability == 0)
        {
            controller.SetIsBroken(true);
        }
        else
        {
            controller.SetIsBroken(false);
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

        if(durability < 0)
        {
            durability = 0;
        }
    }
    public void Repair(int amount)
    {
        controller.RemoveDamagedArea();
        durability += amount;

        if(durability > 100)
        {
            durability = 100;
        }
    }
    public float GetPressure()
    {
        return pressure;
    }
    public void Fire()
    {
        if(canFire == true)
        {
            Vector2 dir;
            GameObject projectile;

            if (gunSwitch == true)
            {
                projectile = Instantiate(bullet, Gun1.transform.position, Quaternion.identity);
                dir = camera.ScreenToWorldPoint(Input.mousePosition) - Gun1.transform.position;
            }
            else
            {
                projectile = Instantiate(bullet, Gun2.transform.position, Quaternion.identity);
                dir = camera.ScreenToWorldPoint(Input.mousePosition) - Gun2.transform.position;
            }
   

            projectile.GetComponent<Projectile>().Init(attack);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            projectile.GetComponent<Rigidbody2D>().AddForce(dir.normalized * bulletForce, ForceMode2D.Impulse);

            if (gunShot != null)
            {
                gunShot.Play();
            }

            Invoke("ResetShot", fireRate);

            canFire = false;
            gunSwitch = !gunSwitch;
        }
    }
    private void ResetShot()
    {
        canFire = true;
    }    

    public int GetDurability()
    {
        return durability;
    }

    public void AdjustFireRate(float rate)
    {
        fireRate -= rate;
    }

    public void ResetFireRate()
    {
        fireRate = shotCooldown;
    }
}
