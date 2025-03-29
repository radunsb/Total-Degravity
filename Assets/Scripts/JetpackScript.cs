using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JetpackScript : MonoBehaviour
{
    public float thrustForce = 1f;
    Rigidbody _rbody;

    bool _thrusting = false;
    bool _backwardsThrusting = false;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_thrusting)
        {
            _rbody.AddForce(_rbody.transform.up * thrustForce * Time.deltaTime, ForceMode.Impulse);
        }
        else if (_backwardsThrusting)
        {
            _rbody.AddForce(-1 * _rbody.transform.up * thrustForce * Time.deltaTime, ForceMode.Impulse);
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
}
