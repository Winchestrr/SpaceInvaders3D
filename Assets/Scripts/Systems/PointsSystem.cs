using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    public static PointsSystem instance;

    public static int points;
    public int pointsDisplay;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    public static void AddPoints(int value)
    {
        points += value;
        instance.pointsDisplay = points;
    }
}
