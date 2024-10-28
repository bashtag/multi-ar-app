using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPictureSceneCanvasManager : MonoBehaviour
{
    public Canvas myPictureSceneCanvas;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MyPictureScene")
        {
            myPictureSceneCanvas.gameObject.SetActive(true);
        }
        else
        {
            myPictureSceneCanvas.gameObject.SetActive(false);
        }
    }
}
