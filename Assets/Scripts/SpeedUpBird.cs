using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBird : MonoBehaviour
{
    public ParticleSystem speedUpFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Projectile>().triggerAbility)
        {
            SpeedUp();
        }
    }

    private void SpeedUp()
    {
        var projectileRbody = GetComponent<Rigidbody2D>();
        var newSpeedUpFx = Instantiate(speedUpFX, transform.position, Quaternion.LookRotation(projectileRbody.velocity * -1));
        Destroy(newSpeedUpFx, 1);


        projectileRbody.velocity *= 2.5f;
        //projectileRbody.mass /= 2;
        GetComponent<Projectile>().triggerAbility = false;
        GetComponent<Projectile>().abilityUses--;
    }
}
