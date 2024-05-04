using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed;

    public float Speed => speed;

    [SerializeField] Transform windSpawner;
    [SerializeField] GameObject wind;
    [SerializeField] OnGroundCheck onGround;

    Rigidbody playerRB;

    public Rigidbody PlayerRB => playerRB;

    public void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    public bool IsOnGround()
    {
        return onGround.OnGround();
    }

    public void InstaciateWind()
    {
        GameObject newWindObj = Instantiate(wind, windSpawner);
        Wind newWind = newWindObj.GetComponent<Wind>();

        newWind.AddWindValues(0);
    }
}
