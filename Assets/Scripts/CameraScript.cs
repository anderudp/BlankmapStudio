using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    // Camera dragging
    private Vector3 mouseOrigin;
    private Vector3 mouseDifference;
    private bool isDragging = false;

    // Zoom
    private Camera mainCamera;
    private static float maxCameraSize = 18.64682f; // maximum zoomed out
    private static float minCameraSize = 5f; // maximum zoomed in
    private static float cameraZoom = maxCameraSize / -25f;
    private float scrollDelta = 0f;

    // Corners of camera (min: bottom left, max: top right)
    private Vector3 currentMax;
    private Vector3 currentMin;
    private Vector3 currentPos;
    private Vector3 absoluteMax;
    private Vector3 absoluteMin;
    private (Vector3, Vector3) minmax;

    void Start() 
    {
        Screen.SetResolution(1134, 1024, false);
        mainCamera = GetComponent<Camera>();
        mainCamera.orthographicSize = maxCameraSize;
        

        minmax = OrthographicBounds();

        absoluteMin = minmax.Item1;
        absoluteMax = minmax.Item2;
    }

    void LateUpdate () 
    {
        AdjustZoom(); // Zoom in or out
        AdjustPosition(); // Handle camera dragging
        RectifyPosition(); // Return camera within bounds, if either of the above brought it out
    }

    private (Vector3, Vector3) OrthographicBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        Bounds bounds = new Bounds(mainCamera.transform.position, 
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return (bounds.min, bounds.max);
    }

    private void AdjustZoom()
    {
        scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0f)
        {
            if (mainCamera.orthographicSize > minCameraSize && mainCamera.orthographicSize < maxCameraSize)
            {
                mainCamera.orthographicSize += scrollDelta > 0f ? cameraZoom : -cameraZoom;
            }
            else if(mainCamera.orthographicSize >= maxCameraSize && scrollDelta > 0f)
            {
                mainCamera.orthographicSize += cameraZoom;
            }
            else if(mainCamera.orthographicSize <= minCameraSize && scrollDelta < 0f)
            {
                mainCamera.orthographicSize -= cameraZoom;
            }
        }
    }

    private void AdjustPosition()
    {
        if(Input.GetMouseButton(1)) 
        {
            mouseDifference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position;
            if (isDragging == false)
            {
                isDragging = true;
                mouseOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        } 
        else isDragging = false;

        if (isDragging == true) 
        {
            mainCamera.transform.position = mouseOrigin - mouseDifference;
        }
    }

    private void RectifyPosition()
    {
        minmax = OrthographicBounds();

        currentMin = minmax.Item1;
        currentMax = minmax.Item2;

        currentPos = mainCamera.transform.position;

        if(currentMax.x > absoluteMax.x) currentPos.x -= currentMax.x - absoluteMax.x;
        if(currentMax.y > absoluteMax.y) currentPos.y -= currentMax.y - absoluteMax.y;

        if(currentMin.x < absoluteMin.x) currentPos.x += absoluteMin.x - currentMin.x;
        if(currentMin.y < absoluteMin.y) currentPos.y += absoluteMin.y - currentMin.y;
        mainCamera.transform.position = currentPos;
    }
}
