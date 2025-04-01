using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
    public float bounceForce;
    public AudioSource _as;
    public AudioClip _boing;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRB = collision.rigidbody;

        if(otherRB != null)
        {
            Vector3 bounceDir = (otherRB.position - transform.position).normalized;
            Vector3 force = bounceDir * bounceForce;

            otherRB.AddForce(force);

            _as.PlayOneShot(_boing);
        }
    }
}
