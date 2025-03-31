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
    public GameObject[] objects;
    int _buttonActive = -1;
    private void Start()
    {
        if (PlayerPrefs.HasKey("lookSensitivity"))
        {
            sensitivitySlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("lookSensitivity");
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
        EventSystem.current.SetSelectedGameObject(objects[_buttonActive].gameObject);
    }
    void OnScrollRight(InputValue value)
    {
        if(_buttonActive >= 0 && _buttonActive <= 2)
        {
            objects[_buttonActive].GetComponent<Slider>().value += 0.2f;
        }
    }
    void OnScrollLeft(InputValue value)
    {
        if (_buttonActive >= 0 && _buttonActive <= 2)
        {
            objects[_buttonActive].GetComponent<Slider>().value -= 0.2f;
        }
    }

    void OnAdvance(InputValue value)
    {
        if(_buttonActive == 3)
        {
            onBack();
        }
    }
}
