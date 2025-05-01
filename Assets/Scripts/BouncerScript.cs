using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
    public float bounceForce;
    public AudioSource _as;
    public AudioClip _boing;

    public Animator springAnimaitor;
    public Animator padAnimator;

    public float bounceCooldown;
    float _lastBounceTime;

    bool ready = true;

    public Transform center;
    public Transform target;

    private void Update()
    {
        if(Time.time > _lastBounceTime + bounceCooldown)
        {
            ready = true;
        }
        springAnimaitor.SetBool("Ready", ready);
        padAnimator.SetBool("Ready", ready);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRB = collision.rigidbody;

        if(otherRB != null && Time.time > _lastBounceTime + bounceCooldown)
        {
            Vector3 bounceDir = (target.position - center.position).normalized;

            Vector3 force = bounceDir * bounceForce;

            otherRB.AddForce(force);

            _as.PlayOneShot(_boing);

            ready = false;
            _lastBounceTime = Time.time;
        }
    }
}
