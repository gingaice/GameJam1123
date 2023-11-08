using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    BaseObstacle asteroid;

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
        }
    }
}
