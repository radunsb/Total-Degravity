using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MonkeScript : MonoBehaviour
{
    Rigidbody _rb;
    int bananaCount = 3;
    WinManager _winManager;
    SwordScript _swordScript;

    public GameObject _netText;
    public GameObject _netImage;
    public GameObject _sword;
    public bool keepInSphere;

    float _nettedTime = 3f;
    bool _isNetted = false;

    public GameObject bananaPrefab;
    public float bananaForce;
    GameObject[] bananaImages;

    ManagerScript _ms;
    MonkeyTutorialScript _mts;

    // Start is called before the first frame update
    void Start()
    {
        _ms = GameObject.FindObjectOfType<ManagerScript>();
        if(_ms == null)
        {
            _mts = GameObject.FindObjectOfType<MonkeyTutorialScript>();
        }
        bananaImages = new GameObject[3];
        _rb = GetComponent<Rigidbody>();
        _swordScript = GetComponent<SwordScript>();
        StartCoroutine(returnBananas());
        GameObject bananaImageParent = GameObject.FindGameObjectWithTag("BananaImage");
        for (int i = 0; i < bananaImageParent.transform.childCount; i++)
        {
            bananaImages[i] = bananaImageParent.transform.GetChild(i).gameObject;
        }
        _winManager = GameObject.FindAnyObjectByType<WinManager>();
    }

    private void Update()
    {

        if(keepInSphere && transform.position.magnitude > 47)
        {
            transform.position = transform.position.normalized * 47;
        }

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

    void OnPause(InputValue value)
    {
        if (_ms != null)
        {
            _ms.doPause();
        }
        else if(_mts != null)
        {
            _mts.doPause();
        }
    }

    private void OnBanana(InputValue value)
    {
        if (bananaCount > 0 && (!_winManager || _winManager._gameStarted))
        {
            _rb.AddForce(-1 * transform.forward * bananaForce, ForceMode.Impulse);
            bananaCount--;

            launchNewBanana();

            for(int i = 0; i < 3; i++)
            {
                //activate number of banana images = to current count
                bananaImages[i].gameObject.SetActive(bananaCount > i);
            }
        }
    }
    IEnumerator returnBananas()
    {
        while (true)
        {
            yield return new WaitForSeconds(6);
            if(bananaCount < 3)
            {
                bananaCount++;
                for (int i = 0; i < 3; i++)
                {
                    bananaImages[i].gameObject.SetActive(bananaCount > i);
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

    void launchNewBanana()
    {
        GameObject newBanana = Instantiate(bananaPrefab, transform.position + transform.forward, Quaternion.identity);
        newBanana.SetActive(true);
        Rigidbody brb = newBanana.GetComponent<Rigidbody>();

        brb.velocity = _rb.velocity;
        brb.AddForce(bananaForce * transform.forward, ForceMode.Impulse);
        brb.AddTorque(3 * new Vector3(Random.value, Random.value, Random.value));
    }

    public int getBananaCount()
    {
        return bananaCount;
    }
}
