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

    private int kills;

    private currentTime EcurrentTime;
    private SpawnerBase Sb;

    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            Sb = GameObject.Find("ObstacleSpawner").GetComponent<SpawnerBase>();
            gameTime = 0;
            kills = 0;
            isPaused = false;

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

    public void TogglePause(bool paused)
    {
        isPaused = paused;
    }
    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void Scoring()
    {
        GetComponent<UIManager>().SetScore(score);
    }
    
    public int GetScore()
    {
        return score;
    }

    public void AddKill()
    {
        kills += 1;
    }

    public int GetKills()
    {
        return kills;
    }

    public void _Timing()
    {
        gameTime = (float)System.Math.Round(gameTime, 2);
        GetComponent<UIManager>().TimerTxt.text = gameTime.ToString();
    }

    private void changeSpawnTime()
    {
        switch (EcurrentTime)
        {
            case currentTime.one:
                Sb.spawnCooldown = 60;
                break;
            case currentTime.two:
                Sb.spawnCooldown = 50;
                break;
            case currentTime.three:
                Sb.spawnCooldown = 40;
                break;
            case currentTime.four:
                Sb.spawnCooldown = 30;
                break;
            case currentTime.five:
                Sb.spawnCooldown = 20;
                break;
        }

        if (gameTime < 30.0f) // i dont know why the fuck this aint running will fix tmr
        {
            EcurrentTime = currentTime.one;
        }
        else if (gameTime < 60.0f)
        {
            EcurrentTime = currentTime.two;
        }
        else if (gameTime < 90f)
        {
            EcurrentTime = currentTime.three;
        }
        else if (gameTime < 120f)
        {
            EcurrentTime = currentTime.four;
        }
        else
        {
            EcurrentTime = currentTime.five;
        }
    }
}
