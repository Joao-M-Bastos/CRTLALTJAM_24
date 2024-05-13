using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spykes : MonoBehaviour
{
    [SerializeField] Transform safePoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerScript player))
        {
            player.TakeDamage(1);
            player.PlayerRB.velocity = Vector3.zero;
            player.transform.position = safePoint.position;
        }
    }
}
