using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrabScript : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform holdArea;
    private GameObject heldObject;
    private Rigidbody heldObjectRBody;

    private float pickupRange = 5.0f;
    private float pickupForce = 150.0f;

    private bool _grabbing = false;

    private void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (heldObject != null)
        {
            MoveObject();
        }
    }

    void OnGrab(InputValue value)
    {
        _grabbing = !_grabbing;

        if (_grabbing)
        {
            if (heldObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    if (hit.transform.gameObject.CompareTag("Grabbable"))
                    {
                        PickupObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                DropObject();
            }
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - holdArea.transform.position);

            heldObjectRBody.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject grabbedObject)
    {
        if (grabbedObject.GetComponent<Rigidbody>())
        {
            heldObjectRBody = grabbedObject.GetComponent<Rigidbody>();
            // heldObjectRBody.useGravity = false;
            heldObjectRBody.drag = 10;
            heldObjectRBody.constraints = RigidbodyConstraints.FreezeRotation;

            heldObject = grabbedObject;
            heldObject.transform.parent = holdArea;
        }
    }

    void DropObject()
    {
        // heldObjectRBody.useGravity = true;
        heldObjectRBody.drag = 1;
        heldObjectRBody.constraints = RigidbodyConstraints.None;

        heldObject.transform.parent = null;
        heldObject = null;
    }
}