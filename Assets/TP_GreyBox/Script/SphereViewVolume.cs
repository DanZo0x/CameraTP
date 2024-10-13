using UnityEngine;

public class SphereViewVolume : AViewVolum
{
    public Transform target;
    public float outerRadius;

    private float distance;

    private SphereCollider col;
    private void OnTriggerEnter(Collider other)
    {
        IsActive = true;
        SetActive(IsActive);
    }
    private void OnTriggerExit(Collider other)
    {
        IsActive = false;
        SetActive(IsActive);
    }

    private void OnValidate()
    {
        col = GetComponent<SphereCollider>();
        col.radius = outerRadius;
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }

    public override float ComputeSelfWeight()
    {
        return 1 - distance/outerRadius;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
