using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObstacleSpawner : SpawnerBase
{
    protected override void SpawnObject(GameObject spawn)
    {
        if (spawn == null)
        {
            return;
        }

        GameObject obstacleObj = Instantiate(spawn);
        BaseObstacle obstacle = obstacleObj.GetComponent<BaseObstacle>();

        if (obstacle != null)
        {
            obstacle.Init(spawnPosition);
        }

        spawnPosition = Vector2.zero;
        spawnTimer = Random.Range(0, spawnCooldown);
    }
}

