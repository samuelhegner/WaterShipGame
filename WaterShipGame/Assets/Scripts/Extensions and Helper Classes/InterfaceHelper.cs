using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InterfaceHelper
{
    public static List<T> FindInterfacesOfType<T>(GameObject objectToFindInterfaceIn)
    {
        List<T> interfaces = new List<T>();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        T[] childrenInterfaces = objectToFindInterfaceIn.GetComponentsInChildren<T>();
        
        for (int i = 0; i < childrenInterfaces.Length; i++)
        {
            interfaces.Add(childrenInterfaces[i]);
        } 

        return interfaces;
    }

}
