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
    public float _lastLaunchTime = int.MinValue;

    public float launchDelay;

    public Material normalMat;
    public Material chargeMat;

    public MeshRenderer[] mehses;

    AudioSource _as;
    public AudioClip _whoosh;

    private void Start()
    {
        _as = GameObject.FindWithTag("SFXsource").GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (Time.time > _lastLaunchTime + launchCooldown)
        {
            _lastLaunchTime = Time.time;
            StartCoroutine(launchThing());
            _as.PlayOneShot(_whoosh);
        }
    }

    private IEnumerator launchThing()
    {

        updateMaterials(chargeMat);
        yield return new WaitForSeconds(launchDelay);
        updateMaterials(normalMat);
        

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

    private void updateMaterials(Material mat)
    {
        foreach (MeshRenderer meh in mehses)
        {
            meh.material = mat;
        }
    }
}
