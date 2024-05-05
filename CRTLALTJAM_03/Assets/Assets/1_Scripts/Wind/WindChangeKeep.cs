using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class WindChangeKeep : Wind
{
    private void Start()
    {
        force = 4;
    }

    public void FixedUpdate()
    {
        if (player == null)
            return;

        //Movimentação
        if (!player.IsOnGround())
            player.PlayerRB.AddForce(transform.forward * -1 * force, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerScript>();
            if (!player.IsOnGround())
                player.PlayerRB.velocity = transform.forward * -2f * force;
        }
    }
}
