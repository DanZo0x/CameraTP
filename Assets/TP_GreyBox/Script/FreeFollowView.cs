using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Curve))]
public class FreeFollowView : AView
{

    [SerializeField] float[] _pitch = new float[3];
    [SerializeField] float[] _roll = new float[3];
    [SerializeField] float[] _fov = new float[3];
    
    [SerializeField] float _yaw;
    [SerializeField] float _yawSpeed;

    [SerializeField] Curve _curve;
    [SerializeField] float _curvePos;
    [SerializeField] float _pitchSpeed;


    public override CameraConf GetConfiguration()
    {
        CameraConf conf = new CameraConf();
        conf.yaw = _yaw;

        conf.pitch = Mathf.Lerp(_pitch[(int)_curvePos], _pitch[(int)_curvePos + 1], _curvePos % 1);
        conf.roll = Mathf.Lerp(_roll[(int)_curvePos], _roll[(int)_curvePos + 1], _curvePos % 1);
        conf.fov = Mathf.Lerp(_fov[(int)_curvePos], _fov[(int)_curvePos + 1], _curvePos % 1);

        conf.pivot = _curve.GetPosition(_curvePos / 2) + transform.position;

        return conf;
    }

    private void Update()
    {
        _curvePos -= Input.GetAxis("Mouse Y") * _pitchSpeed;
        _curvePos = Mathf.Clamp(_curvePos, 0, _pitch.Length-1.0000001f);

        _yaw += Input.GetAxis("Mouse X") * _yawSpeed;
        _yaw %= 360;

        transform.eulerAngles = new Vector3(0, _yaw, 0);
    }

    private void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        _curve.DrawGizmos(Color.red, transform.localToWorldMatrix);
    }
}
