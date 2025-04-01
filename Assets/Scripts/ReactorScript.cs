using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorScript : MonoBehaviour
{
    public float maxHealth;
    float _health;

    public float swordDamageModifier;

    public float momentumDamageModifier;

    public float minMomentumForDamage;

    WinManager _winManager;

    private void Start()
    {
        _health = maxHealth;
        _winManager = GameObject.FindObjectOfType<WinManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRB = collision.rigidbody;

        if(otherRB != null)
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
        takeDamage( swordForceMagnitude * swordDamageModifier );
    }

    private void takeDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0) { 
            Destroy(gameObject);
            _winManager.MonkeyWin();
        }
    }

    public float getHealthFraction()
    {
        return _health / maxHealth;
    }
}
