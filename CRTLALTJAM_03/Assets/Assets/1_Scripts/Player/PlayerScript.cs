using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int maxBreath, breath, maxLife, life;

    float breathTemp, invulnerableTemp,timeHolding, jumpWallCooldown;
    bool hasStartedHolding;

    public bool isOnDialogue;

    [SerializeField] float aceleration;

    public float Aceleration => aceleration;

    public float coyoteTimeCounter;
    float coyoteTime =0.1f;

    public float jumpBufferTime;
    float jumpBuffer = 0.2f;


    float internalAceleration;
    public float InternalAceleration => internalAceleration;

    public void SetInternalAceleration(float value) {  internalAceleration = value; }
    public void ChangeInternalAceleration(float value) { internalAceleration += value; }


    [Range(1, 10)][SerializeField] float jumpForce;

    
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


    public Text text;

    public void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        stateManager = GetComponent<PlayerStateManager>();
        ResetValues();
    }

    public void ResetValues()
    {
        maxBreath = 1;
        breath = maxBreath;
        life = maxLife;
        GameManagerScrpt.GetInstance().canvasManager.SetLifeUI(life);
        GameManagerScrpt.GetInstance().canvasManager.SetBreathUI(breath);
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

        if (breath < maxBreath && !hasStartedHolding)
            RechargeBreath();

        if(invulnerableTemp > 0)
            invulnerableTemp -= Time.deltaTime;

        if (jumpWallCooldown > 0)
            jumpWallCooldown -= Time.deltaTime;
        else if(jumpWallCooldown < 0)
        {
            jumpWallCooldown = 0;
            internalAceleration = 0;
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
            internalAceleration = 0;
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
            GameManagerScrpt.GetInstance().canvasManager.SetBreathUI(breath);
        }

        if (breath == maxBreath)
            return;

        if (breathTemp != 0)
        {
            
            breathTemp += Time.deltaTime;
        }
        else if (IsOnGround() || stateManager.CheckCurrentState(stateManager.WallState))
        {
            GameManagerScrpt.GetInstance().canvasManager.PlayBreathUI(breath + 1);
            breathTemp += 0.1f;
        }
    }

    public bool RecoverAllBreath()
    {
        bool recover = true;

        if(breath >= maxBreath)
            recover = false;
        
        breath = maxBreath;
        breathTemp = 0;

        GameManagerScrpt.GetInstance().canvasManager.SetBreathUI(breath);

        return recover;
    }

    public void AddBreath()
    {
        maxBreath++;
    }

    #region Life
    public void TakeDamage(int value)
    {
        if (invulnerableTemp > 0 || stateManager.CheckCurrentState(stateManager.DashState))
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
            invulnerableTemp = 1f;
            CancelWind();
            life -= value;
        }

        GameManagerScrpt.GetInstance().canvasManager.SetLifeUI(life);
    }

    public bool Recover()
    {

        if (life >= maxLife)
            life = maxLife;
        else
        {
            //Recuperar vida feedback
            
            life++;
            GameManagerScrpt.GetInstance().canvasManager.SetLifeUI(life);
            return true;
        }

        return false;
    }

    public void AddMaxLife()
    {
        life++;
        maxLife++;
        GameManagerScrpt.GetInstance().canvasManager.SetLifeUI(life);
    }

    #endregion

    #region Wind

    public void InstaciateWind(int id)
    {
        breathTemp = 0;
        GameObject currentWind = Instantiate(winds[id], windSpawner);
        internalAceleration = currentWind.transform.forward.x;

        transform.LookAt(transform.position + Vector3.forward * currentWind.transform.forward.x);

        StartCoroutine(CleanDash(currentWind));
    }

    private IEnumerator CleanDash(GameObject newWind)
    {
        if(!onGround)
            PlayerRB.useGravity = false;
        yield return new WaitForSeconds(0.3f);
        PlayerRB.useGravity = true;
        internalAceleration = 0;
        yield return new WaitForSeconds(0.5f);
        Destroy(newWind);   
        
    }

    public void StartCharging()
    {
        if (breath <= 0)
            return;

        hasStartedHolding = true;
    }

    public void ChargeWind()
    {
        if (!hasStartedHolding)
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
        hasStartedHolding = false;

        GameManagerScrpt.GetInstance().canvasManager.SetBreathUI(breath);
    }

    public void ReleaseWind()
    {
        if(!onGround)
            breath -= Mathf.RoundToInt(Mathf.Ceil(timeHolding));

        GameManagerScrpt.GetInstance().canvasManager.SetBreathUI(breath);

        hasStartedHolding = false;

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
