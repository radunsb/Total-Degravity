using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SwordScript : MonoBehaviour
{
    public float swordPower;
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

    public Vector3 swordSwingFinalPosition;
    public Vector3 swordSwingFinalRotation;

    Vector3 _destPosition;
    Vector3 _destRotation;
    float _swordSpeed;

    bool _charging = false;
    float _chargeTime = 0;

    float _swingTime = 0;

    Rigidbody _rb;
    BoxCollider _swordZone;


    // Start is called before the first frame update
    void Start()
    {
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

            swing();
        }
        //we're neither charging nor swinging
        else
        {
            recover();
        }

        moveSword();
    }

    void OnSword(InputValue value)
    {
        _charging = value.Get<float>() > 0;
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
    void swing()
    {
        _destPosition = swordSwingFinalPosition;
        _destRotation = swordSwingFinalRotation;
        _swordSpeed = swingSpeed;
        swordCollider.enabled = true;
    }

    void moveSword()
    {
        swordTransform.localPosition = Vector3.Slerp(_destPosition, swordTransform.position, _swordSpeed);
        swordTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(_destRotation), swordTransform.rotation, _swordSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");

        Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

        if (otherRB != null)
        {
            Vector3 launchForce = swordPower * transform.forward * _chargeTime;

            otherRB.AddForce(launchForce);
            _rb.AddForce(-1 * launchForce);

            Debug.LogFormat("applying force: {0}", launchForce.magnitude);

            _chargeTime = 0;
            _swingTime = 0;
        }
    }
}
