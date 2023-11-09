using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public void ChangeScene(string sceneName)
   {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
   }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
