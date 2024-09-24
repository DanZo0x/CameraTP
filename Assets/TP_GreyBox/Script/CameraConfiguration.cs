using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfiguration : MonoBehaviour
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
    }

    public Vector3 GetRotation(Vector3 quaternion)
    {
        return quaternion;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
