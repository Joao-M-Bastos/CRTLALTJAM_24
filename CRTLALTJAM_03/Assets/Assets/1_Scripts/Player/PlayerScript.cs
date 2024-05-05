using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed;
    [Range(1, 10)][SerializeField] float jumpForce;

    public float Speed => speed;
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
