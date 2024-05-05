using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChange : Wind
{
    private void Start()
    {
        force = 6;
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
