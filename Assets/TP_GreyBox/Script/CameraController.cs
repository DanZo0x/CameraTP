using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    public CameraController Instance;

    public CameraConfiguration.CameraConf _actualConf;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void ApplyConfiguration()
    {
        transform.position = _actualConf.GetPosition();
        transform.rotation = _actualConf.GetRotation();
    }

    private void LateUpdate()
    {
        ApplyConfiguration();
    }

}
