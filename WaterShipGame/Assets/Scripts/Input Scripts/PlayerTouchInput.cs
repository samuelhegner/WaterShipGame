using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchInput : MonoBehaviour
{
    [SerializeField] Camera gameCamera;

    [SerializeField] LayerMask touchableObjectsMask;

    [SerializeField] float rayCastMaximumDistance = 100f;

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch currentTouch = Input.touches[i];

            RaycastHit hit;

            ITouchable touchedObject = checkIfTouchableObjectWasTouched(currentTouch, out hit);

            if (touchedObject != null)
            {
                ProcessTouchedObject(currentTouch, touchedObject, hit);
            }
        }
    }

    ITouchable checkIfTouchableObjectWasTouched(Touch touch, out RaycastHit hit)
    {
        bool touchableObjectHit = rayCastInTouchDirection(touch.position, out hit);

        if (touchableObjectHit)
        {
            return hit.transform.GetComponent<ITouchable>();
        }
        else 
        {
            return null;
        }
    }

    private bool rayCastInTouchDirection(Vector3 touchScreenPosition, out RaycastHit hit)
    {
        Ray rayFromCameraToTouchInWorldPosition = gameCamera.ScreenPointToRay(touchScreenPosition);
        return Physics.Raycast(rayFromCameraToTouchInWorldPosition, out hit, rayCastMaximumDistance, touchableObjectsMask);
    }

    private void ProcessTouchedObject(Touch currentTouch, ITouchable touchedObject, RaycastHit hit)
    {
        switch (currentTouch.phase)
        {
            case TouchPhase.Began:
                touchedObject.OnTouchDown(hit.point);
                break;
            case TouchPhase.Moved:
                touchedObject.OnTouchHeld(hit.point);
                break;
            case TouchPhase.Stationary:
                touchedObject.OnTouchHeld(hit.point);
                break;
            case TouchPhase.Ended:
                touchedObject.OnTouchRelease(hit.point);
                break;
            default:
                break;
        }
    }
}
