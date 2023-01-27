using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuildButtonHandler : MonoBehaviour
{
    public BuildButton[] buildButtonScripts;

    public void ChangeBuildingMat()
    {
        int materialNumber = 0;
        string text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        switch (text)
        {
            case "Wood":
                materialNumber = 0;
                break;
            case "Stone":
                materialNumber = 1;
                break;
            case "Metal":
                materialNumber = 2;
                break;
            case "Misc":
                materialNumber = 2; // change later
                break;
            default:
                break;
        }

        for (int i = 0; i < buildButtonScripts.Length; i++)
        {
            buildButtonScripts[i].buttonImage.sprite = buildButtonScripts[i].buildBlockButtonSprites[materialNumber];
        }
    }
}
