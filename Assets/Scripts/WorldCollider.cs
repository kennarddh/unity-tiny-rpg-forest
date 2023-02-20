using System;

using UnityEngine;

public class WorldCollider : MonoBehaviour
{
    PolygonCollider2D polygonCollider2D;
    EdgeCollider2D edgeCollider2D;

    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        edgeCollider2D.offset = polygonCollider2D.offset;

        Vector2[] newPoints = polygonCollider2D.points;

        Array.Resize(ref newPoints, newPoints.Length + 1);
        newPoints[newPoints.GetUpperBound(0)] = newPoints[0];

        edgeCollider2D.points = newPoints;
    }
}