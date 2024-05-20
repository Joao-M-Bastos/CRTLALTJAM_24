using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLifeCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerScript player))
        {
            player.AddMaxLife();
            Destroy(gameObject);
        }
    }
}
