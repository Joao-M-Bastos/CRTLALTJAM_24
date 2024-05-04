using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float force = 2;

    public void AddWindValues(int addForce)
    {
        force += addForce;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if(!player.IsOnGround())
                player.PlayerRB.AddForce(transform.forward * -1 * force,ForceMode.Force);
        }
    }
}
