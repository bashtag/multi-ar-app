using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoricalSceneCanvasManager : MonoBehaviour
{
    public Canvas historicalSceneCanvas;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "HistoricalDesignScene")
        {
            historicalSceneCanvas.gameObject.SetActive(true);
            GroundObjectPlacer.objectIndex = 0;
        }
        else
        {
            historicalSceneCanvas.gameObject.SetActive(false);
        }
    }
}
