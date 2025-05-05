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
        if (!collision.gameObject.CompareTag("Human"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Human"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<MonkeScript>().getNetted();
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
