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
        if (GameObject.FindGameObjectsWithTag(centerTag).Length > 0)
        {
            centerOfGravity = GameObject.FindWithTag(centerTag).transform;
        }
    }

    private void FixedUpdate()
    {
        if (centerOfGravity != null)
        {
            Vector3 forceDirection = (centerOfGravity.position - transform.position).normalized;
            _rb.AddForce(forceDirection * gravityForce);
        }
    }
}
