using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocityNeeded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionSpeed = collision.relativeVelocity;

        if (collisionSpeed.magnitude > velocityNeeded)
        {


        }
    }
}
