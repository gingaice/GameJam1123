using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SpawnerBase
{
    protected override void SpawnObject(GameObject spawn)
    {
        if(spawn == null)
        {
            return;
        }

        GameObject enemyObj = Instantiate(spawn);
        EnemyBase enemy = enemyObj.GetComponent<EnemyBase>();

        if(enemy != null)
        {
            enemy.Init(spawnPosition, this);
        }

        base.SpawnObject(spawn);
    }
}
