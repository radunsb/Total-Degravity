using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingLauncherScript : MonoBehaviour
{
    public GameObject[] thingsToLaunch;

    public Transform launchFrom;
    public Transform launchTowards;
    
    public float launchSpeed;
    public float launchRotation;

    public float launchCooldown;
    float _lastLaunchTime = int.MinValue;

    public float launchDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && Time.time > _lastLaunchTime + launchCooldown)
        {
            _lastLaunchTime = Time.time;
            StartCoroutine(launchThing());
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _lastLaunchTime = Time.time;
            StartCoroutine(launchThing());
        }
    }

    private IEnumerator launchThing()
    {
        yield return new WaitForSeconds(launchDelay);

        int thingIndex = (int)(Random.value * thingsToLaunch.Length);

        GameObject thingToLaunch = Instantiate(thingsToLaunch[thingIndex], launchFrom);
        thingToLaunch.SetActive(true);

        Rigidbody rb = thingToLaunch.GetComponent<Rigidbody>();

        Vector3 launchVelocity = (launchTowards.position - launchFrom.position).normalized;
        launchVelocity = launchVelocity * launchSpeed;

        rb.velocity = launchVelocity;

        Vector3 launchRotation = new Vector3(Random.value, Random.value, Random.value).normalized;
        rb.angularVelocity = launchRotation * launchSpeed;
    }
}
