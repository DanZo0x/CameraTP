using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
static class MathUtils
{
    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 AC = target - a;
        Vector3 n = (b - a).normalized;
        float dot = Vector3.Dot(AC, n);
        dot = Mathf.Clamp(dot, 0, Vector3.Distance(a, b));
        Vector3 projC = a + n * dot;
        return projC;
    }

    public static Vector3 LinearBezier(Vector3 A, Vector3 B, float t)
    {
        return Vector3.Lerp(A, B, t);
    }
    public static Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t)
    {
        return Vector3.Lerp(Vector3.Lerp(A, B, t), Vector3.Lerp(B, C, t), t);
    }
    public static Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {
        return Vector3.Lerp(QuadraticBezier(A, B, C, t), QuadraticBezier(B, C, D, t), t);
    }
}