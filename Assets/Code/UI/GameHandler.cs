using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public enum currentTime
{
    one,
    two,
    three,
    four
}

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    public float gameTime;

    [SerializeField]
    public int score;

    private currentTime EcurrentTime;
    private SpawnerBase Sb;
    // Start is called before the first frame update
    void Start()
    {
        Sb = GameObject.Find("ObstacleSpawner").GetComponent<SpawnerBase>();
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        Debug.Log(score);
        Scoring();
        _Timing();
        changeSpawnTime();

        switch(EcurrentTime)
        {
            case currentTime.one:
                Sb.spawnCooldown = 4;
                break; 
            case currentTime.two:
                Sb.spawnCooldown = 3;
                break; 
            case currentTime.three:
                Sb.spawnCooldown = 2;
                break; 
            case currentTime.four:
                Sb.spawnCooldown = 1;
                break;
        }
    }

    public void Scoring()
    {
        GetComponent<UIManager>().SetScore(score);
    }
    
    public void _Timing()
    {
        GetComponent<UIManager>().TimerTxt.text = gameTime.ToString();
    }

    private void changeSpawnTime()
    {
        if (gameTime > 60)
        {
            EcurrentTime = currentTime.one;
        }
        else if (gameTime > 120)
        {
            EcurrentTime = currentTime.two;
        }
        else if (gameTime > 180)
        {
            EcurrentTime = currentTime.three;
        }
        else if (gameTime > 240)
        {
            EcurrentTime = currentTime.four;
        }
    }
}
