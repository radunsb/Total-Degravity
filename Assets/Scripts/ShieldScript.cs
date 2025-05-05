using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float maxHealth;
    float _health;

    public float consoleDestructionDamage;

    public float swordDamageModifier;

    public float momentumDamageModifier;

    public float minMomentumForDamage;

    public Collider reactorCollider;

    public AudioSource _as;
    public AudioClip _damage;
    public AudioClip _break;
    public GameObject _explosion;

    private void Start()
    {
        _health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRB = collision.rigidbody;

        if (otherRB != null)
        {
            float momentum = collision.relativeVelocity.magnitude * otherRB.mass;

            if (momentum > minMomentumForDamage)
            {
                takeDamage(momentum * momentumDamageModifier);
            }
        }
    }

    public void takeSwordDamage(float swordForceMagnitude)
    {
        takeDamage(swordForceMagnitude * swordDamageModifier);
    }

    public void takeConsoleDestroyDamage()
    {
        takeDamage(consoleDestructionDamage);
    }

    private void takeDamage(float damage)
    {
        Debug.LogFormat("Shiled taking damage: {0}", damage);
        //Shield takes at least 3 sword hits
        _health -= Mathf.Min(500, damage);

        if (_health <= 0)
        {
            reactorCollider.enabled = true;
            _as.PlayOneShot(_break);
            _explosion.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
        else
        {
            _as.PlayOneShot(_damage);
        }
    }

    public float getHealthFraction()
    {
        return _health / maxHealth;
    }
}
