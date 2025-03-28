using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ZeroGPhysicsScript : MonoBehaviour
{
    public float maxSpeed; 
    public float driftSpeed; //the speed that the object will relax down to

    public float maxRotation;
    public float driftRotation;

    public float slowdownRatio; //the rate at which the object slows down - 0.999 means barely slow down. Anything below 0.9 will stop almost immediately

    bool _slowingV;
    bool _slowingR;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Debug.LogFormat("moving at {0}", _rb.velocity);

        if (_rb.velocity.magnitude > maxSpeed)
        {
            _rb.velocity = _rb.velocity * (maxSpeed / _rb.velocity.magnitude);
        }
        if (_rb.angularVelocity.magnitude > maxRotation)
        {
            _rb.angularVelocity = _rb.angularVelocity * (maxRotation / _rb.angularVelocity.magnitude);
        }

        if (_rb.velocity.magnitude <= driftSpeed)
        {
            //Debug.Log("done slowing down");
            _slowingV = false;
        }
        else if (_slowingV)
        {
            //Debug.LogFormat("slowing down from {0}", _rb.velocity.magnitude);
            _rb.velocity = slowdownRatio * _rb.velocity;
            //Debug.LogFormat("to {0}", _rb.velocity.magnitude);
        }

        if (_rb.angularVelocity.magnitude <= driftRotation)
        {
            _slowingR = false;
        }
        else if (_slowingR)
        {
            _rb.angularVelocity = slowdownRatio * _rb.angularVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("FixedObject"))
        {
            _slowingR = true;
            _slowingV = true;

            
        }
    }
}
