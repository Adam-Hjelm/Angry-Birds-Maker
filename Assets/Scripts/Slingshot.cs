using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;

    bool isMouseDown;

    public GameObject[] birdPrefabs;

    public float birdPositionOffset;

    Rigidbody2D birdRBody;
    Collider2D birdCollider;

    public float force;
    public Vector3 birdVelocity;

    public int currentShots;

    public ColorHandler colorHandler;

    private TrajectoryLine trajectoryLine;

    void Start()
    {
        trajectoryLine = GetComponent<TrajectoryLine>();

        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        InstantiateBird();
    }

    void InstantiateBird()
    {
        int randomNumber = Random.Range(0, 3); // change from random so you can choose birds during map creation

        GameObject newBird = Instantiate(birdPrefabs[randomNumber]);

        colorHandler.birdSprite = newBird.GetComponent<SpriteRenderer>();
        birdRBody = newBird.GetComponent<Rigidbody2D>();
        birdCollider = newBird.GetComponent<Collider2D>();
        birdCollider.enabled = false;

        birdRBody.isKinematic = true;

        ResetStrips();
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

            currentPosition.y = Mathf.Clamp(currentPosition.y, bottomBoundary, 1000);

            trajectoryLine.projectilePos = currentPosition;
            trajectoryLine.DisplayTrajectoryLine(-1 * force * (currentPosition - center.position), currentPosition);

            SetStrips(currentPosition);

            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
            trajectoryLine.lineRend.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        if (birdRBody != null)
        {
            Shoot();
        }
        currentPosition = idlePosition.position;
    }

    void Shoot()
    {
        birdRBody.isKinematic = false;
        birdVelocity = -1 * force * (currentPosition - center.position);
        birdRBody.velocity = birdVelocity;
        birdRBody.gameObject.GetComponent<Projectile>().isAirborne = true;

        Destroy(birdRBody.gameObject, 10);

        birdRBody = null;
        birdCollider = null;
        Invoke(nameof(InstantiateBird), 1);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (birdRBody)
        {
            Vector3 dir = position - center.position;
            birdRBody.transform.position = position + dir.normalized * birdPositionOffset;
            birdRBody.transform.right = -dir.normalized;
        }
    }
}