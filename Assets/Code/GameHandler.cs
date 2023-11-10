using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public enum currentTime
{
    one,
    two,
    three,
    four,
    five
}

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    [SerializeField]
    public float gameTime;

    [SerializeField]
    public int score;

    private currentTime EcurrentTime;
    private SpawnerBase Sb;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            Sb = GameObject.Find("ObstacleSpawner").GetComponent<SpawnerBase>();
            gameTime = 0;

            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        Scoring();
        _Timing();
        changeSpawnTime();
    }

    public void Scoring()
    {
        GetComponent<UIManager>().SetScore(score);
    }
    
    public int GetScore()
    {
        return score;
    }
    public void _Timing()
    {
        GetComponent<UIManager>().TimerTxt.text = gameTime.ToString();
    }

    private void changeSpawnTime()
    {
        switch (EcurrentTime)
        {
            case currentTime.one:
                Sb.spawnCooldown = 5;
                break;
            case currentTime.two:
                Sb.spawnCooldown = 4;
                break;
            case currentTime.three:
                Sb.spawnCooldown = 3;
                break;
            case currentTime.four:
                Sb.spawnCooldown = 2;
                break;
            case currentTime.five:
                Sb.spawnCooldown = 1;
                break;
        }

        if (gameTime > 6.0f) // i dont know why the fuck this aint running will fix tmr
        {
            EcurrentTime = currentTime.two;
        }
        else if (gameTime > 12.0f)
        {
            EcurrentTime = currentTime.three;
        }
        else if (gameTime > 18f)
        {
            EcurrentTime = currentTime.four;
        }
        else if (gameTime > 24f)
        {
            EcurrentTime = currentTime.five;
        }
    }
}
