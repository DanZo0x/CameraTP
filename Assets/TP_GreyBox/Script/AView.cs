using UnityEditor;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight = 0f;

    public abstract CameraConf GetConfiguration();

    public void SetActive(bool isActive)
    {
        CameraController.Instance.AddViews(this);
    }

    public virtual void OnDrawGizmos()
    {
        GetConfiguration().DrawGizmos(Color.red);
    }
}
