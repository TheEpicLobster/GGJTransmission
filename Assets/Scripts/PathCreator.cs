using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour {

    [SerializeField, HideInInspector]
    public List<Vector2> points;

    PathCreator()
    {
        points = new List<Vector2>();
    }

    public void AddSegment(Vector2 newPoint)
    {
        points.Add(newPoint);
    }

    public void MoveSegment(int index, Vector2 newPos)
    {
        points[index] = newPos;
    }
}
