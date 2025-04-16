using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JetpackScript : MonoBehaviour
{
    public float thrustForce = 1f;
    Rigidbody _rbody;

    bool _thrusting = false;
    bool _backwardsThrusting = false;
    Vector2 _horizontalThrusting;
    float adaptiveThrustMultiplier = 1f;
    float[] velocityThresholds = { 2f, 5f, 10f };

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
        thrustForce = thrustForce / 50f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thrustForce = 0.1f;
        float curVelo = _rbody.velocity.magnitude;
        print(curVelo);
        if(curVelo < velocityThresholds[0])
        {
            adaptiveThrustMultiplier = 3.0f;
        }
        else if (curVelo < velocityThresholds[1])
        {
            adaptiveThrustMultiplier = 2.0f;
        }
        else if (curVelo < velocityThresholds[2])
        {
            adaptiveThrustMultiplier = 1.2f;
        }
        else
        {
            adaptiveThrustMultiplier = 1.0f;
        }
        thrustForce *= adaptiveThrustMultiplier;
        if (_thrusting)
        {
            _rbody.AddForce(_rbody.transform.up * thrustForce, ForceMode.Impulse);
        }
        else if (_backwardsThrusting)
        {
            _rbody.AddForce(-1 * _rbody.transform.up * thrustForce, ForceMode.Impulse);
        }
        if(_horizontalThrusting.y > 0)
        {
            _rbody.AddForce(_rbody.transform.forward * thrustForce, ForceMode.Impulse);
        }
        else if(_horizontalThrusting.y < 0)
        {
            _rbody.AddForce(-1 * _rbody.transform.forward * thrustForce, ForceMode.Impulse);
        }
        if(_horizontalThrusting.x > 0)
        {
            _rbody.AddForce(_rbody.transform.right * thrustForce, ForceMode.Impulse);
        }
        else if (_horizontalThrusting.x < 0)
        {
            _rbody.AddForce(-1 * _rbody.transform.right * thrustForce, ForceMode.Impulse);
        }
    }

    void OnJetpack(InputValue value)
    {
        _thrusting = value.Get<float>() > 0;
    }
    void OnReverse(InputValue value)
    {
        _backwardsThrusting = value.Get<float>() > 0;
    }
    
    void OnRotate(InputValue value)
    {
        _horizontalThrusting = value.Get<Vector2>();
    }
}
