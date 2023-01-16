using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isAirborne;

    public bool triggerAbility;

    public int abilityUses = 1;

    void Start()
    {

    }

    void Update()
    {
        if (isAirborne && Input.GetMouseButtonDown(0) && abilityUses > 0)
        {
            triggerAbility = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isAirborne = false;
    }
}
