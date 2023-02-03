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
    public GameObject[] metalbjects;

    void Start()
    {
        ListGames();
    }

    void Update()
    {

    }

    public void ListGames()
    {
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
            //newButton.onClick.AddListener(() => JoinGame(gameInfo)); TODO: Make a function that loads the level on click! c:

            var currentLevel = data[i];
            newButton.onClick.AddListener(() => LoadLevel(currentLevel));
            Debug.Log(currentLevel);
        }

        //var gameInfo = JsonUtility.FromJson<GameInfo>(json);

        //if (userInfo.activeGames.Contains(gameInfo.gameID) || gameInfo.players.Count > 1) Not needed
        //{
        //    //Don't list our own games or full games.
        //    return;
        //}


    }

    private void LoadLevel(Level level)
    {
        var levelGO = level.levelObjects;

        for (int i = 0; i < levelGO.Count; i++)
        {
            GameObject[] chosenObjectMat = null;

            switch (levelGO[i].objectMatIndex)
            {
                case 0:
                    chosenObjectMat = woodObjects;
                    break;
                case 1:
                    chosenObjectMat = stoneObjects;
                    break;
                case 2:
                    chosenObjectMat = metalbjects;
                    break;

                default:
                    break;
            }
            GameObject chosenObject = chosenObjectMat[levelGO[i].objectFormIndex];

            Instantiate(chosenObject, levelGO[i].position, Quaternion.identity);
        }
    }
}
