using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public GameObject sensitivitySlider;
    public GameObject musicVolSlider;
    public GameObject SFXVolSlider;
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
}
