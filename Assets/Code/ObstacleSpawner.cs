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
        StartSpawn();
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
            Instantiate(asteroid, Random.insideUnitSphere * radius + transform.position, Random.rotation);
        }
    }
}
