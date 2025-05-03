using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArtificialGravity : MonoBehaviour {
    Transform centerOfGravity;

    public string centerTag;
    public float gravityForce;

    Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        centerOfGravity = GameObject.FindWithTag(centerTag).transform; 
    }

    private void FixedUpdate()
    {
        Vector3 forceDirection = (centerOfGravity.position - transform.position).normalized;

        _rb.AddForce(forceDirection * gravityForce);
    }
}
