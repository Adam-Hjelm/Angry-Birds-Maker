using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public DragHandler dragHandler;

    // Start is called before the first frame update
    void Start()
    {
        dragHandler = GameObject.Find("DragHandler").GetComponent<DragHandler>();

        if (DragHandler.BuildMode)
            EnablePhysicsForObject(false);
    }

    private void Update()
    {
        if (DragHandler.BuildMode)      // TODO: Put this in the function that activates or deactivates buildmode later!
            EnablePhysicsForObject(false);
        else if (!DragHandler.BuildMode)
            EnablePhysicsForObject(true);
    }

    public void EnablePhysicsForObject(bool isEnabled)
    {
        //Debug.Log("physics disabled!");
        GetComponent<Collider2D>().isTrigger = !isEnabled;
        GetComponent<CollisionDamage>().enabled = isEnabled; // TODO: Have collision damage be turned off a second at the start to avoid objects breaking when physics is enabled
        if (isEnabled)
            GetComponent<Rigidbody2D>().gravityScale = 1;
        else
            GetComponent<Rigidbody2D>().gravityScale = 0;

        //GetComponent<Rigidbody2D>().isKinematic = !isEnabled;
    }

    private void OnDestroy()
    {
        if (dragHandler.lastDragged == this)
        {
            dragHandler.isDragging = false;
            dragHandler.lastDragged = null;
        }
    }
}
