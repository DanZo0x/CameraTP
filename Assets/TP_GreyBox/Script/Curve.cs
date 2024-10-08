using UnityEngine;

public class Curve : MonoBehaviour
{
    [SerializeField] Vector3 A = Vector3.zero;
    [SerializeField] Vector3 B = Vector3.zero;
    [SerializeField] Vector3 C = Vector3.zero;
    [SerializeField] Vector3 D = Vector3.zero;


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
        return Quaternion.Euler(transform.eulerAngles) * MathUtils.CubicBezier(A, B, C, D, t) ;
    }
    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        Vector3 pWorld = MathUtils.CubicBezier(A, B, C, D, t);
        return localToWorldMatrix.MultiplyPoint(pWorld);
    }

    public void DrawGizmos(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = new Color(.5f, .3f, .7f);
        Gizmos.DrawSphere(Quaternion.Euler(transform.eulerAngles) * A + transform.position, .2f);
        Gizmos.DrawSphere(Quaternion.Euler(transform.eulerAngles) * B + transform.position, .2f);
        Gizmos.DrawSphere(Quaternion.Euler(transform.eulerAngles) * C + transform.position, .2f);
        Gizmos.DrawSphere(Quaternion.Euler(transform.eulerAngles) * D + transform.position, .2f);

        for (float i = 0; i < MAX; i++)
        {
            Gizmos.color = Color.Lerp(c, Color.blue, i / MAX);

            Gizmos.DrawSphere(GetPosition(i / MAX, localToWorldMatrix), .1f);

        }
    }
}
