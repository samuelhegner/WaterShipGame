using UnityEngine;
public interface ITouchable
{
    void OnTouchDown(Vector3 touchPointInWorldSpace);
    void OnTouchHeld(Vector3 touchPointInWorldSpace);
    void OnTouchRelease(Vector3 touchPointInWorldSpace);
}
