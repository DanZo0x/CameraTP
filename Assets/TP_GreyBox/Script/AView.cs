using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [SerializeField] bool isActiveOnStart = false;
    
    public float weight = 0f;

    private void Start()
    {
        if (isActiveOnStart) SetActive(true);
    }
    public abstract CameraConf GetConfiguration();

    void SetActive(bool isActive) 
    { 
        CameraController.Instance.AddViews(this);
    }

    public virtual void OnDrawGizmos()
    {
        GetConfiguration().DrawGizmos(Color.red);
    }
}
