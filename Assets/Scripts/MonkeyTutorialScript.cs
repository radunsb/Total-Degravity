using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonkeyTutorialScript : MonoBehaviour
{
    public int tutorialOn = 0;
    public GameObject[] tutorialMessages;
    public Boolean[] showBox;
    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (showBox[tutorialOn] == true)
        {
            textBox.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            textBox.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void OnAdvance(InputValue value)
    {
        if (showBox[tutorialOn] == true)
        {
            tutorialOn++;
            if (showBox[tutorialOn] == false)
            {
                textBox.SetActive(false);
            }
            tutorialMessages[tutorialOn - 1].SetActive(false);
            tutorialMessages[tutorialOn].SetActive(true);
        }
    }

    public void setTutorialOn(int newTutorialOn)
    {
        tutorialMessages[newTutorialOn].SetActive(true);
        for (int i = 0; i < newTutorialOn; i++)
        {
            tutorialMessages[i].SetActive(false);
        }
        tutorialOn = newTutorialOn;
        print(tutorialOn);
    }
}
