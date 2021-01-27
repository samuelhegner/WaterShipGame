using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePopper: MonoBehaviour, ITouchable, IDestroyable
{
    public void OnTouchDown(Vector3 touchPointInWorldSpace)
    {
        popBubble();
    }

    public void OnTouchHeld(Vector3 touchPointInWorldSpace)
    {
        popBubble();
    }

    private void popBubble()
    {
        onDestroy();
    }





    public void OnTouchRelease(Vector3 touchPointInWorldSpace)
    {
        //throw new System.NotImplementedException();
    }

    public void onDestroy()
    {
        Destroy(gameObject);
    }
}
