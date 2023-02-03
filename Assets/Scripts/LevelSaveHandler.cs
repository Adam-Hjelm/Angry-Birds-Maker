using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

[Serializable]
public class Level
{
    public string _name; // TODO: Dont allow characters past a certain limit
    public List<LevelObject> levelObjects;
}

[Serializable]
public class LevelObject
{
    public int objectFormIndex;
    public int objectMatIndex;
    public Vector3 position;
}

public class LevelSaveHandler : MonoBehaviour
{
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var level = new Level();
            level.levelObjects = new List<LevelObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var saveObject = new LevelObject();
                saveObject.position = transform.GetChild(i).transform.position;
                saveObject.objectFormIndex = transform.GetChild(i).GetComponent<CollisionDamage>().shapeIndex;
                saveObject.objectMatIndex = (int)transform.GetChild(i).GetComponent<CollisionDamage>().currentObjectState;

                level.levelObjects.Add(saveObject);
            }
            if (level._name == null || level._name == "")
            {
                level._name = "Default Level Name";
            }
            FirebaseSaveManager.Instance.PushData("games/levels", JsonUtility.ToJson(level));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

            Debug.Log(userID);
            FirebaseSaveManager.Instance.LoadData("games/levels", LoadResult);
        }



        //if (Input.GetKeyDown(KeyCode.L)) //EXAMPLE FOR LOADING MULTIPLE OBJECTS
        //{
        //    FirebaseSaveManager.Instance.LoadMultipleData<LevelObject>("games", LoadResultMultiple);
        //}
    }

    private void LoadResult(DataSnapshot snapshot)
    {
        //foreach (var item in data)
        //{
        //    Debug.Log(item.position);
        //}
    }

    private void LoadResultMultiple(List<LevelObject> data) // For loading multiple objects
    {
        foreach (var item in data)
        {
            Debug.Log(item.position);
        }
    }
}
