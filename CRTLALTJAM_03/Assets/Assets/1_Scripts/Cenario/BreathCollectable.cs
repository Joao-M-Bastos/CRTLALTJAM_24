using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerScript player))
        {
            if (player.RecoverAllBreath())
                Destroy(gameObject);
        }
    }
}
