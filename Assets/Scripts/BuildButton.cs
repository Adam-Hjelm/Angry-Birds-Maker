using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public Sprite[] buildBlockButtonSprites;
    public Image buttonImage;
    public GameObject[] buildObjects;
    public DragHandler dragHandler;

    public int currentMatNumber;
    public bool rotated;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SpawnBuildingBlock()
    {
        GameObject buildObject = Instantiate(buildObjects[currentMatNumber]);

        if (rotated)
        {
            buildObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            buildObject.GetComponent<PhysicsObject>().isRotatedIndex = 1;
        }
        dragHandler.lastDragged = buildObject.GetComponent<DraggableObject>();
        dragHandler.isDragging = true;
    }

    private void OnMouseDown()
    {
        SpawnBuildingBlock();
    }
}
