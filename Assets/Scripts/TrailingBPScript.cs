using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailingBPScript : MonoBehaviour
{
    public Transform target;

    public float slerpRate;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, target.position, slerpRate);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, slerpRate);
    }
}
