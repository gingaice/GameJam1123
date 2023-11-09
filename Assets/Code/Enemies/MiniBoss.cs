using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : EnemyBase
{

    PlayerBase player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider==player)
        {
             player.TakeDamage(attack);
        }
     
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
