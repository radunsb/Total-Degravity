using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    float[] rotDeltas = { 0, 0 };
    float[] rotAngles = { 0, 0 };
    Quaternion currentRot;
    bool invertX;
    bool invertY;
    float lookSensitivity;

    public Texture reticule;
    public Camera cam;
    private void OnGUI()
    {
        float endX = Screen.width;
        int size = 30;
        float posX = cam.pixelWidth / 2 - size / 4;
        if(cam.tag == "HumanCamera")
        {
            posX = endX - posX;
        }
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), reticule);
    }

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
        rotAngles[0] += rotDeltas[0] * lookSensitivity;
        rotAngles[1] += rotDeltas[1] * lookSensitivity;
        rotAngles[1] = Mathf.Clamp(rotAngles[1], -90f, 90f);
        this.transform.rotation = Quaternion.Euler(rotAngles[1], rotAngles[0], 0);
    }

    public void OnCamera(InputValue value)
    {
        Vector2 _movement = value.Get<Vector2>();
        if(_movement.x < 0.1f && _movement.y < 0.1f)
        {
            rotDeltas[0] = 0;
            rotDeltas[1] = 0;
        }
        rotDeltas[0] = !invertX ? _movement.x : -_movement.x;
        rotDeltas[1] = invertY ? _movement.y : -_movement.y;
    }

    public void OnLock(InputValue value)
    {

    }
}
