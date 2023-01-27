using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public Sprite[] buildBlockButtonSprites;
    public Image buttonImage;
    public int matNumberToChangeTo;

    public GameObject[] buildObjects;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }
}
