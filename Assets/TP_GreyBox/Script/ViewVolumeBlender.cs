using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    [SerializeField] List<AViewVolum> _activeViewVolumes = new List<AViewVolum>();
    Dictionary<AView, AViewVolum> _volumesPerViews = new Dictionary<AView, AViewVolum>();

    public static ViewVolumeBlender Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void AddVolume(AViewVolum volume)
    {
        _activeViewVolumes.Add(volume);
        if (!_volumesPerViews.ContainsKey(volume.View))
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


    private void Update()
    {
        _activeViewVolumes.Sort((x, y) =>
        {
            var t = x.Priority.CompareTo(y.Priority);
            return t != 0 ? t : y.Uid.CompareTo(x.Uid);
        });
        foreach (AViewVolum volume in _activeViewVolumes)
        {
            float weight = Mathf.Clamp01(volume.ComputeSelfWeight());
            float remainingWheight = 1f - weight;

            foreach (AViewVolum v2 in _activeViewVolumes) v2.View.weight *= remainingWheight;

            volume.View.weight += weight;

        }
        CameraController.Instance.ActiveViews = _volumesPerViews.Keys.ToList();
    }






    private void OnGUI()
    {
        foreach (var volume in _activeViewVolumes)
        {
            GUILayout.Label(volume.View.name + "Prio : " + volume.Priority);
        }
    }


}
