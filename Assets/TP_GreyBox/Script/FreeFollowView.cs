using UnityEngine;

public class FreeFollowView : AView
{

    public float[] pitch = new float[3];
    public float[] roll = new float[3];
    public float[] fov = new float[3];

    public float yaw;
    public float yawSpeed;

    Curve c = new Curve(new Vector3(0,2,0), new Vector3(-5, .66f, 0), new Vector3(-4, -.5f, 0), new Vector3(0, -2, 0));
    public float curvePos;

    public override CameraConf GetConfiguration()
    {
        CameraConf conf = new CameraConf();
        conf.yaw = yaw ;

        conf.pitch = pitch[(int)curvePos];
        conf.roll = roll[(int)curvePos];
        conf.fov = fov[(int)curvePos];

        conf.pivot = c.GetPosition(curvePos/2) +transform.position;

        return conf;
    }

    private void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        c.DrawGizmos(Color.red, transform.localToWorldMatrix);
    }
}
