using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public float secondsRemaining;
    public TMP_Text countdownText;
    public bool _gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStarted)
        {
            secondsRemaining -= Time.deltaTime;
            int minutes = (int)(secondsRemaining / 60);
            int seconds = (int)(secondsRemaining % 60);
            countdownText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
        if (secondsRemaining <= 0) {
            SceneManager.LoadScene("HumanWinScene");
        }
    }

    //TODO: either the reactor calls this method
    public void MonkeyWin()
    {
        SceneManager.LoadScene("MonkeyWinScene");
    }
}
