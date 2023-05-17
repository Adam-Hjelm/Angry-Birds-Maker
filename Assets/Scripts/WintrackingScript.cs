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
            StartCoroutine(ReturnToLevelSelect());
        }
        //return remainingEnemies == 0;
    }

    IEnumerator ReturnToLevelSelect()
    {
        yield return new WaitForSeconds(2.5f);
        SceneTransitionHandler.Instance.GoToMainMenuScene();
    }
}
