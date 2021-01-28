using UnityEngine;
using System;
using UnityEngine.Assertions;

public class BubblePopper: MonoBehaviour, ITouchable, IDestroyable
{
    public Action bubblePopped;

    private void OnEnable()
    {
        Health health = GetComponent<Health>();
        Assert.IsNotNull(health);
        health.objectDestroyed += popBubble;
    }
    private void OnDisable()
    {
        Health health = GetComponent<Health>();
        Assert.IsNotNull(health);
        health.objectDestroyed -= popBubble;
    }
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
