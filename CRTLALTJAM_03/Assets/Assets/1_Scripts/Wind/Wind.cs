using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    protected PlayerScript player;
    protected float force;

    public void AddWindValues(int addForce)
    {
        force += addForce;
    }

    /*
    public void FixedUpdate()
    {
        if (player == null)
            return;

        if (!player.IsOnGround())
            player.PlayerRB.AddForce(transform.forward * -1 * force, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerScript>();
            if (!player.IsOnGround())
                player.PlayerRB.AddForce(transform.forward * -2f * force, ForceMode.Impulse);
        }
    }

    */
}
