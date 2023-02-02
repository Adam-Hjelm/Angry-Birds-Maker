using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public Sprite[] buildBlockButtonSprites;
    public Image buttonImage;
    public int currentMatNumber;

    public GameObject[] buildObjects;

    public DragHandler dragHandler;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SpawnBuildingBlock()
    {
        GameObject buildObject = Instantiate(buildObjects[currentMatNumber]);
        //buildObject.GetComponent<BuildBlock>().isDragged = true;

        dragHandler.lastDragged = buildObject.GetComponent<DraggableObject>();
        dragHandler.isDragging = true;
    }
}
