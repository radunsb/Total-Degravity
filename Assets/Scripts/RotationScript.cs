using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationScript : MonoBehaviour
{
    float[] rotDeltas = { 0, 0 };
    float[] rotAngles = { 0, 0 };
    float rotationDeadzone = 3f;

    Rigidbody _rbody;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rotAngles[0] += rotDeltas[0] * rotationDeadzone % 360;
        rotAngles[1] += rotDeltas[1] * rotationDeadzone % 360;
        this.transform.rotation = Quaternion.Euler(rotAngles[0], 0, rotAngles[1]);
        _rbody.MoveRotation(Quaternion.Euler(rotAngles[0], 0, rotAngles[1]));
        this.transform.Rotate(rotAngles[0], 0, rotAngles[1]);
    }

    public void OnRotate(InputValue value)
    {
        Vector2 _movement = value.Get<Vector2>();
        if (_movement.x < 0.1f && _movement.y < 0.1f)
        {
            rotDeltas[0] = 0;
            rotDeltas[1] = 0;
        } else
        {
            rotDeltas[0] = _movement.x;
            rotDeltas[1] = _movement.y;
        }
        print("Detecting rotation attempt");
    }
}
