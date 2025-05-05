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

    public AudioSource _as;

    public AudioClip _damage;

    public AudioClip _destroy;
    public GameObject _explosion;

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
            _as.PlayOneShot(_destroy);
            _explosion.gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(MonkeyWin());
        }
        else
        {
            _as.PlayOneShot(_damage);
        }
    }

    IEnumerator MonkeyWin()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        if (_winManager != null)
        {
            _winManager.MonkeyWin();
        }
        else
        {
            MonkeyTutorialScript _mts = GameObject.FindObjectOfType<MonkeyTutorialScript>();
            _mts.setTutorialOn(20);
        }
    }

    public float getHealthFraction()
    {
        return _health / maxHealth;
    }
}
