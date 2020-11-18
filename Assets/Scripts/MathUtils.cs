using System;
using UnityEngine;

public static class MathUtils
{
    private const float DEFAULT_EPSILON = 0.01f;

    public static bool AreApproximatelyEqual(float a, float b, float epsilon = DEFAULT_EPSILON)
    {
        return Math.Abs(a - b) <= epsilon;
    }

    public static bool AreApproximatelyEqual(Vector3 a, Vector3 b, float epsilon = DEFAULT_EPSILON)
    {
        return  AreApproximatelyEqual(a.x, b.x, epsilon) &&
                AreApproximatelyEqual(a.y, b.y, epsilon) &&
                AreApproximatelyEqual(a.z, b.z, epsilon);
    }
}

