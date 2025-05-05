using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class ObjectiveArrowScript : MonoBehaviour
{
    Camera _cam;
    RectTransform arrowTrans;
    Transform objective;

    public string arrowTag;
    public string objectiveTag;

    public float arrowSpeed;

    public int screenID;

    //right, bottom, left, top
    Vector4 edgePositions;

    float screenWidth;
    float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        objective = GameObject.FindWithTag(objectiveTag).transform;
        arrowTrans = GameObject.FindWithTag(arrowTag).GetComponent<RectTransform>();

        _cam = GetComponent<Camera>();

        screenWidth = _cam.pixelWidth;
        screenHeight = _cam.pixelHeight;


        if (screenID == 0)
        {
            //right, bottom, left, top
            edgePositions.w = screenWidth - arrowTrans.rect.width / 2;
            edgePositions.x = arrowTrans.rect.height * 1.5f;
            edgePositions.y = arrowTrans.rect.width / 2;
            edgePositions.z = screenHeight - (arrowTrans.rect.height * 1.5f);
        }
        else
        {
            edgePositions.w = 2 * screenWidth - arrowTrans.rect.width / 2;
            edgePositions.x = arrowTrans.rect.height * 1.5f;
            edgePositions.y = screenWidth + arrowTrans.rect.width / 2;
            edgePositions.z = screenHeight - (arrowTrans.rect.height * 1.5f);
        }

        Debug.Log(edgePositions);
    }

    // Update is called once per frame
    void Update()
    {
        //don't do anything unless you can find the objective 
        if(objective == null)
        {
            objective = GameObject.FindWithTag(objectiveTag).transform;
            return;
        }

        Vector3 objectivePosition = _cam.WorldToScreenPoint(objective.position);
        if(screenID == 1)
        {
            objectivePosition = new Vector3(objectivePosition.x - screenWidth, objectivePosition.y, objectivePosition.z);
            //print(objectivePosition);
        }
        bool offRight = objectivePosition.x > screenWidth;
        bool offLeft = objectivePosition.x < 0;
        bool offBottom = objectivePosition.y < 0;
        bool offTop = objectivePosition.y > screenHeight;

        bool onScreen = !offRight && !offLeft && !offTop && !offBottom && objectivePosition.z > 0;



        arrowTrans.gameObject.SetActive(!onScreen);

        if (arrowTrans.gameObject.activeSelf)
        {
            float xTarget;
            float yTarget;

            //rotate arrow to point at objective
            Vector2 arrowPosition = _cam.WorldToScreenPoint(arrowTrans.position);

            //rotate arrow to point at the objective. z=0 means right, z=90 means up
            float angleBetween = Vector2.Angle(objectivePosition, Vector2.right);
            if(offBottom) angleBetween = -angleBetween;

            arrowTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, angleBetween));

            //edgePositions stores (rightEdge, bottomEdge, leftEdge, topEdge)
            if (offRight) xTarget = edgePositions.w;
            else if (offLeft) xTarget = edgePositions.y;
            else xTarget = screenID*screenWidth + screenWidth / 2;

            if (offTop) yTarget = edgePositions.z;
            else if (offBottom) yTarget = edgePositions.x;
            else yTarget = screenHeight / 2;

            arrowTrans.position = Vector3.Slerp(arrowTrans.position, new Vector3(xTarget, yTarget, 0), arrowSpeed);
        }
    }
}
