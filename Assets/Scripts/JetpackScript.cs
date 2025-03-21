using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JetpackScript : MonoBehaviour
{
    public float thrustForce = 0.001f;
    Rigidbody _rbody;

    bool _thrusting = false;
    bool _currentlyThrusting = false;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_thrusting && !_currentlyThrusting)
        {
            _rbody.AddForce(_rbody.transform.up * thrustForce * Time.deltaTime, ForceMode.Impulse);
            _currentlyThrusting = true;
        }
        else if (_thrusting && _currentlyThrusting)
        {
            _rbody.AddForce(_rbody.transform.up * thrustForce * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            _currentlyThrusting = false;
        }
    }

    void OnJetpack(InputValue value)
    {
        _thrusting = value.Get<float>() > 0;
    }
}
