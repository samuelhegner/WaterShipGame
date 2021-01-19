using UnityEngine;


public class CubeColourChanger : MonoBehaviour, ITouchable
{
    public void OnTouchDown()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        Debug.Log("Touch: Start");
    }

    public void OnTouchHeld()
    {
        Debug.Log("Touch: Held");
    }

    public void OnTouchRelease()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        Debug.Log("Touch: Ended");
    }
}
