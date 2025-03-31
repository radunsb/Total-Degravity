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

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
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

    public void OnHelp()
    {
        SceneManager.LoadScene("HelpScene");
    }
    
    void OnAdvance(InputValue value)
    {
        if(_buttonActive != -1)
        {
            buttons[_buttonActive].onClick.Invoke();
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
    }
}
