using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    public float gameTime;

    [SerializeField]
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        Debug.Log(score);
        Scoring();
        _Timing();
    }

    public void Scoring()
    {
        GetComponent<UIManager>().SetScore(score);
    }
    
    public void _Timing()
    {
        GetComponent<UIManager>().TimerTxt.text = gameTime.ToString();
    }
}
