using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MonkeScript : MonoBehaviour
{
    Rigidbody _rb;
    int _bananaCount = 3;
    public GameObject[] _bananaTexts;
    WinManager _winManager;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(returnBananas());
        _bananaTexts = GameObject.FindGameObjectsWithTag("BananaText");
        _winManager = GameObject.FindObjectOfType<WinManager>();
    }


    private void OnBanana(InputValue value)
    {
        if (_bananaCount > 0 && _winManager._gameStarted)
        {
            _rb.AddForce(-1 * transform.forward, ForceMode.Impulse);
            _bananaCount--;
            foreach (GameObject text in _bananaTexts)
            {
                text.GetComponent<Text>().text = "Current Bananas: " + _bananaCount;
            }
        }
    }
    IEnumerator returnBananas()
    {
        while (true)
        {
            yield return new WaitForSeconds(6);
            if(_bananaCount < 3)
            {
                _bananaCount++;
                foreach(GameObject text in _bananaTexts)
                {
                    text.GetComponent<Text>().text = "Current Bananas: " + _bananaCount;
                }
            }
        }
    }
}
