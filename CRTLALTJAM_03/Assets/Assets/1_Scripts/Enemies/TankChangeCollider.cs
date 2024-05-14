using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankChangeCollider : MonoBehaviour
{
    [SerializeField] string tagToCheck;
    [SerializeField] bool isEnter;

    LitToyTank litToyTank;

    private void Awake()
    {
        litToyTank = GetComponentInParent<LitToyTank>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEnter) return;

        if (other.gameObject.CompareTag(tagToCheck))
            litToyTank.Flip();
    }


    private void OnTriggerExit(Collider other)
    {
        if (isEnter) return;

        if (other.gameObject.CompareTag(tagToCheck))
            litToyTank.Flip();
    }
}
