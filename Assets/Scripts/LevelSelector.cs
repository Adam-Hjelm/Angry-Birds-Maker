using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelsListHolder;
    public Button buttonPrefab;

    public GameObject[] woodObjects;
    public GameObject[] stoneObjects;
    public GameObject[] metalObjects;
    public GameObject enemyObject;

    public GameObject levelListCanvas;
    public GameObject levelParts;

    void Start()
    {
        ListGames();
    }

    void Update()
    {

    }

    public void ListGames()
    {
        levelListCanvas.SetActive(true);
        Debug.Log("Listing Games");

        foreach (Transform child in levelsListHolder.transform)
            GameObject.Destroy(child.gameObject);

        FirebaseSaveManager.Instance.LoadMultipleData<Level>("games/levels", ShowGames);
    }

    public void ShowGames(List<Level> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            var newButton = Instantiate(buttonPrefab, levelsListHolder.transform).GetComponent<Button>();
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = data[i]._name;

            var currentLevel = data[i];
            newButton.onClick.AddListener(() => LoadLevel(currentLevel));
            Debug.Log(currentLevel);
        }
    }

    private void LoadLevel(Level level)
    {
        levelListCanvas.SetActive(false);
        levelParts.SetActive(true);

        SceneTransitionHandler.Instance.AddListenerOnReturnButton();

        var levelGO = level.levelObjects;

        for (int i = 0; i < levelGO.Count; i++)
        {
            GameObject[] chosenObjectMat = null;
            GameObject chosenObject = null;

            switch (levelGO[i].objectMatIndex)
            {
                case 0:
                    chosenObject = enemyObject;
                    break;
                case 1:
                    chosenObjectMat = woodObjects;
                    break;
                case 2:
                    chosenObjectMat = stoneObjects;
                    break;
                case 3:
                    chosenObjectMat = metalObjects;
                    break;

                default:
                    break;
            }
            if (chosenObject != enemyObject)
            {
                chosenObject = chosenObjectMat[levelGO[i].objectFormIndex];
            }

            GameObject newlevelObj = Instantiate(chosenObject, levelGO[i].position, Quaternion.identity);

            if (levelGO[i].isRotated == 1)
            {
                newlevelObj.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
        DragHandler.BuildMode = false;
    }
}
