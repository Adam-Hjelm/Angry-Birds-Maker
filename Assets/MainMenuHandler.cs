using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    public void GoToLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void GoToBuildLevelScene()
    {
        DragHandler.BuildMode = true;
        SceneManager.LoadScene("BuildLevel");
    }

    public void GoToMyLevelsScene()
    {
        //SceneManager.LoadScene("MyLevels");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has quit!");
    }
}
