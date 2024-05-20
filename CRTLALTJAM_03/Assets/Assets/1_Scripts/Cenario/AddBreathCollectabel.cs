using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBreathCollectabel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerScript player))
        {
            player.AddBreath();
            Destroy(gameObject);
        }
    }
}
