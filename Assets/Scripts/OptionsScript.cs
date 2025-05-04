using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public GameObject sensitivitySlider;
    public GameObject musicVolSlider;
    public GameObject SFXVolSlider;
    public GameObject monkeyFOVSlider;
    public GameObject humanFOVSlider;
    public GameObject[] objects;
    int _buttonActive = -1;
    public AudioSource _as;
    public AudioSource _musicSource;
    public AudioClip _buttonChange;
    public AudioClip _buttonConfirm;
    float volume;

    private void Start()
    {
        _musicSource = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            volume = PlayerPrefs.GetFloat("SFXVol")/5f;
        }
        else
        {
            volume = 1;
        }
        
        if (PlayerPrefs.HasKey("lookSensitivity"))
        {
            sensitivitySlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("lookSensitivity");
        }
        if (PlayerPrefs.HasKey("musicVol"))
        {
            musicVolSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVol");
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            SFXVolSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVol");           
        }

        if (PlayerPrefs.HasKey("MonkeyFOV"))
        {
            monkeyFOVSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MonkeyFOV");
        }

        if (PlayerPrefs.HasKey("HumanFOV"))
        {
            humanFOVSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("HumanFOV");
        }
    }
    public void onBack()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void sensitivityChanged(float newSens)
    {
        PlayerPrefs.SetFloat("lookSensitivity", newSens);
    }
    public void musicVolChanged(float newVol)
    {
        PlayerPrefs.SetFloat("musicVol", newVol);
        _musicSource.volume = newVol / 5f;
    }
    public void SFXVolChanged(float newVol)
    {
        PlayerPrefs.SetFloat("SFXVol", newVol);
        volume = newVol / 5f;
    }
    public void MonkeyFOVChanged(float newFOV)
    {
        PlayerPrefs.SetFloat("MonkeyFOV", monkeyFOVSlider.GetComponent<Slider>().value);
    }
    public void HumanFOVChanged(float newFOV)
    {
        PlayerPrefs.SetFloat("HumanFOV", humanFOVSlider.GetComponent<Slider>().value);
    }

    void OnScrollUp(InputValue value)
    {
        if (_buttonActive == -1)
        {
            _buttonActive = 0;
        }
        else
        {
            _buttonActive = (_buttonActive - 1 + objects.Length) % objects.Length;
        }
        _as.PlayOneShot(_buttonChange, volume);
        EventSystem.current.SetSelectedGameObject(objects[_buttonActive].gameObject);
    }
    void OnScrollDown(InputValue value)
    {
        if (_buttonActive == -1)
        {
            _buttonActive = 0;
        }
        else
        {
            _buttonActive = (_buttonActive + 1) % objects.Length;
        }
        _as.PlayOneShot(_buttonChange, volume);
        EventSystem.current.SetSelectedGameObject(objects[_buttonActive].gameObject);
    }
    void OnScrollRight(InputValue value)
    {
        if(_buttonActive >= 0 && _buttonActive <= 4)
        {
            _as.PlayOneShot(_buttonChange, volume);
            if (_buttonActive == 3 || _buttonActive == 4)
            {
                objects[_buttonActive].GetComponent<Slider>().value += 5.0f;
            } 
            else
            {
                objects[_buttonActive].GetComponent<Slider>().value += 0.2f;
            }
        }
    }
    void OnScrollLeft(InputValue value)
    {
        if (_buttonActive >= 0 && _buttonActive <= 4)
        {
            _as.PlayOneShot(_buttonChange, volume);
            if (_buttonActive == 3 || _buttonActive == 4)
            {
                objects[_buttonActive].GetComponent<Slider>().value -= 5.0f;
            }
            else
            {
                objects[_buttonActive].GetComponent<Slider>().value -= 0.2f;
            }
        }
    }

    void OnAdvance(InputValue value)
    {
        if(_buttonActive == 5)
        {
            onBack();
            _as.PlayOneShot(_buttonConfirm, volume);
        }
    }
}
