using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    public static Vector2 RotateVectorRad(Vector2 vec, float angle)
    {
        return new Vector2(vec.x * Mathf.Cos(angle) - vec.y * Mathf.Sin(angle), vec.x * Mathf.Sin(angle) + vec.y * Mathf.Cos(angle));
    }
    public static Vector2 RotateVector(Vector2 vec, float angle)
    {
        angle = Mathf.Deg2Rad * angle;
        return RotateVectorRad(vec, angle);
    }

}