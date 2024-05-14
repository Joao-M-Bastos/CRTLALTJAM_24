using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGroundCheck : MonoBehaviour
{
    LitToyTank litToyTank;

    private void Awake()
    {
        litToyTank = GetComponentInParent<LitToyTank>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            litToyTank.SetOnGround(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            litToyTank.SetOnGround(false);
        }
    }
}
