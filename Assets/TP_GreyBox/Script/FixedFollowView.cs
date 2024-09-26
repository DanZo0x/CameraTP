using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FixedFollowView : AView
{
    public Transform Target;
    public float roll;
    public float fov;

    public Transform CentralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;

    CameraConf config;

    public override CameraConf GetConfiguration()
    {
        if (Target != null || CentralPoint != null)
        {
            Vector3 dirToPlayer = (Target.position - transform.position).normalized;
            Vector3 dirToCentral = (CentralPoint.position - transform.position).normalized;
            var a = Mathf.Atan2(dirToPlayer.x, dirToPlayer.z) * Mathf.Rad2Deg;
            var b = Mathf.Atan2(dirToCentral.x, dirToCentral.z) * Mathf.Rad2Deg;
            if (Mathf.Abs(a - b) < yawOffsetMax)
            {
                config.yaw = a;
            }
            if (Mathf.Abs(a - b) < pitchOffsetMax)
            {
                config.pitch = -Mathf.Asin(dirToPlayer.y);
            }

            config.roll = roll;
            config.pivot = transform.position;
            config.distance = 0;
            config.fov = fov;

            return config;
        }
        else throw new System.Exception("No Target or CentralPoint in FollowView");

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Handles.color = Color.green;
        Vector3 dirToCentral = (CentralPoint.position - transform.position).normalized;
        dirToCentral.y = CentralPoint.localPosition.y;
        Handles.DrawWireArc(CentralPoint.position, Vector3.up, dirToCentral, yawOffsetMax, 1);
        Handles.DrawWireArc(CentralPoint.position, Vector3.up, dirToCentral, -yawOffsetMax, 1);
        Handles.color = Color.green + new Color(0,-0.5f,0);
        Handles.DrawWireArc(CentralPoint.position, Vector3.right, dirToCentral, pitchOffsetMax, 1);
        Handles.DrawWireArc(CentralPoint.position, Vector3.right, dirToCentral, -pitchOffsetMax, 1);



    }
}
