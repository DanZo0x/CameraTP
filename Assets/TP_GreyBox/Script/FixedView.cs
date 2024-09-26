using UnityEngine;

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;

    CameraConf config;
    public override CameraConf GetConfiguration()
    {
        config.yaw = yaw;
        config.pitch = pitch;
        config.roll = roll;
        config.pivot = transform.position;
        config.distance = 0;
        config.fov = fov;

        return config;
    }

}
