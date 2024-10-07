using UnityEngine;

public class Curve : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;


    const float MAX = 32;

    public Curve(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }
    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        Vector3 pWorld = MathUtils.CubicBezier(A, B, C, D, t);
        return localToWorldMatrix.MultiplyPoint(pWorld);
    }

    public void DrawGizmos(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = new Color(.5f, .3f, .7f);
        Gizmos.DrawSphere(A, .2f);
        Gizmos.DrawSphere(B, .2f);
        Gizmos.DrawSphere(C, .2f);
        Gizmos.DrawSphere(D, .2f);

        for (float i = 0; i < MAX; i++)
        {
            Gizmos.color = Color.Lerp(c, Color.blue, i / MAX);

            Gizmos.DrawSphere(GetPosition(i / MAX, localToWorldMatrix), .1f);

        }
    }
}
