using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MonkeScript : MonoBehaviour
{
    Rigidbody _rb;
    int _bananaCount = 3;
    public GameObject[] _bananaTexts;
    WinManager _winManager;
    SwordScript _swordScript;

    public GameObject _netText;
    public GameObject _netImage;
    public GameObject _sword;

    float _nettedTime = 3f;
    bool _isNetted = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _swordScript = GetComponent<SwordScript>();
        StartCoroutine(returnBananas());
        _bananaTexts = GameObject.FindGameObjectsWithTag("BananaText");
        _winManager = GameObject.FindObjectOfType<WinManager>();
    }

    private void Update()
    {
        if (_isNetted)
        {
            if (_nettedTime <= 0)
            {
                _isNetted = false;
                _nettedTime = 0;

                _netText.SetActive(false);
                _netImage.SetActive(false);

                _sword.gameObject.GetComponent<MeshRenderer>().enabled = true;

                _swordScript.breakFree();
            }
            else
            {
                _netText.GetComponent<UnityEngine.UI.Text>().text = "You're trapped in a net!\r\n\r\n" + (int)_nettedTime;

                _nettedTime -= Time.deltaTime;
            }
        }
    }

    private void OnBanana(InputValue value)
    {
        if (_bananaCount > 0 && _winManager._gameStarted)
        {
            _rb.AddForce(-1 * transform.forward, ForceMode.Impulse);
            _bananaCount--;
            foreach (GameObject text in _bananaTexts)
            {
                text.GetComponent<UnityEngine.UI.Text>().text = "Current Bananas: " + _bananaCount;
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
                    text.GetComponent<UnityEngine.UI.Text>().text = "Current Bananas: " + _bananaCount;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NetProjectile"))
        {
            _isNetted = true;
            _swordScript.getNetted();

            _netImage.SetActive(true);
            _netText.SetActive(true);

            _sword.gameObject.GetComponent<MeshRenderer>().enabled = false;

            _nettedTime = 3f;
        }
    }
}
