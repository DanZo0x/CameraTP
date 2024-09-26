using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRail : MonoBehaviour
{
    public bool isLoop = false;

    List<Vector3> _nods = new List<Vector3>();
    [SerializeField] float length = 0;
    [SerializeField] float drawBall = 0;

    public List<Vector3> Nods { get => _nods;}

    public float GetLenght() => length;

    public Vector3 GetPosition(float distance)
    {
        distance = Mathf.Clamp(distance,0,length);
        float testDistance = 0;
        int i = 1;
        while(testDistance < distance)
        {
            testDistance += Vector3.Distance(Nods[i - 1], Nods[i]);
            if(testDistance < distance) i++;
        }
        testDistance -= Vector3.Distance(Nods[i - 1], Nods[i]);
        distance -= testDistance;
        float max = Vector3.Distance(Nods[i - 1], Nods[i]);

        return Vector3.Lerp(Nods[i - 1], Nods[i], distance / max);
    }


    void Start()
    {
        Nods.Clear();
        length = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Nods.Add(transform.GetChild(i).position);
            if(i != 0) length += Vector3.Distance(Nods[i - 1], Nods[i]);
        }

        if (isLoop)
        {
            length += Vector3.Distance(Nods[0], Nods[^1]);
            Nods.Add(Nods[0]);
        }

    }

    
    private void OnValidate()
    {
        Start();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(.7f,.3f,0,1);
        
        for(int i = 1; i < Nods.Count; i++)
        {
            Gizmos.DrawLine(Nods[i - 1], Nods[i]);
        }
        Gizmos.color = new Color(.6f, .4f, 0.5f, 1);

        if (isLoop) Gizmos.DrawLine(Nods[0], Nods[^1]);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetPosition(drawBall), .1f);
    }
}
