using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraManagerScript : MonoBehaviour
{
    private static ARCameraManagerScript instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
