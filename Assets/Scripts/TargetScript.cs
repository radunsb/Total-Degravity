using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    HumanTutorialScript _hts;
    // Start is called before the first frame update
    void Start()
    {
        _hts = GameObject.FindObjectOfType<HumanTutorialScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NetProjectile"))
        {
            _hts.targetHit();
            Destroy(gameObject);
        }
    }
    
}
