using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Camera cam;
    public CameraConfiguration.CameraConf conf;

    #region Singleton Pattern
    private void Awake()
    {
        if (cam == null)
        {
            Debug.LogWarning("CAM WAS NULL SET TO CAM.MAIN");
            cam = Camera.main;
        }
        else
        {
            Debug.LogWarning("CAM'S NOT NULL");
        }
    }
    #endregion

    void ApplyConfiguration()
    {
        cam.transform.position = conf.GetPosition();
        cam.transform.rotation = conf.GetRotation();
    }

    private void LateUpdate()
    {
        ApplyConfiguration();
    }

}
