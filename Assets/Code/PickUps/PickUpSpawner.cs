using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : SpawnerBase
{
    protected override void SpawnObject(GameObject spawn)
    {
        if (spawn == null)
        {
            return;
        }

        GameObject pickUpObj = Instantiate(spawn);
        PickUpBase pickUp = pickUpObj.GetComponent<PickUpBase>();

        if (pickUp != null)
        {
            pickUp.Init(spawnPosition);
        }

        spawnPosition = Vector2.zero;
        spawnTimer = Random.Range(0, spawnCooldown);
    }
}
