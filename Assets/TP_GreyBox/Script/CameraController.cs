using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.Analytics;

public class CameraController : MonoBehaviour
{

    public static CameraController Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public CameraConf _actualConf;

    public Transform _player;
    public CameraConf _configCible;
    public CameraConf _configCourante;

    public AView _FollowView;

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
        string allAtZero = "";
        foreach (AView view in _activeViews)
        {
            double distance = Vector3.Distance(_player.position, view.transform.position);
            double powerOfView = 0;
            if (distance < 10)
            {
                powerOfView = 1 / (distance / (10 - distance));
                powerOfView = Mathf.Clamp((float)powerOfView, 0, 10);
            }
            else
            {
                allAtZero += "0";
                continue;
            }

            allAtZero += "1";

            float newWeight = (float)powerOfView * view.weight;

            CameraConf config = view.GetConfiguration();

            Rollsum += new Vector2(Mathf.Cos(config.roll * Mathf.Deg2Rad),
            Mathf.Sin(config.roll * Mathf.Deg2Rad)) * newWeight;

            Pitchsum += new Vector2(Mathf.Cos(config.pitch * Mathf.Deg2Rad),
            Mathf.Sin(config.pitch * Mathf.Deg2Rad)) * newWeight;

            Yawsum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * newWeight;

            Possum += config.GetPosition() * newWeight;
            Fovsum += config.fov * newWeight;
            weightSum += newWeight;
        }

        if (!allAtZero.Contains('1'))
        {
            _FollowView.gameObject.SetActive(true);
            return _FollowView.GetConfiguration();
        }
        else
        {
            _FollowView.gameObject.SetActive(false);
        }

        result.roll = Vector2.SignedAngle(Vector2.right, Rollsum);
        result.yaw = Vector2.SignedAngle(Vector2.right, Yawsum);
        result.pitch = Vector2.SignedAngle(Vector2.right, Pitchsum);

        result.pivot = Possum / weightSum;
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
