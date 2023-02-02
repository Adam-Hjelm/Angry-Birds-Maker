using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseSaveManager : MonoBehaviour
{
    //Singleton variables
    private static FirebaseSaveManager _instance;
    public static FirebaseSaveManager Instance { get { return _instance; } }

    //Function that gets called after load or save
    public delegate void OnLoadedDelegate(DataSnapshot snapshot);
    public delegate void OnSaveDelegate();

    FirebaseDatabase db;

    private void Awake()
    {
        //Singleton setup
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        db = FirebaseDatabase.DefaultInstance;
        db.SetPersistenceEnabled(false); //Fix data cache problems
    }

    //loads the data at "path" then returns json result to the delegate/callback function
    public void LoadData(string path, OnLoadedDelegate onLoadedDelegate)
    {
        db.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Send our result (datasnapshot) to whom asked for it.
            onLoadedDelegate(task.Result);
        });
    }

    //Save the data at the given path, save callback optional
    public void SaveData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        db.RootReference.Child(path).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Call our delegate if it's not null
            onSaveDelegate?.Invoke();
        });
    }

    //Save the data at the given path, save callback optional
    public void PushData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        db.RootReference.Child(path).Push().SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Call our delegate if it's not null
            onSaveDelegate?.Invoke();
        });
    }


    public delegate void OnLoadedMultipleDelegate<T>(List<T> data); //list of objects

    //Returns one list of objects that we want to load from the database
    public void LoadMultipleData<T>(string path, OnLoadedMultipleDelegate<T> onLoadedDelegate)
    {
        db.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            var ListOfT = new List<T>();

            foreach (var item in task.Result.Children)
                ListOfT.Add(JsonUtility.FromJson<T>(item.GetRawJsonValue()));

            onLoadedDelegate(ListOfT);
        });
    }

    public void RemoveData(string path)
    {
        db.RootReference.Child(path).RemoveValueAsync();
    }
}