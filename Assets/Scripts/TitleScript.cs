using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }
}
