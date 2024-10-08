using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AViewVolum : MonoBehaviour
{
    public int Priority = 0;
    public AView view;

    private int Uid;
    static int NextUid = 0;

    public virtual float ComputeSelfWeight()
    {
        return 1f;
    }
    void Awake()
    {
        Uid = NextUid;
        NextUid++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
