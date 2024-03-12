using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event Action<object, Character> PlayerEntered;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.attachedRigidbody == null)
        {
            return;
        }
        
        if (collider.attachedRigidbody.CompareTag("Player") == true &&
            collider.attachedRigidbody.TryGetComponent(out Character character) == true)
        {
            PlayerEntered?.Invoke(this, character);
        }
    }
}
