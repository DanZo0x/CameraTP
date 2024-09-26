using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
}
public class DollyView : AView
{
    public bool isAuto = false;

    public float roll;
    public float distance;
    public float fov;

    public Transform Target;
    public CamRail rail;
    public float distanceOnRail;

    public float speed;

    CameraConf config;
    public override CameraConf GetConfiguration()
    {
        if (Target != null || rail != null)
        {
            distanceOnRail += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            distanceOnRail = Mathf.Clamp(distanceOnRail, 0.0001f, rail.GetLenght());
            if (isAuto)
            {
                float OldDist = float.MaxValue;
                for (int i = 0; i < rail.Nods.Count - 1; i++)
                {
                    Vector3 proj = MathUtils.GetNearestPointOnSegment(rail.Nods[i], rail.Nods[i + 1], Target.position);
                    float dist = Vector3.Distance(Target.position, proj);
                    if (dist < OldDist)
                    {
                        OldDist = dist;
                        config.pivot = proj;
                    }
                }
            }
            else
            {
                config.pivot = rail.GetPosition(distanceOnRail);
            }
            Vector3 dirToPlayer = (Target.position - config.pivot).normalized;
            config.yaw = Mathf.Atan2(dirToPlayer.x, dirToPlayer.z) * Mathf.Rad2Deg; ;
            config.pitch = -Mathf.Asin(dirToPlayer.y) * Mathf.Rad2Deg;
            config.roll = roll;
            config.distance = 0;
            config.fov = fov;
            return config;

        }
        else throw new System.Exception("No Target or Rail in DollyView");
    }


}
