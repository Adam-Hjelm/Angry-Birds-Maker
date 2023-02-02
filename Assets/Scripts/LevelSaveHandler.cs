using System;
using System.Collections.Generic;
using UnityEngine;

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
            FirebaseSaveManager.Instance.LoadMultipleData<LevelObject>("games", LoadResult);
        }
    }

    private void LoadResult(List<LevelObject> data)
    {
        foreach (var item in data)
        {
            Debug.Log(item.position);
        }
    }
}
