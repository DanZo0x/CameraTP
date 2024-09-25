using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AView : MonoBehaviour
{
    public float weight = 0f;
    public virtual CameraConfiguration GetConfiguration()
    {
        return null;
    }
}

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();
        config.conf.yaw = yaw;
        config.conf.pitch = pitch;
        config.conf.roll = roll;
        config.conf.fov = fov;
        config.conf.distance = 0;

        return config;
    }

}
