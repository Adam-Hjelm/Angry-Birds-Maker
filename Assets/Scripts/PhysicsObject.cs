using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] float maxDamage;
    [SerializeField] float minDamage;

    private float startObjHealth;
    public float objHealth;
    public Sprite[] sprites;
    private SpriteRenderer spriteRend;
    private float requiredVelocity;

    public ParticleSystem damageParticle;
    public Sprite damagedSprite;
    public ParticleSystem destroyedParticle;

    private bool isDestroyed = false;

    public Color metalColor;
    public Color stoneColor;
    public Color woodColor;

    public int shapeIndex;
    public int isRotatedIndex = 0;

    public WintrackingScript wintracker;

    private bool isSimulatingPhysics = false;

    public enum ObjectState
    {
        Enemy,
        WoodBlock,
        StoneBlock,
        MetalBlock
    }

    public ObjectState currentObjectState;

    void Start()
    {
        startObjHealth = objHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        requiredVelocity = startObjHealth / 2;

        if (currentObjectState == ObjectState.Enemy)
            wintracker = GameObject.Find("LevelParts").GetComponentInChildren<WintrackingScript>();

        StartCoroutine(WaitForPhysics());
    }

    IEnumerator WaitForPhysics()
    {
        isSimulatingPhysics = false;
        yield return new WaitForSeconds(1);
        isSimulatingPhysics = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSimulatingPhysics) return;

        Vector2 collisionSpeed = collision.relativeVelocity;
        Rigidbody2D otherRBody = collision.gameObject.GetComponent<Rigidbody2D>();

        if (collisionSpeed.magnitude > requiredVelocity)
        {
            float damageToTake;
            damageToTake = collisionSpeed.magnitude;

            Mathf.Clamp(damageToTake, minDamage, maxDamage);
            TakeDamage(damageToTake);

            if (isDestroyed && otherRBody != null)
                otherRBody.velocity = collisionSpeed / 2;
        }

    }

    private void TakeDamage(float damagetaken)
    {
        objHealth -= damagetaken;

        if (damageParticle != null)
        {
            ParticleSystem newDamageParticle = Instantiate(damageParticle, transform.position, Quaternion.identity);
            var main = newDamageParticle.main;
            main.startColor = gameObject.GetComponent<SpriteRenderer>().color;

            Destroy(newDamageParticle.gameObject, main.duration);
        }

        if (startObjHealth / 2 >= objHealth && damagedSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprite;
        }

        if (objHealth <= 0)
        {
            DestroySelf();
            isDestroyed = true;
        }
    }

    private void DestroySelf()
    {
        objHealth = 0;

        if (!gameObject.CompareTag("Enemy"))
        {
            var newParticle = Instantiate(destroyedParticle, transform.position, Quaternion.identity);

            var main = newParticle.main;
            switch (currentObjectState)
            {
                case ObjectState.WoodBlock:
                    main.startColor = woodColor;
                    break;
                case ObjectState.StoneBlock:
                    main.startColor = stoneColor;
                    break;
                case ObjectState.MetalBlock:
                    main.startColor = metalColor;
                    break;
                default:
                    break;
            }
            Destroy(newParticle, 1);
        }
        else
        {
            wintracker.CheckIfMatchWon();
        }
        Destroy(gameObject);
    }
}
