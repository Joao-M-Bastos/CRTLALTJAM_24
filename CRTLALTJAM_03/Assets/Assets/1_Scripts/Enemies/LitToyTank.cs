using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitToyTank : Flammable
{

    [SerializeField] float baseSpeed, activeCooldown, flammeActiveCooldown;
    [SerializeField] Gas gasA, gasB;
    [SerializeField] Transform gasExits;
    float speed;
    bool onGround, aLastActive;
    Rigidbody tankRB;

    private void Awake()
    {
        tankRB = GetComponent<Rigidbody>();
        speed = baseSpeed;
    }

    private void Update()
    {
        if (activeCooldown > 0)
            activeCooldown -= Time.deltaTime;
        else
            ChangeActiveGas(0);
        
        CanDeactivateFlame();
    }

    private void ChangeActiveGas(float time)
    {
        activeCooldown = 1f + time;

        if (aLastActive)
            gasB.ActiveFlame(time);
        else
            gasA.ActiveFlame(time);

        aLastActive = !aLastActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerScript player))
        {
            player.TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {
        if(onGround)
            tankRB.velocity = transform.forward * speed * Time.deltaTime;
    }

    public void SetOnGround(bool value)
    {
        onGround = value;
    }

    public void Flip()
    {
        if (onGround)
        {
            gasExits.Rotate(new Vector3(0, -180, 0));
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    public override void DeactivateFlame()
    {
        fireActive = false;
        speed = baseSpeed;
    }

    public override void ActiveFlame(float time)
    {
        fireActive = true;
        timeActive = baseActiveFlameTime + time;
        speed = baseSpeed * 4;

        ChangeActiveGas(timeActive - 1);
        ChangeActiveGas(timeActive - 1);
    }
}
