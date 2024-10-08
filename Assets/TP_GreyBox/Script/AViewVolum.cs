using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AViewVolum : MonoBehaviour
{
    public int Priority = 0;
    public AView View;

    [SerializeField] private int Uid;
    static int NextUid = 0;

    protected bool IsActive { get; private set; }

    public virtual float ComputeSelfWeight()
    {
        return 1f;
    }
    void Awake()
    {
        Uid = NextUid++;
    }

    protected void SetActive(bool isActive)
    {
        IsActive = isActive;
        if(IsActive) AddVolume(this);
        else RemoveVolume(this);
    }

    public virtual void AddVolume(AViewVolum volume)
    {
        volume.View.SetActive(true);
    }
    public virtual void RemoveVolume(AViewVolum volume)
    {
        volume.View.SetActive(false);
    }
}
