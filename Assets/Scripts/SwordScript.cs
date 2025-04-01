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

    public Vector3 swordRestPosition;
    public Vector3 swordRestRotation;

    public Vector3 swordChargingPosition;
    public Vector3 swordChargingRotation;

    public Vector3 swordFrontSwingFinalPosition;
    public Vector3 swordFrontSwingFinalRotation;

    public Vector3 swordBackSwingFinalPosition;
    public Vector3 swordBackSwingFinalRotation;

    Vector3 _destPosition;
    Vector3 _destRotation;
    float _swordSpeed;

    bool _charging = false;
    bool _front = false;
    bool _netted = false;

    float _hitsToBreakFree = 0;
    float _chargeTime = 0;

    float _swingTime = 0;

    Rigidbody _rb;
    BoxCollider _swordZone;

    WinManager _winManager;

    // Start is called before the first frame update
    void Start()
    {
        _winManager = GameObject.FindObjectOfType<WinManager>();
        _destPosition = swordRestPosition;
        _destRotation = swordRestRotation;

        _swordSpeed = recoverSpeed;

        _rb = GetComponent<Rigidbody>();
        _swordZone = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
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
        else if(_swingTime >= maxSwingTime)
        {
            _swingTime = 0;
            _chargeTime = 0;
        }
        //if we aren't charging, but we have a charge built up
        //swing the sword
        else if (_chargeTime > 0)
        {
            _swingTime += Time.deltaTime;

            // if not trapped in a net, swing the sword normally
            if (!_netted)
            {
                swing(_front);
            }
            // otherwise break free
            else
            {
                // add a moveSword for while in the net
                _hitsToBreakFree--;

                if (_hitsToBreakFree <= 0)
                {
                    _netted = false;
                }
            }
        }
        //we're neither charging nor swinging
        else
        {
            recover();
        }

        moveSword();
    }

    void OnSwordFront(InputValue value)
    {
        if (_winManager._gameStarted)
        {
            _charging = value.Get<float>() > 0;
            _front = true;
        }
    }

    void OnSwordBack(InputValue value)
    {
        if (_winManager._gameStarted)
        {
            _charging = value.Get<float>() > 0;
            _front = false;
        }
    }

    void charge()
    {
        _destPosition = swordChargingPosition;
        _destRotation = swordChargingRotation;
        _swordSpeed = recoverSpeed;
        swordCollider.enabled = false;
    }

    void recover()
    {
        _destPosition = swordRestPosition;
        _destRotation = swordRestRotation;
        _swordSpeed = recoverSpeed;
        swordCollider.enabled = false;
    }

    //we call swing every frame while we're swinging
    //that is, every frame where chargeTime > 0 and charging is false
    void swing(bool front)
    {
        if (front)
        {
            _destPosition = swordFrontSwingFinalPosition;
            _destRotation = swordFrontSwingFinalRotation;
        }
        else
        {
            _destPosition = swordBackSwingFinalPosition;
            _destRotation = swordBackSwingFinalRotation;
        }        
        _swordSpeed = swingSpeed;
        swordCollider.enabled = true;
    }

    void moveSword()
    {
        swordTransform.localPosition = Vector3.Slerp(_destPosition, swordTransform.localPosition, _swordSpeed);
        swordTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(_destRotation), swordTransform.localRotation, _swordSpeed);
    }

    void getNetted()
    {
        _netted = true;
        _hitsToBreakFree = 3;

        recover();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger entered");

        if (other.CompareTag("NetProjectile")){
            getNetted();
            return;
        }

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
