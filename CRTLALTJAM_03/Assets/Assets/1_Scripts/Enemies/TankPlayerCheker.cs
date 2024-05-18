using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerCheker : MonoBehaviour
{
    [SerializeField] LitToyTank litToyTank;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerScript player))
        {
            
            litToyTank.PlayerFound(player.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerScript player))
        {
            litToyTank.PlayerLost();
        }
    }
}
