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

            ITouchable touchedObject = checkIfTouchableObjectWasTouched(currentTouch);

            if (touchedObject != null)
            {
                ProcessTouchedObject(currentTouch, touchedObject);
            }
        }
    }

    ITouchable checkIfTouchableObjectWasTouched(Touch touch)
    {
        RaycastHit hit;
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

    private void ProcessTouchedObject(Touch currentTouch, ITouchable touchedObject)
    {
        switch (currentTouch.phase)
        {
            case TouchPhase.Began:
                touchedObject.OnTouchDown();
                break;
            case TouchPhase.Moved:
                touchedObject.OnTouchHeld();
                break;
            case TouchPhase.Stationary:
                touchedObject.OnTouchHeld();
                break;
            case TouchPhase.Ended:
                touchedObject.OnTouchRelease();
                break;
            default:
                break;
        }
    }
}
