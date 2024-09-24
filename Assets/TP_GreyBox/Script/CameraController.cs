using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraConfUtilities
{
    [SerializeField]
    public struct CameraConf
    {
        public float yaw;
        public float pitch;
        public float roll;
        public Vector3 pivot;
        public float distance;
        public float fov;

        public Quaternion GetRotation(Vector3 quaternion)
        {
            return new Quaternion(0, 0, 0, 0);
        }
        public Vector3 GetPosition(Vector3 pivot, float Offset)
        {
            return Vector3.zero;
        }
    }
}

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
