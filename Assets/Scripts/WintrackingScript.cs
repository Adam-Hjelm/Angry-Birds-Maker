using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WintrackingScript : MonoBehaviour
{
    private int remainingEnemies = 0;
    private GameObject winText;

    public void CheckIfMatchWon()
    {
        remainingEnemies = 0;
        CollisionDamage[] collisionDamageScripts = FindObjectsOfType<CollisionDamage>();

        for (int i = 0; i < collisionDamageScripts.Length; i++)
        {
            if (collisionDamageScripts[i].currentObjectState == CollisionDamage.ObjectState.Enemy)
                remainingEnemies++;
        }

        remainingEnemies -= 1;

        Debug.Log(remainingEnemies);

        if (remainingEnemies == 0)
        {
            winText = GameObject.Find("WinTextHolder");
            winText.transform.GetChild(0).gameObject.SetActive(true);

            Invoke(nameof(SceneTransitionHandler.Instance.GoToMainMenuScene), 3);
        }
        //return remainingEnemies == 0;
    }
}
