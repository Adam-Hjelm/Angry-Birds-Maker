using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneTransitionHandler : MonoBehaviour
{
    private static SceneTransitionHandler _instance;
    public static SceneTransitionHandler Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GoToLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void GoToBuildLevelScene()
    {
        DragHandler.BuildMode = true;
        SceneManager.LoadScene("BuildLevel");
    }

    public void AddListenerOnReturnButton()
    {
        var button = GameObject.Find("ReturnToMenuButton").GetComponent<Button>();
        button.onClick.AddListener(GoToMainMenuScene);
    }

    //public void GoToMyLevelsScene()
    //{
    //    //SceneManager.LoadScene("MyLevels");
    //}

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has quit!");
    }
}
