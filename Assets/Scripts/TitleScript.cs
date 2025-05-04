using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public Button[] buttons;
    public int _buttonActive = -1;
    public AudioSource _as;
    public AudioSource _musicSource;
    public AudioClip _buttonChange;
    public AudioClip _buttonConfirm;
    float volume;
    private void Start()
    {
        GameObject[] _musicSources = GameObject.FindGameObjectsWithTag("MusicSource");
        if(_musicSources.Length > 1)
        {
            Destroy(_musicSources[1]);
        }
        _musicSource = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
        _as = GameObject.FindGameObjectWithTag("SFXsource").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("musicVol"))
        {
            _musicSource.volume = PlayerPrefs.GetFloat("musicVol") / 5f;
        }
        DontDestroyOnLoad(_musicSource.gameObject);
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            volume = PlayerPrefs.GetFloat("SFXVol") / 5f;
        }
        else
        {
            volume = 1;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnStart()
    {
        Destroy(_musicSource.gameObject);
        SceneManager.LoadScene("GameScene");
    }
    public void OnOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void OnHelp()
    {
        SceneManager.LoadScene("HelpScene");
    }
    
    void OnAdvance(InputValue value)
    {
        if(_buttonActive != -1)
        {
            buttons[_buttonActive].onClick.Invoke();
            _as.PlayOneShot(_buttonConfirm, volume);
        }
    }

    void OnScrollUp(InputValue value)
    {
        if (_buttonActive == -1)
        {
            _buttonActive = 0;
        }
        else
        {
            _buttonActive = (_buttonActive - 1 + buttons.Length) % buttons.Length;
        }
        EventSystem.current.SetSelectedGameObject(buttons[_buttonActive].gameObject);
        _as.PlayOneShot(_buttonChange, volume);
    }
    void OnScrollDown(InputValue value)
    {
        if (_buttonActive == -1)
        {
            _buttonActive = 0;
        }
        else
        {
            _buttonActive = (_buttonActive + 1) % buttons.Length;
        }
        EventSystem.current.SetSelectedGameObject(buttons[_buttonActive].gameObject);
        _as.PlayOneShot(_buttonChange, volume);
    }
}
