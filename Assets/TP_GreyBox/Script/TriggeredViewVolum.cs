using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggeredViewVolum : AViewVolum
{
    const string TAG = "Player";
    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag(TAG)) isTriggered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
    private void Update()
    {
        if (isTriggered && !IsActive)
        {
            SetActive(true);
        }

        if (!isTriggered && IsActive)
        {
            SetActive(false);
        }
    }
}
