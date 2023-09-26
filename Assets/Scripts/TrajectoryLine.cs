using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public Vector3 projectilePos;
    public Slingshot slingShotScript;

    public LineRenderer lineRend;

    private int lineSegments = 40;

    private float timeOfTheFlight = 2.5f;

    void Start()
    {
        lineRend.enabled = false;
    }

    public void DisplayTrajectoryLine(Vector3 startVelocity, Vector3 startPos)
    {
        lineRend.enabled = true;
        float timeStep = timeOfTheFlight / lineSegments;

        Vector3[] lineRendPoints = CalculateLine(timeStep, startVelocity, startPos);

        lineRend.positionCount = lineSegments;
        lineRend.SetPositions(lineRendPoints);
    }

    private Vector3[] CalculateLine(float timeStep, Vector3 startVelocity, Vector3 startPos)
    {
        Vector3[] LineRendPoints = new Vector3[lineSegments];

        LineRendPoints[0] = startPos;

        for (int i = 1; i < lineSegments; i++)
        {
            float timeDiff = timeStep * i;

            Vector3 noGravityPosition = startVelocity * timeDiff;
            Vector3 gravityDiff = -0.5f * Physics.gravity.y * timeDiff * timeDiff * Vector3.up;
            Vector3 withGravityPosition = startPos + noGravityPosition - gravityDiff;
            LineRendPoints[i] = withGravityPosition;
        }
        return LineRendPoints;
    }
}
