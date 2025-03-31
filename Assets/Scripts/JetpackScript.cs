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
    float[] thrustThresholds = { 2, 1.5f, 1 };

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
        thrustForce = thrustForce / 50f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
