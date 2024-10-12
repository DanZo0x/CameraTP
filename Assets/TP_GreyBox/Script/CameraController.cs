using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [SerializeField]
    float smoothTime = .8f;

    [SerializeField] 
    private List<AView> _activeViews = new();
    public List<AView> ActiveViews { get => _activeViews; set => _activeViews = value; }

    
    public CameraConf _actualConf;


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

            Rollsum += new Vector2(Mathf.Cos(config.roll * Mathf.Deg2Rad),
            Mathf.Sin(config.roll * Mathf.Deg2Rad)) * view.weight;

            Pitchsum += new Vector2(Mathf.Cos(config.pitch * Mathf.Deg2Rad),
            Mathf.Sin(config.pitch * Mathf.Deg2Rad)) * view.weight;

            Yawsum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;

            Possum += config.GetPosition() * view.weight;
            Fovsum += config.fov * view.weight;
            weightSum += view.weight;
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
        transform.position = Vector3.Lerp(transform.position,_actualConf.GetPosition(), smoothTime * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _actualConf.GetRotation(), smoothTime * Time.deltaTime);
        
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,_actualConf.fov,smoothTime * Time.deltaTime);
    }

    private void LateUpdate()
    {
        _actualConf = ComputeAverage();
        ApplyConfiguration();
    }

}
