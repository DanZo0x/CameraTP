using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AViewVolum : MonoBehaviour
{
    public int Priority = 0;
    public AView View;

    [SerializeField] private int _uid;
    static int NextUid = 0;

    protected bool IsActive { get; set; }
    public int Uid { get => _uid; set => _uid = value; }

    public virtual float ComputeSelfWeight()
    {
        return 1f;
    }
    void Awake()
    {
        _uid = NextUid++;
    }

    protected void SetActive(bool isActive)
    {
        IsActive = isActive;
        if(IsActive) AddVolume(this);
        else RemoveVolume(this);
    }

    public virtual void AddVolume(AViewVolum volume)
    {
        ViewVolumeBlender.Instance.AddVolume(volume);
    }
    public virtual void RemoveVolume(AViewVolum volume)
    {
        ViewVolumeBlender.Instance.RemoveVolume(volume);
    }
}
