using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreck : Flammable
{
    [SerializeField] float force, activeForceMultiplier;

    private void Update()
    {
        CanDeactivateFlame();
    }

    public override void ActiveFlame(float time)
    {
        fireActive = true;
        timeActive = baseActiveFlameTime + time;
        transform.localScale *= 2;
    }

    public override void DeactivateFlame()
    {
        fireActive = false;
        transform.localScale /= 2;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody targetRB))
        {
            if(!fireActive)
                targetRB.AddForce(transform.forward * force, ForceMode.Force);
            else
                targetRB.AddForce(transform.forward * force * activeForceMultiplier, ForceMode.Acceleration);
        }
    }
}
