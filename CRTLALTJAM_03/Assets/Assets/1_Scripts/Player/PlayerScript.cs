using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int maxBreath;
    [SerializeField] int breath;
    float breathTemp;
    bool startedHolding;
    float timeHolding;
    [SerializeField] float speed;
    public float aceleration;
    [Range(1, 10)][SerializeField] float jumpForce;

    public float Speed => speed;
    public float JumpForce => jumpForce;

    [SerializeField] Transform windSpawner;
    [SerializeField] GameObject[] winds;
    [SerializeField] bool onGround;

    public Text text;

    Rigidbody playerRB;
    Animator playerAnim;

    public Rigidbody PlayerRB => playerRB;
    public Animator PlayerAnim => playerAnim;

    public void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        breath = maxBreath;
    }

    public void Update()
    {
        if (breath < maxBreath && !startedHolding)
            RechargeBreath();
    }

    public bool IsOnGround()
    {
        return onGround;
    }

    public void ChangeOnGround(bool value)
    {
        onGround = value;
    }

    public void RechargeBreath()
    {
        if (breathTemp > 1)
        {
            breathTemp = 0;
            breath += 1;
        }

        if (breathTemp != 0)
            breathTemp += Time.deltaTime;
        else if (IsOnGround())
            breathTemp += 0.1f;
    }

    #region Wind

    public void InstaciateWind(int id)
    {
        GameObject currentWind = Instantiate(winds[id], windSpawner);
        aceleration = currentWind.transform.forward.x;

        transform.LookAt(transform.position + Vector3.forward * currentWind.transform.forward.x);

        StartCoroutine(CleanDash(currentWind));
    }

    private IEnumerator CleanDash(GameObject newWind)
    {
        if(!onGround)
            PlayerRB.useGravity = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(newWind);
        PlayerRB.useGravity = true;
    }

    public void StartCharging()
    {
        if (breath < 0)
            return;

        startedHolding = true;
    }

    public void ChargeWind()
    {
        if (!startedHolding)
            return;
        timeHolding += Time.deltaTime;


        if (timeHolding > breath)
        {
            timeHolding = 0;
            ReleaseWind();
            breath = 0;
        }
    }

    public void ReleaseWind()
    {
        if(!onGround)
            breath -= Mathf.RoundToInt(Mathf.Ceil(timeHolding));

        startedHolding = false;

        if (timeHolding == 0)
            return;

        breathTemp = 0;

        switch (timeHolding)
        {
            case < 1:
                InstaciateWind(0);
                break;

            case < 2:
                InstaciateWind(1);
                break;
            case < 3:
                InstaciateWind(2);
                break;
        }

        timeHolding = 0;
        
    }
    #endregion
}
