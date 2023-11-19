using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SpawnerBase
{
    [SerializeField]
    public GameObject miniBoss;
    [SerializeField]
    public int miniBossCheckpointIncrement;

    private Vector2 miniBossSpawnPos;
    private int miniBossSpawnCheckpoint;

    protected new void Start()
    {
        base.Start();

        miniBossSpawnCheckpoint = miniBossCheckpointIncrement;
    }
    protected new void Update()
    {
        base.Update();

        if (GameHandler.instance.GetKills() >= miniBossSpawnCheckpoint)
        {
            GetMiniBoss();
        }
    }
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

    private void SpawnMiniBoss(GameObject spawn, Vector2 spawnPos)
    {
        if (spawn == null)
        {
            return;
        }

        GameObject enemyObj = Instantiate(spawn);
        EnemyBase enemy = enemyObj.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.Init(spawnPos, this);
        }

        miniBossSpawnCheckpoint += miniBossCheckpointIncrement;
        miniBossSpawnPos = Vector2.zero;
    }
    private void GetMiniBoss()
    {
        miniBossSpawnPos = GeneratePos();

        if(miniBossSpawnPos != Vector2.zero)
        {
            SpawnMiniBoss(miniBoss, miniBossSpawnPos);
        }
    }
}
