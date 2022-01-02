using System;
using UnityEngine;

public class PlayerTriggerController : MonoBehaviour
{
    public event Action Triggered;

    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        if (other.gameObject.CompareTag(GameTags.Player))
        {
            Triggered?.Invoke();
            _triggered = true;
        }
    }
}