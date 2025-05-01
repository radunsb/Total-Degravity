using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerScript : MonoBehaviour
{
    public Boolean human;
    public int tutorialNum;
    HumanTutorialScript _hts;
    MonkeyTutorialScript _mts;

    // Start is called before the first frame update
    void Start()
    {
        if (human)
        {
            _hts = GameObject.FindObjectOfType<HumanTutorialScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (human)
        {
            _hts.setTutorialOn(tutorialNum);
            Destroy(gameObject);
        }
    }
}
