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

    //[SerializeField]
    //float radius;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
    }


    //private void OnDrawGizmos()
    //{
    //    //this is to get how far the ai can see
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawn()
    {
        Bounds bounds = GetComponent<Collider>().bounds;

        if (asteroid != null) 
        {
            //Instantiate(asteroid, );
            for(int i = 0;i < 4;i++)
            {
                float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
                float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
                float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);
                //Instantiate(asteroid, Random.insideUnitSphere * radius + transform.position, Random.rotation);
                GameObject newHazard = GameObject.Instantiate(asteroid);
                newHazard.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
            }

        }
    }
}
