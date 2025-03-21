using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationScript : MonoBehaviour
{
    float[] rotDeltas = { 0, 0 };
    float[] rotAngles = { 0, 0 };
    float rotationDeadzone = 10f;

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
        rotAngles[0] += rotDeltas[0] * rotationDeadzone * Time.deltaTime;
        rotAngles[1] += rotDeltas[1] * rotationDeadzone * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(rotAngles[1] % 360, 0, rotAngles[0] % 360);
        //_rbody.MoveRotation(Quaternion.Euler(rotAngles[0] % 360, 0, rotAngles[1] % 360));
        //this.transform.Rotate(rotAngles[0] % 360, 0, rotAngles[1] % 360);
    }

    public void OnRotate(InputValue value)
    {
        Vector2 _movement = value.Get<Vector2>();
        rotDeltas[0] = -_movement.x;
        rotDeltas[1] = _movement.y;
        print("Detecting rotation attempt");
    }
}
