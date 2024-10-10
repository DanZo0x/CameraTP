using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour 
{
    [SerializeField] List<AViewVolum> _activeViewVolumes = new List<AViewVolum>();
    Dictionary<AView, AViewVolum> _volumesPerViews;

    public void AddVolume(AViewVolum volume)
    {
        _activeViewVolumes.Add(volume);
        if(!_volumesPerViews.ContainsKey(volume.View))
        {
            volume.View.SetActive(true);
        }
        _volumesPerViews[volume.View] = volume;
    }
    public void RemoveVolume(AViewVolum volume)
    {
        _activeViewVolumes.Remove(volume);
        _volumesPerViews.Remove(volume.View);
        volume.View.SetActive(false);
    }
    private void OnGUI()
    {
        foreach (var volume in _activeViewVolumes) 
        { 
            GUILayout.Label(volume.View.name);
        }
    }
}
