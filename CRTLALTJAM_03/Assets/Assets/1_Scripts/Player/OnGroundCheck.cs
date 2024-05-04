using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundCheck : MonoBehaviour
{
    bool onGround;

    public bool OnGround()
    {
        return onGround;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
            onGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
            onGround = false;
    }
}
