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
    public Boolean[] showBox = { true, true, false };
    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        tutorialOn++;
        if (showBox[tutorialOn] == false)
        {
            textBox.SetActive(false);
        }
        tutorialMessages[tutorialOn - 1].SetActive(false);
        tutorialMessages[tutorialOn].SetActive(true);
    }
}
