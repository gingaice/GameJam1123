using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AreaSpawner : SpawnerBase
{
    [SerializeField]
    public float radiusMin;
    [SerializeField] 
    public float radiusMax;
    protected override Vector2 GeneratePos()
    {
        float spawnX;
        float spawnY;

        spawnX = Random.Range(worldBounds.min.x, worldBounds.max.x);
        spawnY = Random.Range(worldBounds.min.y, worldBounds.max.y);

        Vector2 spawnCheck = new Vector2(spawnX, spawnY);

        LayerMask mask = LayerMask.GetMask("Areas");
        mask += LayerMask.GetMask("Border");

        Collider2D[] hits = Physics2D.OverlapCircleAll(spawnCheck, 7.5f, mask);

        if (hits.Length != 0)
        {
            return Vector2.zero;
        }

        return spawnCheck;
    }

    protected override void SpawnObject(GameObject spawn)
    {
        if (spawn == null)
        {
            return;
        }

        GameObject areaObj = Instantiate(spawn);
        AreaBase area = areaObj.GetComponent<AreaBase>();

        if (area != null)
        {
            area.Init(spawnPosition ,Random.Range(radiusMin, radiusMax));
        }

        spawnPosition = Vector2.zero;
        spawnTimer = 0;
    }
}
