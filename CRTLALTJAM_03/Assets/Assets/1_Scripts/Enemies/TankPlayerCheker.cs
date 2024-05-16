using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerCheker : MonoBehaviour
{
    [SerializeField] LitToyTank litToyTank;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerSpawner player))
        {
            
        }
    }
}
