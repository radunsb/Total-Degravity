using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour
{
    int currentScreenOn = 0;
    public GameObject[] screens;
    public Button[] buttons;
    int _buttonActive = -1;
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
    public void OnBack()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnTutorial()
    {
        screens[0].SetActive(false);
        screens[1].SetActive(true);
        StartCoroutine(setCurrentScreenOn(1));
        
    }

    public void OnControls()
    {
        screens[0].SetActive(false);
        screens[8].SetActive(true);
        StartCoroutine(setCurrentScreenOn(8));
    }

    public void OnHumanTutorials()
    {
        SceneManager.LoadScene("HumanTutorial");
    }

    IEnumerator setCurrentScreenOn(int num)
    {
        yield return new WaitForEndOfFrame();
        currentScreenOn = num;
    }

    void OnAdvance(InputValue value)
    {
        if(currentScreenOn == 0)
        {
            if (_buttonActive != -1)
            {
                buttons[_buttonActive].onClick.Invoke();
                _as.PlayOneShot(_buttonConfirm, volume);
            }
        }
        if(currentScreenOn >= 1 && currentScreenOn < 7 || currentScreenOn == 8)
        {
            screens[currentScreenOn].SetActive(false);
            currentScreenOn++;
            screens[currentScreenOn].SetActive(true);
            _as.PlayOneShot(_buttonConfirm, volume);
        }
        else if(currentScreenOn == 7 || currentScreenOn == 9)
        {
            screens[currentScreenOn].SetActive(false);
            currentScreenOn = 0;
            screens[currentScreenOn].SetActive(true);
            _as.PlayOneShot(_buttonConfirm, volume);
        }
        
    }

    void OnScrollUp(InputValue value)
    {
        switch (_buttonActive)
        {
            case -1:
                {
                    _buttonActive = 0;
                    break;
                }
            case 0:
                {
                    _buttonActive = 4;
                    break;
                }
            case 1:
                {
                    _buttonActive = 4;
                    break;
                }
            default:
                {
                    _buttonActive = _buttonActive - 2;
                    break;
                }
        }
        _as.PlayOneShot(_buttonChange, volume);
        EventSystem.current.SetSelectedGameObject(buttons[_buttonActive].gameObject);
    }

    void OnScrollDown(InputValue value)
    {
        switch (_buttonActive)
        {
            case -1:
                {
                    _buttonActive = 0;
                    break;
                }
            case 4:
                {
                    _buttonActive = 0;
                    break;
                }
            case 3:
                {
                    _buttonActive = 4;
                    break;
                }
            default:
                {
                    _buttonActive = _buttonActive + 2;
                    break;
                }
        }
        _as.PlayOneShot(_buttonChange, volume);
        EventSystem.current.SetSelectedGameObject(buttons[_buttonActive].gameObject);
    }

    void OnScrollRight(InputValue value)
    {
        switch (_buttonActive)
        {
            case -1:
                {
                    _buttonActive = 0;
                    break;
                }
            case 0:
                {
                    _buttonActive = 1;
                    break;
                }
            case 1:
                {
                    _buttonActive = 0;
                    break;
                }
            case 2:
                {
                    _buttonActive = 3;
                    break;
                }
            case 3:
                {
                    _buttonActive = 2;
                    break;
                }
            default:
                {
                    break;
                }
        }
        _as.PlayOneShot(_buttonChange, volume);
        EventSystem.current.SetSelectedGameObject(buttons[_buttonActive].gameObject);
    }

    void OnScrollLeft(InputValue value)
    {
        OnScrollRight(value);
    }
}
