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

    AudioSource _as;

    private void Start()
    {
        _health = maxHealth;
        _as = GetComponent<AudioSource>();
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
                _as.Play();
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

        _health -= damage;

        if (_health <= 0)
        {
            reactorCollider.enabled = true;
            Destroy(gameObject);
        }
    }

    public float getHealthFraction()
    {
        return _health / maxHealth;
    }
}
