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
    [SerializeField] int maxLife, life;
    float breathTemp, invulnerable;
    bool startedHolding;
    public bool isOnDialogue;
    float timeHolding, jumpWallCooldown;
    [SerializeField] float speed;

    public float coyoteTimeCounter;
    float coyoteTime =0.1f;

    public float jumpBufferTime;
    float jumpBuffer = 0.2f;

    public float aceleration;
    [Range(1, 10)][SerializeField] float jumpForce;

    public float Speed => speed;
    public float JumpForce => jumpForce;
    public float JumpWallCooldown => jumpWallCooldown;

    [SerializeField] Transform windSpawner;
    [SerializeField] GameObject[] winds;
    [SerializeField] bool onGround;
    [SerializeField] LayerMask wallMask;
    PlayerStateManager stateManager;

    Rigidbody playerRB;
    Animator playerAnim;

    public Rigidbody PlayerRB => playerRB;
    public Animator PlayerAnim => playerAnim;
    public LayerMask WallMask => wallMask;

    public void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        stateManager = GetComponent<PlayerStateManager>();
        ResetValues();
    }

    public void ResetValues()
    {
        breath = maxBreath;
        life = maxLife;
    }

    public void Update()
    {
        if (Mathf.Abs(playerRB.velocity.z) > 0.1f)
        {
            Quaternion oldWindRotation = windSpawner.transform.rotation;
            transform.LookAt(transform.position + Vector3.forward * MathF.Sign(playerRB.velocity.z));
            windSpawner.transform.rotation = oldWindRotation;
        }

        if (IsOnGround())
            coyoteTimeCounter = coyoteTime;
        else
        coyoteTimeCounter -= Time.deltaTime;

        if (breath < maxBreath && !startedHolding)
            RechargeBreath();

        if(invulnerable > 0)
            invulnerable -= Time.deltaTime;

        if (jumpWallCooldown > 0)
            jumpWallCooldown -= Time.deltaTime;
        else if(jumpWallCooldown < 0)
        {
            jumpWallCooldown = 0;
            aceleration = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTime = jumpBuffer;
        }else
        {
            jumpBufferTime -= Time.deltaTime;
        }
    }


    public bool IsOnGround()
    {
        return onGround;
    }

    public void SetWallJumpCooldown()
    {
        jumpWallCooldown = 0.6f;
    }

    public void ChangeOnGround(bool value)
    {
        if (value)
        {
            jumpWallCooldown = 0;
            aceleration = 0;
            playerRB.velocity /= 2;
        }
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
        else if (IsOnGround() || stateManager.CheckCurrentState(stateManager.WallState))
            breathTemp += 0.1f;
    }

    public bool RecoverAllBreath()
    {
        bool recover = true;

        if(breath >= maxBreath)
            recover = false;
        
        breath = maxBreath;
        breathTemp = 0;

        return recover;
    }

    #region Life
    public void TakeDamage(int value)
    {
        if (invulnerable > 0 || stateManager.CheckCurrentState(stateManager.DashState))
            return;

        if (value >= life)
        {
            GameManagerScrpt.GetInstance().GameOver();
            life = 0;
            return;
        }
        else
        {
            //TomarDanoFeedback
            invulnerable = 1f;
            CancelWind();
            life -= value;
        }
    }

    public bool Recover()
    {

        if (life >= maxLife)
            life = maxLife;
        else
        {
            //Recuperar vida feedback
            
            life++;
            return true;
        }

        return false;
    }

    #endregion

    #region Wind

    public void InstaciateWind(int id)
    {
        breathTemp = 0;
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
        PlayerRB.useGravity = true;
        yield return new WaitForSeconds(7f);
        Destroy(newWind);
        
    }

    public void StartCharging()
    {
        if (breath <= 0)
            return;

        startedHolding = true;
    }

    public void ChargeWind()
    {
        if (!startedHolding)
            return;
        timeHolding += Time.deltaTime;


        if (timeHolding > breath)
            CancelWind();
    }

    public void CancelWind()
    {
        breathTemp = 0;
        breath -= Mathf.RoundToInt(Mathf.Ceil(timeHolding));
        timeHolding = 0;
        startedHolding = false;
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
                jumpWallCooldown = 0;
                break;
            case < 3:
                InstaciateWind(2);
                jumpWallCooldown = 0;
                break;
        }

        timeHolding = 0;
        
    }
    #endregion
}
