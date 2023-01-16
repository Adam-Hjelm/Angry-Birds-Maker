using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBird : MonoBehaviour
{
    public ParticleSystem weightParticleSys;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Projectile>().triggerAbility)
        {
            WeightUo();
        }
    }

    private void WeightUo()
    {
        var projectileRbody = GetComponent<Rigidbody2D>();
        var newWeightFX = Instantiate(weightParticleSys, transform.position, Quaternion.LookRotation(projectileRbody.velocity * -1));

        projectileRbody.mass *= 5;

        GetComponent<Projectile>().triggerAbility = false;
        GetComponent<Projectile>().abilityUses--;
    }
}
