using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GroundObjectPlacer : DefaultObserverEventHandler
{
    [SerializeField]
    private GameObject[] objectPrefabs;  // Array to hold the 4 prefabs
    public static int objectIndex = 0;  // Tracks which object in the array is next to be placed
    private LayerMask touchableLayers;
    private static GameObject selectedObject = null;   // The object being dragged

    // UI buttons for rotating and moving objects
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public Button moveUpButton;
    public Button moveDownButton;
    public Button moveLeftButton;
    public Button moveRightButton;
    
    protected override void Start()
    {
        base.Start();

        touchableLayers = LayerMask.GetMask("Touchable");
        // objectIndex = 0;

        // Add listeners to the UI buttons
        rotateLeftButton.onClick.AddListener(() => RotateObject(-10f));
        rotateRightButton.onClick.AddListener(() => RotateObject(10f));
        moveUpButton.onClick.AddListener(() => MoveObject(Vector3.up));
        moveDownButton.onClick.AddListener(() => MoveObject(Vector3.down));
        moveLeftButton.onClick.AddListener(() => MoveObject(Vector3.left));
        moveRightButton.onClick.AddListener(() => MoveObject(Vector3.right));
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        // If we have placed all objects, don't place any more
        if (objectIndex >= objectPrefabs.Length)
        {
            Debug.Log("All objects have been placed.");
            return;
        }

        // Place the current object in the array at the detected ground plane location
        if (objectPrefabs[objectIndex] != null)
        {
            GameObject newPrefab = Instantiate(objectPrefabs[objectIndex], transform.position, transform.rotation);
            newPrefab.layer = LayerMask.NameToLayer("Touchable");
            Debug.Log($"Placed {objectPrefabs[objectIndex].name} at position {transform.position}");

            selectedObject = newPrefab;
            
            // Move to the next object in the array for the next placement
            Debug.Log($"Index: {objectIndex}");
            ++objectIndex;
            Debug.Log($"Index(after inc): {objectIndex}");
        }
    }

    void Update()
    {
        // Handle touch interactions to select an object for manipulation
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            Debug.Log(ray.origin);
            Debug.Log(ray.direction);
            Debug.Log("It is just a touch");

            // Check if the ray hits any collider from the touchable layer
            // if (Physics.Raycast(ray, out RaycastHit hit, 100f, touchableLayers))
            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                Debug.Log($"Raycast hit: {hit.transform.name}");
                LogTransform("Hit", hit.transform);
                // LogTransform("gameobject:", hit.transform.gameObject);

                // If the object hit is in the touchable layer, set it as the selected object
                if (hit.transform.gameObject != null && hit.transform.gameObject.name != "Ground Plane Stage")
                {
                    selectedObject = hit.transform.gameObject;  // Store the selected object
                    Debug.Log("(it is a hit)Selected object: " + selectedObject.name);
                }
            }
        }
    }

    // Method to rotate the selected object
    private void RotateObject(float angle)
    {
        if (selectedObject != null)
        {
            selectedObject.transform.Rotate(Vector3.up, angle);  // Rotate around the Y-axis
            Debug.Log($"Rotated {selectedObject.name} by {angle} degrees.");
        }
        else
        {
            Debug.Log("No object selected for rotation.");
        }
    }

    // Method to move the selected object
    private void MoveObject(Vector3 direction)
    {
        if (selectedObject != null)
        {
            selectedObject.transform.position += direction * 0.01f;  // Adjust movement speed as needed
            Debug.Log($"Moved {selectedObject.name} to {selectedObject.transform.position}");
        }
        else
        {
            Debug.Log("No object selected for movement.");
        }
    }

    private void LogTransform(string label, Transform transform)
    {
        Debug.LogWarning($"{label} - Position: {transform.position}, Rotation: {transform.rotation.eulerAngles}, Scale: {transform.localScale}");
    }
}
