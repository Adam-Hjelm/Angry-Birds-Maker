using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;

[Serializable]
public class Level
{
    public string _name;
    public List<LevelObject> levelObjects;
}

[Serializable]
public class LevelObject
{
    public int objectFormIndex;
    public int objectMatIndex;
    public int isRotated;
    public Vector3 position;
}

public class LevelSaveHandler : MonoBehaviour
{
    public TMP_InputField levelNameInputField;

    public void UploadLevel()
    {
        Debug.Log("Uploading Level");
        var level = new Level();
        level.levelObjects = new List<LevelObject>();

        level._name = levelNameInputField.text;

        for (int i = 0; i < transform.childCount; i++)
        {
            var saveObject = new LevelObject();
            saveObject.position = transform.GetChild(i).transform.position;
            saveObject.objectFormIndex = transform.GetChild(i).GetComponent<PhysicsObject>().shapeIndex;
            saveObject.objectMatIndex = (int)transform.GetChild(i).GetComponent<PhysicsObject>().currentObjectState;
            saveObject.isRotated = transform.GetChild(i).GetComponent<PhysicsObject>().isRotatedIndex;

            level.levelObjects.Add(saveObject);
        }
        if (level._name == null || level._name == "")
        {
            level._name = "Default Level Name";
        }
        FirebaseSaveManager.Instance.PushData("games/levels", JsonUtility.ToJson(level));

        Debug.Log("Level Uploaded");
    }

    private void LoadResultDebug(DataSnapshot snapshot)
    {
        //foreach (var item in data)
        //{
        //    Debug.Log(item.position);
        //}
    }

    private void LoadResultMultipleDebug(List<LevelObject> data) // For loading multiple objects
    {
        foreach (var item in data)
        {
            Debug.Log(item.position);
        }
    }
}
