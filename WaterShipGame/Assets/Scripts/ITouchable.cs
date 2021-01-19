using System.Collections;
using UnityEngine;

public interface ITouchable
{
    void OnTouchDown();
    void OnTouchHeld();
    void OnTouchRelease();
}
