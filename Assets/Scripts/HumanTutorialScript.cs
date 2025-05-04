using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HumanTutorialScript : MonoBehaviour
{
    public int targetsDestroyed = 0;
    public GameObject[] destroyOnThree;
    public int tutorialOn = 0;
    public GameObject[] tutorialMessages;
    public Boolean[] showBox;
    public GameObject textBox;
    public GameObject _grabCube;
    public GameObject _player;
    public GameObject _thingLauncher;
    ThingLauncherScript _tls;
    AudioSource _musicSource;
    AudioSource _SFXSource;
    // Start is called before the first frame update
    void Start()
    {
        _musicSource = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("musicVol"))
        {
            _musicSource.volume = PlayerPrefs.GetFloat("musicVol") / 5f;
        }
        _SFXSource = GameObject.FindGameObjectWithTag("SFXsource").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            _SFXSource.volume = PlayerPrefs.GetFloat("SFXVol") / 5f;
        }
        _tls = _thingLauncher.GetComponent<ThingLauncherScript>();
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
        if (_grabCube.transform.parent == _player.transform && tutorialOn == 12)
        {
            setTutorialOn(13);
        }
        if(tutorialOn == 14 && Time.time - _tls._lastLaunchTime > 2 && Time.time - _tls._lastLaunchTime < 4)
        {
            setTutorialOn(15);
        }
    }

    public void targetHit()
    {
        targetsDestroyed++;
        if(targetsDestroyed == 3)
        {
            foreach(GameObject go in destroyOnThree){
                Destroy(go);
            }
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
        for(int i = 0; i < newTutorialOn; i++)
        {
            tutorialMessages[i].SetActive(false);
        }
        tutorialOn = newTutorialOn;
        print(tutorialOn);
    }
}
