using System;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    public Action OnDragComplete;

    private Vector3 offset;
    private Camera cam;
    private bool isDragging = false;  // Flag to track if the piece is being dragged

    void Start()
    {
        cam = Camera.main;  // Get the main camera reference
    }

    void Update()
    {
        // Check for touch or mouse input depending on the platform
        if (isDragging)
        {
            // Handle dragging for both touch and mouse
            Vector3 currentPosition = GetInputPosition();
            transform.position = currentPosition + offset;
        }
    }

    void OnMouseDown()
    {
        if (IsTouchOrMouseDown())
        {
            // Start dragging the piece when touch or mouse button is pressed
            offset = transform.position - GetInputPosition();
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        if (IsTouchOrMouseUp())
        {
            // Stop dragging the piece when touch or mouse button is released
            isDragging = false;
            OnDragComplete?.Invoke();
        }
    }

    Vector3 GetInputPosition()
    {
        Vector3 inputPosition;
        if (Application.isMobilePlatform)
        {
            // Use touch input for mobile devices
            if (Input.touchCount > 0)
            {
                inputPosition = Input.GetTouch(0).position;
                inputPosition.z = cam.WorldToScreenPoint(transform.position).z;  // Keep depth same as the object
            }
            else
            {
                inputPosition = Vector3.zero;
            }
        }
        else
        {
            // Use mouse input for desktop
            inputPosition = Input.mousePosition;
            inputPosition.z = cam.WorldToScreenPoint(transform.position).z;
        }

        // Convert screen position to world position
        return cam.ScreenToWorldPoint(inputPosition);
    }

    bool IsTouchOrMouseDown()
    {
        if(!enabled) return false;

        // Handle mouse down (desktop) or touch down (mobile)
        if (Application.isMobilePlatform)
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
        else
        {
            return Input.GetMouseButtonDown(0);  // Mouse down for desktop
        }
    }

    bool IsTouchOrMouseUp()
    {
        if(!enabled) return false;

        // Handle mouse up (desktop) or touch up (mobile)
        if (Application.isMobilePlatform)
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        }
        else
        {
            return Input.GetMouseButtonUp(0);  // Mouse up for desktop
        }
    }
}
