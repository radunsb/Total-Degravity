using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MonkeHintScript : MonoBehaviour
{
    TMP_Text hintText;
    GameObject hintPanel;

    GameObject walls; 

    public MonkeScript ms;

    public float fastThresholdSpeed;
    public float slowWarningTime;

    float lastFastTime;

    bool hasSwungBack = false;

    Rigidbody _rb; 

    private void Start()
    {
        hintPanel = GameObject.FindWithTag("MonkeHint");
        hintText = GameObject.FindWithTag("MonkeHintText").GetComponent<TMP_Text>();
        walls = GameObject.FindWithTag("Wall");

        _rb = GetComponent<Rigidbody>();

        lastFastTime = Time.time;
    }

    private float distanceToCenter()
    {
        return (walls.transform.position - transform.position).magnitude;
    }

    private void Update()
    {
        Debug.LogFormat("has swing back: {0}", hasSwungBack);

        if(_rb.velocity.magnitude > fastThresholdSpeed)
        { 
            lastFastTime = Time.time;
        }

        if(Time.time > lastFastTime + slowWarningTime && ms.getBananaCount() > 0 && distanceToCenter() <= 110 && hasSwungBack)
        {
            hintPanel.SetActive(true);
            hintText.text = "Press C to propel yourself backwards"; 
        }
        else if(hintPanel.activeSelf && hasSwungBack)
        {
            hintPanel.SetActive(false);
        }
    }

    void OnSwordBack(InputValue value)
    {
        if (!hasSwungBack)
        {
            hasSwungBack = true;
            hintPanel.SetActive(false);
        }
    }
}
