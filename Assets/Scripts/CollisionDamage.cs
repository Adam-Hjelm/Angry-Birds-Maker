using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    private float startObjHealth;
    public float objHealth;
    public Sprite[] sprites;
    private SpriteRenderer spriteRend;
    private float requiredVelocity;

    public ParticleSystem damageParticle;
    public Sprite damagedSprite;

    private bool doSpeedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        startObjHealth = objHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        requiredVelocity = startObjHealth / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        Vector2 collisionSpeed = collision.relativeVelocity;
        Rigidbody2D otherRBody = collision.gameObject.GetComponent<Rigidbody2D>();

        if (collisionSpeed.magnitude > requiredVelocity)
        {
            float damageToTake;
            if (otherRBody != null)
            {
                damageToTake = (collisionSpeed.magnitude * otherRBody.mass) / GetComponent<Rigidbody2D>().mass;
            }
            else
            {
                damageToTake = collisionSpeed.magnitude / GetComponent<Rigidbody2D>().mass;
            }

            TakeDamage(damageToTake);
            Debug.Log(objHealth + " health left");
            Debug.Log(damageToTake);

            if (doSpeedUp && otherRBody != null)
                otherRBody.velocity = collisionSpeed / 2;

            //if (collisionSpeed.magnitude > (objHealth * 5))
            //{
            //    DestroySelf();
            //    if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            //    {
            //        collision.gameObject.GetComponent<Rigidbody2D>().velocity = collisionSpeed / 2;
            //    }
            //}
        }

    }

    private void DestroySelf()
    {
        objHealth = 0;


        //if (gameObject.CompareTag("Enemy"))
        //{

        //}
        //Do some particles explosion stuff
        Destroy(gameObject);
    }

    private void TakeDamage(float damagetaken)
    {
        objHealth -= damagetaken;

        if (damageParticle != null)
        {
            ParticleSystem newDamageParticle = Instantiate(damageParticle, transform.position, Quaternion.identity);
            var main = newDamageParticle.main;
            main.startColor = gameObject.GetComponent<SpriteRenderer>().color;

            Destroy(newDamageParticle, main.duration);
        }

        if (startObjHealth / 2 >= objHealth && damagedSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprite;
        }

        if (objHealth <= 0)
        {
            DestroySelf();
            doSpeedUp = true;
        }
    }
}
