using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed;
    float aceleration;
    [Range(1, 10)][SerializeField] float jumpForce;

    public float Speed => speed;
    public float Aceleration => aceleration;
    public float JumpForce => jumpForce;

    [SerializeField] Transform windSpawner;
    [SerializeField] GameObject wind;
    GameObject currentWind;
    [SerializeField] bool onGround;

    public Text text;

    Rigidbody playerRB;

    public Rigidbody PlayerRB => playerRB;

    public void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        float baseMultiplier = 4;

        if (IsOnGround())
            baseMultiplier = 2;

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            aceleration = 0;

            aceleration -= Mathf.Sign(PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime;

            if (Mathf.Abs(PlayerRB.velocity.z) < 0.05f)
            {
                aceleration = 0;
                PlayerRB.velocity = new Vector3(0, PlayerRB.velocity.y, 0);
            }
        }
        else
        {
            if (Mathf.Abs(PlayerRB.velocity.z) < 8 && Mathf.Abs(aceleration) < 0.5f)
                aceleration += Input.GetAxisRaw("Horizontal") * (baseMultiplier + 1) * Time.deltaTime;

            if(Mathf.Abs(PlayerRB.velocity.z) > 8)
                aceleration = 0;

            if (Mathf.Sign(PlayerRB.velocity.z) != Mathf.Sign(Input.GetAxisRaw("Horizontal")))
                aceleration -= Mathf.Sign(PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime;
        }

        Debug.Log(aceleration);
    }

    public bool IsOnGround()
    {
        return onGround;
    }

    public void ChangeOnGround(bool value)
    {
        onGround = value;
    }

    public void InstaciateWind()
    {
        currentWind = Instantiate(wind, windSpawner);
        Wind newWind = currentWind.GetComponent<Wind>();

        newWind.AddWindValues(0);
    }

    public void DeleteWind()
    {
        if(currentWind != null)
            Destroy(currentWind);
    }
}
