using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    public Vector2 interactPointPos;

    public Vector3 worldPos;

    public DraggableObject lastDragged;

    public bool isDragging;
    public static bool BuildMode = true;

    void Start()
    {

    }

    void Update()
    {
        if (isDragging && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            DropObject();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            interactPointPos = new Vector2(mousePosition.x, mousePosition.y);

            //transform.position = Camera.main.ScreenToWorldPoint(interactPointPos);
        }
        else if (Input.touchCount > 0)
        {
            interactPointPos = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        worldPos = Camera.main.ScreenToWorldPoint(interactPointPos);

        if (isDragging)
            DragObject();

        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector3.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);

                DraggableObject draggable = hit.transform.gameObject.GetComponent<DraggableObject>();
                if (draggable != null)
                {
                    lastDragged = draggable;
                    StartDrag();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.G) && BuildMode == false)
        {
            BuildMode = true;
            Debug.Log("buildmode turned on");
        }
        else if (Input.GetKeyUp(KeyCode.G) && BuildMode)
        {
            BuildMode = false;
            Debug.Log("buildmode turned off");
        }
    }

    private void DropObject()
    {
        isDragging = false;
    }

    private void StartDrag()
    {
        isDragging = true;

    }

    private void DragObject()
    {
        lastDragged.transform.position = new Vector2(worldPos.x, worldPos.y);
    }
}
