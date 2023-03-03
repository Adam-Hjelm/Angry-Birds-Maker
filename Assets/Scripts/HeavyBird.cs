using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBird : MonoBehaviour
{
    public ParticleSystem weightParticleSys;

    void Update()
    {
        if (GetComponent<Projectile>().triggerAbility)
        {
            IncreaseMassAbility();
        }
    }

    private void IncreaseMassAbility()
    {
        var projectileRbody = GetComponent<Rigidbody2D>();
        var newWeightFX = Instantiate(weightParticleSys, transform.position, Quaternion.LookRotation(transform.up));
        Destroy(newWeightFX);

        projectileRbody.mass *= 5;

        GetComponent<Projectile>().triggerAbility = false;
        GetComponent<Projectile>().abilityUses--;
    }
}
