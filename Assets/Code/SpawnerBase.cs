using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    [SerializeField]
    public GameObject worldBase;
    [SerializeField]
    public CinemachineVirtualCamera camera;

    [SerializeField]
    public List<GameObject> spawnable;
    [SerializeField]
    public List<int> baseSpawnRates;

    [SerializeField]
    public int spawnCooldown;
    protected float spawnTimer;

    protected Vector2 spawnPosition;
    protected GameObject toSpawn;

    private float cameraHeight;
    private float cameraWidth;

    protected Bounds worldBounds;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector2.zero;
        spawnPosition = Vector2.zero;
        spawnTimer = 0;

        cameraHeight = camera.m_Lens.OrthographicSize * 2;
        cameraWidth = cameraHeight * camera.m_Lens.Aspect;

        worldBounds = new Bounds(worldBase.transform.position, new Vector2(worldBase.transform.localScale.x - 2f, worldBase.transform.localScale.y - 2f));
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnCooldown)
        {
            SpawnObject(GetSpawnObject());
        }
    }

    protected virtual void SpawnObject(GameObject spawn)
    {
        spawnPosition = Vector2.zero;
        spawnTimer = 0;
    }
    protected virtual GameObject GetSpawnObject()
    {
        GeneratePos();

        if (spawnPosition != Vector2.zero)
        {
            GenerateToSpawn();

            if (toSpawn != null)
            {
                return toSpawn;
            }
        }

        return null;
    }
    protected virtual void GeneratePos()
    {
        Bounds negativeBounds = new Bounds(camera.transform.position, new Vector2(cameraWidth, cameraHeight));

        float spawnX;
        float spawnY;

        spawnX = Random.Range(worldBounds.min.x, worldBounds.max.x);
        spawnY = Random.Range(worldBounds.min.y, worldBounds.max.y);

        Vector2 spawnCheck = new Vector2(spawnX, spawnY);

        if (negativeBounds.Contains(spawnCheck))
        {
            return;
        }

        spawnPosition = spawnCheck;
    }
    protected virtual void GenerateToSpawn()
    {
        int random = Random.Range(0, 100);

        int lowRange;
        int highRange = 0;

        for (int i = 0; i < spawnable.Count; i++)
        {
            lowRange = highRange;
            highRange += baseSpawnRates[i];

            if (random >= lowRange && random <= highRange)
            {
                toSpawn = spawnable[i];
                return;
            }
        }
    }
}
