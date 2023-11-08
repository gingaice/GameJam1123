using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObstacleSpawner : MonoBehaviour
{
    BaseObstacle asteroid;

    [SerializeField]
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        asteroid = new BaseObstacle();
        StartSpawn();
    }


    private void OnDrawGizmos()
    {
        //this is to get how far the ai can see
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawn()
    {
        
        if (asteroid != null) 
        {
            //Instantiate(asteroid, );
            for(int i = 0;i < 4;i++)
            {
                Instantiate(asteroid, Random.insideUnitSphere * radius + transform.position, Random.rotation);
            }

        }
    }
}
