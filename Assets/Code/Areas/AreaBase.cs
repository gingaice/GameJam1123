using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBase : MonoBehaviour
{
    private PlayerBase player;

    [SerializeField]
    public float effectDelay;
    private float effectTimer;

    [SerializeField]
    public float minLifeSpan;
    [SerializeField]
    public float maxLifeSpan;

    private float lifeSpan;
    // Start is called before the first frame update
    void Start()
    {
        player = null;
    }

    public void Init(Vector2 position, float radius)
    {
        transform.position = position;

        float scale = radius * 2;
        transform.localScale = new Vector3(scale, scale, scale);

        lifeSpan = Random.Range(minLifeSpan, maxLifeSpan);

        Destroy(gameObject, lifeSpan);
    }

    private void Update()
    {
        effectTimer += Time.deltaTime;
    }
    protected void FixedUpdate()
    {
        if(player != null)
        {
            if(effectTimer >= effectDelay)
            {
                Effect(player);
            }
        }
    }
    protected virtual void Effect(PlayerBase player) 
    {
        effectTimer = 0;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerBase>())
        {
            player = collision.gameObject.GetComponent<PlayerBase>();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBase>())
        {
            player = null;
        }
    }
}
