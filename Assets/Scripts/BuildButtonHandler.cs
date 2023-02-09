using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
                materialNumber = 3;
                break;
            default:
                break;
        }

        if (materialNumber == 3)
        {
            var firstBuildButtonScript = gameObject.transform.GetChild(0).GetComponent<BuildButton>();
            for (int i = 0; i < buildButtonScripts.Length; i++)
            {
                buildButtonScripts[i].gameObject.SetActive(false);
            }
            firstBuildButtonScript.gameObject.SetActive(true);
            firstBuildButtonScript.buttonImage.sprite = firstBuildButtonScript.buildBlockButtonSprites[materialNumber];
            firstBuildButtonScript.currentMatNumber = materialNumber;

            return;
        }

        for (int i = 0; i < buildButtonScripts.Length; i++)
        {
            buildButtonScripts[i].gameObject.SetActive(true);
            buildButtonScripts[i].buttonImage.sprite = buildButtonScripts[i].buildBlockButtonSprites[materialNumber];
            buildButtonScripts[i].currentMatNumber = materialNumber;
        }
    }

    public void RotateBuildingObj()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 90);

        //Vector2 gridCellSize = gameObject.GetComponent<GridLayoutGroup>().cellSize;

        for (int i = 0; i < buildButtonScripts.Length; i++)
        {
            if (buildButtonScripts[i].rotated != true)
            {
                buildButtonScripts[i].transform.rotation = rotation;
                buildButtonScripts[i].rotated = true;
                //gridCellSize.y = 50;
            }
            else
            {
                buildButtonScripts[i].transform.rotation = Quaternion.identity;
                buildButtonScripts[i].rotated = false;
                //gridCellSize.y = 60;
            }
        }
        //GetComponent<GridLayoutGroup>().cellSize = gridCellSize;
    }

    public void UploadLevel()
    {

    }
}
