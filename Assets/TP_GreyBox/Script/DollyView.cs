using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
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
            Vector3 dirToPlayer = (Target.position - config.pivot).normalized;
            config.yaw = Mathf.Atan2(dirToPlayer.x, dirToPlayer.z) * Mathf.Rad2Deg; ;
            config.pitch = -Mathf.Asin(dirToPlayer.y) * Mathf.Rad2Deg;
            config.roll = roll;
            config.pivot = rail.GetPosition(distanceOnRail);
            config.distance = 0;
            config.fov = fov;
            return config;

        }
        else throw new System.Exception("No Target or Rail in DollyView");
    }
}
