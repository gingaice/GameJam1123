using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObstacleSpawner : MonoBehaviour
{
    //BaseObstacle asteroid;

    [SerializeField]
    GameObject asteroid;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //StartSpawn();
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            StartSpawn();

            timer = Random.Range(1,5);
        }
    }

    void StartSpawn()
    {
        Bounds bounds = GetComponent<Collider>().bounds;

        if (asteroid != null) 
        {
            //Instantiate(asteroid, );
            float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
            float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
            float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);
            //Instantiate(asteroid, Random.insideUnitSphere * radius + transform.position, Random.rotation);
            GameObject newHazard = GameObject.Instantiate(asteroid);
            newHazard.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);

        }
    }
}
