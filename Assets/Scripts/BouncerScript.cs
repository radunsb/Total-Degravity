using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
    public float bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRB = collision.rigidbody;

        if(otherRB != null)
        {
            Vector3 bounceDir = (otherRB.position - transform.position).normalized;
            Vector3 force = bounceDir * bounceForce;

            otherRB.AddForce(force);

            //TODO: add boing sound effect
        }
    }
}
