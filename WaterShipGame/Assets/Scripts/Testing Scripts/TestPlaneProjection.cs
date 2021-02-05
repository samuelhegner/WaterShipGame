using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlaneProjection : MonoBehaviour
{
    public Vector3 rayDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Vector3.zero, rayDirection * 10f, Color.red);
        Vector3 projectedVector = Vector3.ProjectOnPlane(rayDirection, Vector3.up);
        Debug.DrawRay(Vector3.zero, projectedVector.normalized * 10f, Color.green);

    }
}
