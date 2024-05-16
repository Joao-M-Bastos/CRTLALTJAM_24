using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitToyTank : Flammable
{

    [SerializeField] float baseSpeed, activeCooldown, flammeActiveCooldown;
    [SerializeField] Gas gasA, gasB;
    [SerializeField] Transform gasExits;
    Transform playerPosition;
    float speed;
    bool onGround, lookingRight;
    Rigidbody tankRB;

    private void Awake()
    {
        tankRB = GetComponent<Rigidbody>();
        speed = baseSpeed;
    }

    private void Update()
    {
        if(!gasB.gameObject.activeSelf && playerPosition.position.x - transform.position.x > 0)
            gasB.gameObject.SetActive(true);
        else if(!gasA.gameObject.activeSelf && playerPosition.position.x - transform.position.x < 0)
            gasA.gameObject.SetActive(true);


        if (activeCooldown > 0)
            activeCooldown -= Time.deltaTime;
        else
            TryActivateGas(0);
        
        CanDeactivateFlame();
    }

    private void TryActivateGas(float time)
    {
        if (playerPosition == null)
            return;

        activeCooldown = 1f + time;

        if (playerPosition.position.x - transform.position.x > 0)
            gasB.ActiveFlame(time);
        else
            gasA.ActiveFlame(time);
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

        if (playerPosition == null)
            return;

        if (onGround)
            tankRB.velocity = transform.forward * speed * Time.deltaTime * 0.3f;

        if (playerPosition.position.x - transform.position.x < 0 && lookingRight)
            Flip();
        if (playerPosition.position.x - transform.position.x > 0 && !lookingRight)
            Flip();
    }

    public void SetOnGround(bool value)
    {
        onGround = value;
    }

    public void Flip()
    {
        if (onGround)
        {
            lookingRight = !lookingRight;
            gasExits.Rotate(new Vector3(0, -180, 0));
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    public void PlayerFound(Transform player)
    {
        playerPosition = player;
    }

    public void PlayerLost()
    {
        playerPosition = null;

        if (!fireActive)
        {
            gasB.gameObject.SetActive(false);
            gasA.gameObject.SetActive(false);
        }
    }

    public override void DeactivateFlame()
    {
        fireActive = false;
        speed = baseSpeed;

        gasA.gameObject.SetActive(false);
        gasB.gameObject.SetActive(false);
    }

    public override void ActiveFlame(float time)
    {
        fireActive = true;
        timeActive = baseActiveFlameTime + time;
        speed = baseSpeed * 3;

        gasA.gameObject.SetActive(true);
        gasB.gameObject.SetActive(true);

        gasA.ActiveFlame(time);
        gasB.ActiveFlame(time);
    }
}
