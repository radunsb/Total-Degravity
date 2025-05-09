using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSceneScript : MonoBehaviour
{
    int _buttonActive = -1;
    public Button[] buttons;
    public AudioSource _as;
    public AudioClip _buttonChange;
    public AudioClip _buttonConfirm;
    float volume;
    private void Start()
    {
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            volume = PlayerPrefs.GetFloat("SFXVol") / 5f;
        }
        else
        {
            volume = 1;
        }
    }
    public void OnReplay()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnMenu()
    {
        SceneManager.LoadScene("TitleScene");
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

    void OnAdvance(InputValue value)
    {
        if (_buttonActive != -1)
        {
            buttons[_buttonActive].onClick.Invoke();
            _as.PlayOneShot(_buttonConfirm, volume);
        }
    }
}
