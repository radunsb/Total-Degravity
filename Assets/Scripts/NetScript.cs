using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NetScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(Deactivate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
