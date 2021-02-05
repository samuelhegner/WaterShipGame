using UnityEngine;


public class CubeColourChanger : MonoBehaviour, ITouchable
{
    public void OnTouchDown(Vector3 touchPointInWorldSpace)
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        Debug.Log("Touch: Start");
    }

    public void OnTouchHeld(Vector3 touchPointInWorldSpace)
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    public void OnTouchRelease(Vector3 touchPointInWorldSpace)
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        Debug.Log("Touch: Ended");
    }
}
