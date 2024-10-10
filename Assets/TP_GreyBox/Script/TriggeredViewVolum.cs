using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggeredViewVolum : AViewVolum
{
    const string TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        SetActive(false);
    }
}
