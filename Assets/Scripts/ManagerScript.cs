using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public GameObject humanPrefab;
    public PlayerInputManager inputManager;
    public Text p1Text;
    public Text p2Text;
    bool playerOneIn = false;
    public WinManager _winManager;
    float _musicVol;
    float _sfxVol;
    public AudioSource _sfxSource;
    public AudioSource _musicSource;
    bool paused = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
               
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            _sfxVol = PlayerPrefs.GetFloat("SFXVol") / 5f;
        }
        else
        {
            _sfxVol = 1;
        }
        if (PlayerPrefs.HasKey("musicVol"))
        {
            _musicVol = PlayerPrefs.GetFloat("musicVol") / 5f;
        }
        else
        {
            _musicVol = 1;
        }
        _musicSource.volume = _musicVol;
        _sfxSource.volume = _sfxVol;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void onFirstPlayerJoin()
    {
        if (playerOneIn)
        {
            onSecondPlayerJoin();
        }
        else
        {
            inputManager.playerPrefab = humanPrefab;
            p1Text.gameObject.SetActive(false);
            StartCoroutine(waitToSayPlayerIn());
        }

        //Uncomment the line below for single player testing
        onSecondPlayerJoin();
    }

    public void onSecondPlayerJoin()
    {
        _winManager._gameStarted = true;
        p2Text.gameObject.SetActive(false);
    }

    IEnumerator waitToSayPlayerIn()
    {
        yield return new WaitForSeconds(.2f);
        playerOneIn = true;
    }

    public void doPause()
    {
        if (paused)
        {
            pauseMenu.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
    }
}
