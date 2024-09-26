using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    public static CameraController Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public CameraConf _actualConf;

    public List<AView> _activeViews = new List<AView>();


    public void AddViews(AView a) { _activeViews.Add(a); }
    public void RemoveViews(AView a) { _activeViews.Remove(a); }
    CameraConf ComputeAverage()
    {
        CameraConf result = new CameraConf();

        Vector2 Rollsum = Vector2.zero;
        Vector2 Pitchsum = Vector2.zero;
        Vector2 Yawsum = Vector2.zero;

        Vector3 Possum = Vector3.zero;

        float Fovsum = 0;

        float weightSum = 0;
        foreach (AView view in _activeViews)
        {
            CameraConf config = view.GetConfiguration();
            //Roll
            Rollsum += new Vector2(Mathf.Cos(config.roll * Mathf.Deg2Rad),
            Mathf.Sin(config.roll * Mathf.Deg2Rad)) * view.weight;
            //pitch
            Pitchsum += new Vector2(Mathf.Cos(config.pitch * Mathf.Deg2Rad),
            Mathf.Sin(config.pitch * Mathf.Deg2Rad)) * view.weight;
            //yaw
            Yawsum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;

            //pos
            print(config.GetPosition());
            Possum += config.GetPosition() * view.weight;

            Fovsum += config.fov * view.weight;

            weightSum += view.weight;
        }
        result.roll = Vector2.SignedAngle(Vector2.right, Rollsum);
        result.yaw = Vector2.SignedAngle(Vector2.right, Yawsum);
        result.pitch = Vector2.SignedAngle(Vector2.right, Pitchsum);

        result.pivot = Possum / weightSum;
        print(Possum +"  "+ weightSum);
        result.fov = Fovsum / weightSum;

        return result;
    }
    void ApplyConfiguration()
    {
        transform.position = _actualConf.GetPosition();
        transform.rotation = _actualConf.GetRotation();
        Camera.main.fieldOfView = _actualConf.fov;

    }

    private void LateUpdate()
    {
        _actualConf = ComputeAverage();
        ApplyConfiguration();
    }

}
