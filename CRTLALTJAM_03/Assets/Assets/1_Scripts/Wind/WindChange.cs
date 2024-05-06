using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChange : MonoBehaviour
{
    PlayerScript player;
    float force;

    private void Start()
    {
        force = 7.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerScript>();
            if (!player.IsOnGround())
            {
                player.PlayerRB.velocity = transform.forward * -2f * force;

                //Nerfa o dash para cima
                player.PlayerRB.velocity -= Vector3.up * player.PlayerRB.velocity.y / 2;
            }
        }
        if (other.CompareTag("Pushable"))
        {
            Rigidbody objRB = other.GetComponent<Rigidbody>();
            objRB.AddForce(transform.forward * force, ForceMode.Impulse);
        }
    }
}
