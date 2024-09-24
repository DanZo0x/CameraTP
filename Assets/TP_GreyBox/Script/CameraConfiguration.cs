using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CameraConfiguration : MonoBehaviour
{
    [Serializable]
    public struct CameraConf
    {
        [Range(0f, 360f)]
        public float yaw;
        [Range(-90f, 90f)]
        public float pitch;
        [Range(-180f, 180f)]
        public float roll;
        public Vector3 pivot;
        public float distance;
        public float fov;

        public Quaternion GetRotation()
        {
            return Quaternion.Euler(yaw, pitch, roll);
        }
        public Vector3 GetPosition()
        {
            return pivot + (GetRotation() * (Vector3.back * distance));
        }
    }

    public CameraConf conf;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(conf.pivot, .25f);
        Vector3 position = conf.GetPosition();
        Gizmos.DrawLine(conf.pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, conf.GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, conf.fov, 5f, 0.5f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;

    }
}
