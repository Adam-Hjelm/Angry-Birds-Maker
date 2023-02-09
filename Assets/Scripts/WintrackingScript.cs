using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WintrackingScript : MonoBehaviour
{
    private int remainingEnemies = 0;

    public bool CheckIfMatchWon()
    {
        remainingEnemies = 0;
        CollisionDamage[] collisionDamageScripts = FindObjectsOfType<CollisionDamage>();

        for (int i = 0; i < collisionDamageScripts.Length; i++)
        {
            if (collisionDamageScripts[i].currentObjectState == CollisionDamage.ObjectState.Enemy)
                remainingEnemies++;
        }

        Debug.Log(remainingEnemies);

        return remainingEnemies == 0;
    }
}
