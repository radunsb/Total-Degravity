using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SwordScript : MonoBehaviour
{
    public float swordFrontPower;
    public float swordBackPower;    

    public float maxChargeTime;

    public float swingSpeed;
    public float recoverSpeed;

    public float maxSwingTime;

    public Transform swordTransform;
    public Collider swordCollider;
    public Collider backFender; 
    public Collider frontFender;

    public Transform restTransform;

    public Transform chargeTransform;

    public Transform frontSwingTransform;

    public Transform backSwingTransform;


    Transform destination;    

    float _swordSpeed;

    bool _charging = false;
    bool _front = false;
    bool _isNetted = false;

    float _chargeTime = 0;

    float _swingTime = 0;

    Rigidbody _rb;
    BoxCollider _swordZone;

    WinManager _winManager;

    AudioSource _as;
    public AudioClip _basicStrike;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<ManagerScript>())
        {
            _as = GameObject.FindObjectOfType<ManagerScript>()._sfxSource;
        }
        _winManager = GameObject.FindObjectOfType<WinManager>();
        
        destination = restTransform;

        _swordSpeed = recoverSpeed;

        _rb = GetComponent<Rigidbody>();
        _swordZone = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isNetted)
        {
            if (_charging)
            {
                _swingTime = 0;

                charge();

                if (_chargeTime >= maxChargeTime)
                {
                    _chargeTime = maxChargeTime;
                }
                else
                {
                    _chargeTime += Time.deltaTime;
                }
            }
            else if (_swingTime >= maxSwingTime)
            {
                _swingTime = 0;
                _chargeTime = 0;
            }
            //if we aren't charging, but we have a charge built up
            //swing the sword
            else if (_chargeTime > 0)
            {
                _swingTime += Time.deltaTime;

                swing(_front);
            }
            //we're neither charging nor swinging
            else
            {
                recover();
            }

            moveSword();
        }
    }

    void OnSwordFront(InputValue value)
    {
        if (!_winManager || _winManager._gameStarted)
        {
            if (!_isNetted)
            {
                _charging = value.Get<float>() > 0;
                _front = true;
            }
        }
    }

    void OnSwordBack(InputValue value)
    {
        if (!_winManager || _winManager._gameStarted)
        {
            if (!_isNetted)
            {
                _charging = value.Get<float>() > 0;
                _front = false;
            }
        }
    }

    void charge()
    {
        destination = chargeTransform;
        _swordSpeed = recoverSpeed;
        swordCollider.enabled = false;
        backFender.enabled = false;
        frontFender.enabled = false;
    }

    void recover()
    {
        destination = restTransform;
        _swordSpeed = recoverSpeed;
        swordCollider.enabled = false;
        backFender.enabled = false;
        frontFender.enabled = false;
    }

    //we call swing every frame while we're swinging
    //that is, every frame where chargeTime > 0 and charging is false
    void swing(bool front)
    {
        if (front)
        {
            destination = frontSwingTransform;
            frontFender.enabled = true;
        }
        else
        {
            destination = backSwingTransform;
            backFender.enabled = true; 
        }        
        _swordSpeed = swingSpeed;
        swordCollider.enabled = true;
    }

    void moveSword()
    {
        swordTransform.localPosition = Vector3.Slerp(swordTransform.localPosition, destination.localPosition, _swordSpeed);
        swordTransform.localRotation = Quaternion.Slerp(swordTransform.localRotation, destination.localRotation, _swordSpeed);
        swordTransform.localScale = Vector3.Slerp(swordTransform.localScale, destination.localScale, _swordSpeed);
    }

    public void getNetted()
    {
        _isNetted = true;
        recover();
    }

    public void breakFree()
    {
        _isNetted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger entered");

        Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

        Vector3 launchForce;
        if (_front)
        {
            launchForce = swordFrontPower * transform.forward * _chargeTime;
        }
        else
        {
            launchForce = -1 * swordBackPower * transform.forward * _chargeTime;
        }

        if (other.CompareTag("Reactor"))
        {
            other.gameObject.GetComponent<ReactorScript>().takeSwordDamage(launchForce.magnitude);
        }
        else if (other.CompareTag("Shield")){
            other.gameObject.GetComponent<ShieldScript>().takeSwordDamage(launchForce.magnitude);
        }
        else
        {
            _as.PlayOneShot(_basicStrike);
        }

        if (otherRB != null)
        {
            otherRB.AddForce(launchForce);
            _rb.AddForce(-1 * launchForce);

            //Debug.LogFormat("applying force: {0}", launchForce.magnitude);

            _chargeTime = 0;
            _swingTime = 0;
        }
        else if(otherRB == null && other.gameObject.tag == "Wall")
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(-1 * launchForce);
            _chargeTime = 0;
            _swingTime = 0;
        }
    }

}
