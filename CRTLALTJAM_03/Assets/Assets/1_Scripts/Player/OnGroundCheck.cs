using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundCheck : MonoBehaviour
{
    [SerializeField] PlayerScript player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground") || other.CompareTag("Pushable"))
            player.ChangeOnGround(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Pushable"))
            player.ChangeOnGround(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Pushable"))
            if(!player.IsOnGround())
                player.ChangeOnGround(true);
    }
}
