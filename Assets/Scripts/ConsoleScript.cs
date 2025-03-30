using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleScript : MonoBehaviour
{
    public ShieldScript shield;

    public Material dullMat;

    public MeshRenderer columnRibMR;

    private bool _active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (_active && other.CompareTag("Sword"))
        {
            _active = false;

            GetComponent<Renderer>().materials = new Material[] { dullMat};
            columnRibMR.materials = new Material[] { dullMat };

            if (shield != null)
            {
                shield.takeConsoleDestroyDamage();
            }
            //TODO: add a cool explosion here
        }
    }
}