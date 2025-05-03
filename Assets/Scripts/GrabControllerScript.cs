using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrabScript : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform holdArea;

    public Transform leftArm;
    public Transform rightArm;

    public float armSpeed;

    public Vector3 rArmRestPosition;
    public Vector3 rArmRestRotation;
    public Vector3 rArmRestScale;

    public Vector3 rArmGrabPosition;
    public Vector3 rArmGrabRotation;
    public Vector3 rArmGrabScale;

    private GameObject heldObject;
    private Rigidbody heldObjectRBody;

    private float pickupRange = 5.0f;
    private float pickupForce = 150.0f;
    private float throwForce = 20;

    private bool _grabbing = false;

    private void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (heldObject != null)
        {
            MoveObject();
            MoveArmsToGrab();
        }
        else
        {
            MoveArmsToRest();
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
                    if (hit.transform.gameObject.CompareTag("Grabbable") || hit.transform.gameObject.CompareTag("Player"))
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
        heldObject.transform.localPosition = Vector3.zero;
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

            if (!grabbedObject.CompareTag("Player"))
            {
                heldObject.transform.position += grabbedObject.GetComponent<GrabbableObjectScript>().additionalHoldDistance;
            }
        }
    }

    void DropObject()
    {
        // heldObjectRBody.useGravity = true;
        heldObjectRBody.drag = 0;
        heldObjectRBody.constraints = RigidbodyConstraints.None;
        heldObjectRBody.AddForce(cameraTransform.forward * throwForce, ForceMode.Impulse);
        heldObject.transform.parent = null;
        heldObject = null;
    }

    void MoveArmsToGrab()
    {
        rightArm.localPosition = Vector3.Slerp(rightArm.localPosition, rArmGrabPosition, armSpeed);
        rightArm.localEulerAngles = rArmGrabRotation;
        rightArm.localScale = Vector3.Slerp(rightArm.localScale, rArmGrabScale, armSpeed);

        Vector3 lArmGrabScale = new Vector3(-1 * rArmGrabScale.x, rArmGrabScale.y, rArmGrabScale.z);

        leftArm.localPosition = Vector3.Slerp(leftArm.localPosition, rArmGrabPosition, armSpeed);
        leftArm.localEulerAngles = rArmGrabRotation;
        leftArm.localScale = Vector3.Slerp(leftArm.localScale, lArmGrabScale, armSpeed);
    }

    void MoveArmsToRest()
    {
        rightArm.localPosition = Vector3.Slerp(rightArm.localPosition, rArmRestPosition, armSpeed);
        rightArm.localEulerAngles = rArmRestRotation;
        rightArm.localScale = Vector3.Slerp(rightArm.localScale, rArmRestScale, armSpeed);

        Vector3 lArmRestScale = new Vector3(-1 * rArmRestScale.x, rArmRestScale.y, rArmRestScale.z);

        leftArm.localPosition = Vector3.Slerp(leftArm.localPosition, rArmRestPosition, armSpeed);
        leftArm.localEulerAngles = rArmRestRotation;
        leftArm.localScale = Vector3.Slerp(leftArm.localScale, lArmRestScale, armSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sword") && heldObject != null)
        {
            DropObject();
        }
    }
}