using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    float[] rotAngles = { 0, 0 };
    Quaternion currentRot;
    bool invertX;
    bool invertY;
    float lookSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        invertX = PlayerPrefs.HasKey("invertX") ? PlayerPrefs.GetInt("invertX") == 1 : false;
        invertX = PlayerPrefs.HasKey("invertY") ? PlayerPrefs.GetInt("invertY") == 1 : false;
        lookSensitivity = PlayerPrefs.HasKey("lookSensitivity") ? PlayerPrefs.GetFloat("lookSensitivity") : 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(rotAngles[1], rotAngles[0], 0) * lookSensitivity);
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0);
    }

    public void OnCamera(InputValue value)
    {
        Vector2 _movement = value.Get<Vector2>();
        if(_movement.x < 0.1f && _movement.y < 0.1f)
        {
            rotAngles[0] = 0;
            rotAngles[1] = 0;
        }
        rotAngles[0] = !invertX ? _movement.x : -_movement.x;
        rotAngles[1] = invertY ? _movement.y : -_movement.y;
    }

    public void OnLock(InputValue value)
    {

    }
}
