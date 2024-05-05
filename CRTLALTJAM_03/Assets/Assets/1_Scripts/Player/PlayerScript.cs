using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed;
    public float aceleration;
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

    public void FixedUpdate()
    {
        
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
        aceleration = 0;

        StartCoroutine(CleanDash());

        newWind.AddWindValues(0);
    }

    private IEnumerator CleanDash()
    {
        PlayerRB.useGravity = false;
        yield return new WaitForSeconds(0.3f);
        PlayerRB.useGravity = true;
    }

    public void DeleteWind()
    {
        if(currentWind != null)
            Destroy(currentWind);
    }
}
