using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{

    public Button[] buttons;
    public int _buttonActive = -1;
    public AudioSource _as;
    public AudioClip _buttonChange;
    public AudioClip _buttonConfirm;
    float volume;
    public ManagerScript _ms;
    public MonkeyTutorialScript _mts;
    public HumanTutorialScript _hts;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            volume = PlayerPrefs.GetFloat("SFXVol") / 5f;
        }
        else
        {
            volume = 1;
        }

    }


    void OnAdvance(InputValue value)
    {
        if (_buttonActive != -1)
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

    public void OnResume()
    {
        if (_ms != null)
        {
            _ms.doPause();
        }
        else if(_mts != null)
        {
            _mts.doPause();
        }
        else
        {
            _hts.doPause();
        }
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
