using UnityEngine;
using System;

public class BubblePopper: MonoBehaviour, ITouchable, IDestroyable
{
    public Action bubblePopped;

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
        bubblePopped?.Invoke();
        onDestroy();
    }

    public void OnTouchRelease(Vector3 touchPointInWorldSpace)
    {
    }

    public void onDestroy()
    {
        gameObject.SetActive(false);
    }
}
